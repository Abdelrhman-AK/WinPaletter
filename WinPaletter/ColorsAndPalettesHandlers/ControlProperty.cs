using System.Reflection;
using System.Windows.Forms;

namespace WinPaletter
{
    public class ControlProperty<T>
    {
        public Control TargetControl { get; private set; }
        public PropertyInfo Property { get; private set; }

        // Initial color — read-only
        public T InitialValue { get; private set; }

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                // Update the actual control property immediately
                Property.SetValue(TargetControl, _value);
            }
        }

        public ControlProperty(Control control, PropertyInfo property)
        {
            TargetControl = control;
            Property = property;

            // Capture the initial value (readonly)
            InitialValue = (T)property.GetValue(control);

            // Set the current value to initial value
            _value = InitialValue;
        }
    }

}
