using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Manages search functionality for a DataGridView, allowing users to filter and highlight matching items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchManager<T>
    {
        private readonly DataGridView _data;
        private readonly UI.WP.TextBox _searchBox;
        private readonly Color _highlightBackColor;
        private readonly Color _highlightForeColor;

        private Regex _highlightRegex;

        private readonly List<T> _allItems;
        private List<T> _filteredItems;

        private List<DataGridViewCell> _matchedCells = new();
        private int _matchIndex = -1;

        // Cached property accessors for string properties
        private readonly List<Func<T, string>> _stringGetters;

        // For debouncing TextChanged (optional)
        private readonly Timer _debounceTimer;

        /// <summary>
        /// Initializes a new instance of the SearchManager class.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchBox"></param>
        /// <param name="highlightBackColor"></param>
        /// <param name="highlightForeColor"></param>
        /// <param name="originalItems"></param>
        public SearchManager(DataGridView data, UI.WP.TextBox searchBox,
                             Color highlightBackColor, Color highlightForeColor,
                             BindingList<T> originalItems)
        {
            _data = data;
            _searchBox = searchBox;
            _highlightBackColor = highlightBackColor;
            _highlightForeColor = highlightForeColor;

            _allItems = [.. originalItems];
            _filteredItems = [.. _allItems];

            _data.DataSource = new BindingList<T>(_filteredItems);

            // Cache string property getters once (no reflection later)
            _stringGetters = [.. typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.PropertyType == typeof(string) && p.GetGetMethod() != null)
                .Select(CreateGetter)];

            _searchBox.TextChanged += SearchBox_TextChanged;
            _data.CellPainting += Data_CellPainting;
            _data.KeyDown += Data_KeyDown;

            _debounceTimer = new Timer { Interval = 250 };
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        // Compiles a Func<T,string> getter for property p
        private static Func<T, string> CreateGetter(PropertyInfo p)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var prop = Expression.Property(param, p);
            var convert = Expression.TypeAs(prop, typeof(string));
            return Expression.Lambda<Func<T, string>>(convert, param).Compile();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            // Debounce filtering for better performance on rapid typing
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            ApplyFilter(_searchBox.Text);
        }

        private void ApplyFilter(string searchText)
        {
            _matchedCells.Clear();
            _matchIndex = -1;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                _highlightRegex = null;
                ResetDataSource(_allItems);
                return;
            }

            try
            {
                _highlightRegex = new Regex(searchText, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
            catch
            {
                // Invalid regex pattern – fallback to showing all items
                _highlightRegex = null;
                ResetDataSource(_allItems);
                return;
            }

            // Filter using the compiled regex directly
            _filteredItems = [.. _allItems.Where(item =>
                _stringGetters.Any(getter =>
                {
                    var val = getter(item);
                    return val != null && _highlightRegex.IsMatch(val);
                })
            )];

            ResetDataSource(_filteredItems);

            // Build matched cells after filtering and data bound
            _data.ClearSelection();
            _matchedCells.Clear();

            foreach (DataGridViewRow row in _data.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible && cell.Value != null &&
                        _highlightRegex.IsMatch(cell.Value.ToString()))
                    {
                        _matchedCells.Add(cell);
                    }
                }
            }

            if (_matchedCells.Count > 0)
            {
                _matchIndex = 0;
                SelectCell(_matchedCells[0]);
            }

            _data.Invalidate();
        }

        private void ResetDataSource(List<T> items)
        {
            _data.DataSource = new BindingList<T>(items);
        }

        private void SelectCell(DataGridViewCell cell)
        {
            _data.ClearSelection();
            cell.Selected = true;
            _data.CurrentCell = cell;
            _data.FirstDisplayedScrollingRowIndex = cell.RowIndex;
        }

        private void Data_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (_highlightRegex == null || e.RowIndex < 0 || e.Value == null || !_data.Columns[e.ColumnIndex].Visible)
                return;

            string fullText = e.Value.ToString();
            MatchCollection matches = _highlightRegex.Matches(fullText);
            if (matches.Count == 0)
                return;

            e.PaintBackground(e.CellBounds, true);

            using SolidBrush highlightBrush = new(_highlightBackColor);
            using SolidBrush textBrush = new(_highlightForeColor);

            TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.TextBoxControl;
            int startX = e.CellBounds.Left;
            int y = e.CellBounds.Top + (e.CellBounds.Height - TextRenderer.MeasureText("A", _data.Font, Size.Empty, flags).Height) / 2;

            int cursor = 0;
            foreach (Match match in matches)
            {
                string before = fullText.Substring(cursor, match.Index - cursor);
                Size beforeSize = TextRenderer.MeasureText(before, _data.Font, Size.Empty, flags);
                TextRenderer.DrawText(e.Graphics, before, _data.Font, new Point(startX, y), e.CellStyle.ForeColor, flags);
                startX += beforeSize.Width;

                string matchText = fullText.Substring(match.Index, match.Length);
                Size matchSize = TextRenderer.MeasureText(matchText, _data.Font, Size.Empty, flags);

                Rectangle highlightRect = new(startX, y, matchSize.Width, matchSize.Height);
                e.Graphics.FillRectangle(highlightBrush, highlightRect);
                TextRenderer.DrawText(e.Graphics, matchText, _data.Font, new Point(startX, y), textBrush.Color, flags);
                startX += matchSize.Width;

                cursor = match.Index + match.Length;
            }

            if (cursor < fullText.Length)
            {
                string rest = fullText.Substring(cursor);
                TextRenderer.DrawText(e.Graphics, rest, _data.Font, new Point(startX, y), e.CellStyle.ForeColor, flags);
            }

            e.Handled = true;
        }

        private void Data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Down)
            {
                JumpToNextMatch(true);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                JumpToNextMatch(false);
                e.Handled = true;
            }
        }

        private void JumpToNextMatch(bool forward)
        {
            if (_matchedCells.Count == 0) return;

            _matchIndex = (_matchIndex + (forward ? 1 : -1) + _matchedCells.Count) % _matchedCells.Count;
            SelectCell(_matchedCells[_matchIndex]);
        }
    }
}