using System.Collections;
using System.Collections.Generic;

namespace SectionJS.NET
{
    public class SectionJSModel : IEnumerable<SectionJSModel.Section>
    {
        /// <summary>
        /// Sections
        /// </summary>
        public List<Section> Sections = new List<Section>();

        public void Add(string viewName, object model = null)
        {
            Sections.Add(new Section { ViewName = viewName, Model = model });
        }

        public class Section
        {
            public string ViewName { get; set; }
            public object Model { get; set; }
        }

        public IEnumerator<Section> GetEnumerator()
        {
            return Sections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
