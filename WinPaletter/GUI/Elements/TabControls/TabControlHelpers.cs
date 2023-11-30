using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WinPaletter.UI.WP
{
    [ToolboxItem(false)]
    public class TransparentTabPage : TabPage
    {
        public TransparentTabPage()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Text = "Tab page";
        }

        public override string Text { get; set; } = "Tab page";

        public Image Image { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cpar = base.CreateParams;
                if (!DesignMode)
                {
                    cpar.ExStyle |= 0x20;
                    return cpar;
                }
                else
                {
                    return cpar;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            if (!DesignMode)
            {
                //Makes background drawn properly, and transparent
                InvokePaintBackground(this, e);

                e.Graphics.Clear(Color.Transparent);
            }
            else
            {
                e.Graphics.Clear(Program.Style.Schemes.Main.Colors.Back);
            }

            base.OnPaint(e);
        }
    }

    [ToolboxItem(false)]
    public class TransparentTabPageCollection : TabControl.TabPageCollection
    {
        public TransparentTabPageCollection(System.Windows.Forms.TabControl owner) : base(owner)
        {

        }

        public new TransparentTabPage this[int index]
        {
            get { return (TransparentTabPage)base[index]; }
        }
    }

    internal class TransparentTabPagesHostDesigner : ParentControlDesigner
    {
        #region Private Instance Variables
        private DesignerVerbCollection m_verbs = new
        DesignerVerbCollection();
        private IDesignerHost m_DesignerHost;
        private ISelectionService m_SelectionService;
        #endregion

        public TransparentTabPagesHostDesigner() : base()
        {
            DesignerVerb verb1 = new("Add Tab", new EventHandler(OnAddPage));
            DesignerVerb verb2 = new("Remove Tab", new EventHandler(OnRemovePage));
            m_verbs.AddRange(new DesignerVerb[] { verb1, verb2 });
        }

        #region Properties

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (m_verbs.Count == 2)
                {
                    System.Windows.Forms.TabControl MyControl = (System.Windows.Forms.TabControl)Control;
                    if (MyControl.TabCount == 0)
                    {
                        m_verbs[1].Enabled = true;
                    }
                    else
                    {
                        m_verbs[1].Enabled = false;
                    }
                }
                return m_verbs;
            }
        }

        public IDesignerHost DesignerHost
        {
            get
            {
                if (m_DesignerHost == null)
                    m_DesignerHost = (IDesignerHost)(GetService(typeof(IDesignerHost)));

                return m_DesignerHost;
            }
        }

        public ISelectionService SelectionService
        {
            get
            {
                if (m_SelectionService == null)
                    m_SelectionService = (ISelectionService)(this.GetService(typeof(ISelectionService)));
                return m_SelectionService;
            }
        }

        #endregion

        void OnAddPage(Object sender, EventArgs e)
        {
            System.Windows.Forms.TabControl ParentControl = (System.Windows.Forms.TabControl)Control;
            System.Windows.Forms.Control.ControlCollection oldTabs = ParentControl.Controls;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            TransparentTabPage P = (TransparentTabPage)(DesignerHost.CreateComponent(typeof(TransparentTabPage)));
            P.Text = P.Name;
            ParentControl.TabPages.Add(P);

            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"],
            oldTabs, ParentControl.TabPages);
            ParentControl.SelectedTab = P;

            SetVerbs();

        }

        void OnRemovePage(Object sender, EventArgs e)
        {
            System.Windows.Forms.TabControl ParentControl = (System.Windows.Forms.TabControl)Control;
            System.Windows.Forms.Control.ControlCollection oldTabs = ParentControl.Controls;

            if (ParentControl.SelectedIndex < 0) return;

            RaiseComponentChanging(TypeDescriptor.GetProperties(ParentControl)["TabPages"]);

            DesignerHost.DestroyComponent(ParentControl.TabPages[ParentControl.SelectedIndex]);

            RaiseComponentChanged(TypeDescriptor.GetProperties(ParentControl)["TabPages"],
            oldTabs, ParentControl.TabPages);

            SelectionService.SetSelectedComponents(new IComponent[] { ParentControl }, SelectionTypes.Auto);

            SetVerbs();
        }

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            TransparentTabPage newTab = new() { Text = "New tab" };
            ((System.Windows.Forms.TabControl)Control).TabPages.Add(newTab);
        }

        private void SetVerbs()
        {
            System.Windows.Forms.TabControl ParentControl = (System.Windows.Forms.TabControl)Control;

            switch (ParentControl.TabPages.Count)
            {
                case 0:
                    Verbs[1].Enabled = false;
                    break;
                default:
                    Verbs[1].Enabled = true;
                    break;
            }
        }

        private const int WM_NCHITTEST = 0x84;

        private const int HTTRANSPARENT = -1;
        private const int HTCLIENT = 1;

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
            {
                //select tabcontrol when Tabcontrol clicked outside of TabItem
                if (m.Result.ToInt32() == HTTRANSPARENT)
                    m.Result = (IntPtr)HTCLIENT;
            }
        }

        private enum TabControlHitTest
        {
            TCHT_NOWHERE = 1,
            TCHT_ONITEMICON = 2,
            TCHT_ONITEMLABEL = 4,
            TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
        }

        private const int TCM_HITTEST = 0x130D;

        private struct TCHITTESTINFO
        {
            public System.Drawing.Point pt;
            public TabControlHitTest flags;
        }

        protected override bool GetHitTest(System.Drawing.Point point)
        {
            if (this.SelectionService.PrimarySelection == this.Control)
            {
                TCHITTESTINFO hti = new();

                hti.pt = this.Control.PointToClient(point);
                hti.flags = 0;

                System.Windows.Forms.Message m = new
                System.Windows.Forms.Message();
                m.HWnd = this.Control.Handle;
                m.Msg = TCM_HITTEST;

                IntPtr lparam =
                System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(hti));
                System.Runtime.InteropServices.Marshal.StructureToPtr(hti, lparam, false);
                m.LParam = lparam;

                base.WndProc(ref m);
                System.Runtime.InteropServices.Marshal.FreeHGlobal(lparam);

                if (m.Result.ToInt32() != -1)
                    return hti.flags != TabControlHitTest.TCHT_NOWHERE;

            }

            return false;
        }

        //Fix the AllSizable selectionrule on DockStyle.Fill
        public override System.Windows.Forms.Design.SelectionRules
        SelectionRules
        {
            get
            {
                if (Control.Dock == System.Windows.Forms.DockStyle.Fill)
                    return
                    System.Windows.Forms.Design.SelectionRules.Visible;
                return base.SelectionRules;
            }
        }
    }
}