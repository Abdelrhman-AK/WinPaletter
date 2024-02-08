using System.Collections.Generic;

namespace libmsstyle
{
    public class StylePart
    {
        public int PartId { get; set; }
        public string PartName { get; set; }
        public Dictionary<int, StyleState> States { get; set; }

        public StylePart()
        {
            States = new Dictionary<int, StyleState>();
        }

        public IEnumerable<StyleProperty> GetImageProperties()
        {
            foreach (KeyValuePair<int, StyleState> state in States)
            {
                List<StyleProperty> imgProps = state.Value.Properties.FindAll((p) =>
                    p.IsImageProperty()
                );

                foreach (StyleProperty imgProp in imgProps)
                {
                    yield return imgProp;
                }
            }
        }
    }
}
