using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AnimatorNS
{
    public partial class TabControlEx : TabControl
    {
        private readonly Animator animator;

        public TabControlEx()
        {
            InitializeComponent();
            animator = new Animator
            {
                AnimationType = AnimationType.VertSlide
            };
            var anim = animator.DefaultAnimation;
            anim.TimeCoeff = 1f;
            anim.AnimateOnlyDifferences = false;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Animation Animation
        {
            get => animator.DefaultAnimation;
            set => animator.DefaultAnimation = value;
        }

        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            base.OnSelecting(e);
            // Avoid unnecessary BeginUpdate/EndUpdate if not visible
            if (Visible && TabCount > 0)
            {
                var clipRect = new Rectangle(0, ItemSize.Height + 3, Width, Height - ItemSize.Height - 3);
                animator.BeginUpdate(this, false, null, clipRect);
                BeginInvoke((MethodInvoker)(() => animator.EndUpdate(this)));
            }
        }
    }
}