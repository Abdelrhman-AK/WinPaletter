using System;
using System.Collections.Generic;
using System.Linq;

namespace libmsstyle
{
    public class StyleState
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public List<StyleProperty> Properties { get; set; }

        public StyleState()
        {
            Properties = new List<StyleProperty>();
        }

        public bool TryGetPropertyValue<T>(IDENTIFIER ident, ref T value)
        {
            StyleProperty prop = this.Properties.Find((p) => p.Header.nameID == (int)ident);
            if (prop == default(StyleProperty))
            {
                return false;
            }

            value = prop.GetValueAs<T>();
            return true;
        }

    }
}
