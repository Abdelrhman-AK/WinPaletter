using AnimatorNS;
using System;
using System.Windows.Forms;

namespace WinPaletter
{
    internal class Animator
    {
        readonly AnimatorNS.Animator _animator;

        public Animator()
        {
            _animator = new AnimatorNS.Animator
            {
                Interval = 15,
                TimeStep = 0.07f,
                DefaultAnimation = Animation.Transparent,
                AnimationType = AnimationType.Transparent
            };
        }

        public void Show(Control control, bool parallel = false)
        {
            if (control == null || control.IsDisposed || control.IsInUse()) return;

            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => Show(control, parallel)));
                return;
            }

            if (!control.IsHandleCreated) control.CreateControl();

            control.DoubleBuffer();

            if (Program.Style.Animations)
                _animator.Show(control, parallel);
            else
                control.Visible = true;
        }

        public void ShowSync(Control control, bool parallel = false)
        {
            if (control == null || control.IsDisposed || control.IsInUse()) return;

            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => ShowSync(control, parallel)));
                return;
            }

            if (!control.IsHandleCreated) control.CreateControl();

            control.DoubleBuffer();

            if (Program.Style.Animations)
                _animator.ShowSync(control, parallel);
            else
                control.Visible = true;
        }

        public void Hide(Control control, bool parallel = false)
        {
            if (control == null || control.IsDisposed || control.IsInUse()) return;

            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => Hide(control, parallel)));
                return;
            }

            if (!control.IsHandleCreated) control.CreateControl();

            control.DoubleBuffer();

            if (Program.Style.Animations)
                _animator.Hide(control, parallel);
            else
                control.Visible = false;
        }

        public void HideSync(Control control, bool parallel = false)
        {
            if (control == null || control.IsDisposed || control.IsInUse()) return;

            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => HideSync(control, parallel)));
                return;
            }

            if (!control.IsHandleCreated) control.CreateControl();

            control.DoubleBuffer();

            if (Program.Style.Animations)
                _animator.HideSync(control, parallel);
            else
                control.Visible = false;
        }
    }
}