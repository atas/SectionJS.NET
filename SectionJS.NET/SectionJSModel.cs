using System.Collections;
using System.Collections.Generic;

namespace SectionJS.NET
{
    public class SectionJSModel : IEnumerable<SectionJSModel.Section>
    {
        /// <summary>
        /// Sections List
        /// </summary>
        public List<Section> Sections = new List<Section>();

        /// <summary>
        /// Add a new section into the Sections List
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        public void Add(string viewName, object model = null)
        {
            Sections.Add(new Section { ViewName = viewName, Model = model });
        }

        /// <summary>
        /// Child class to wrap ViewName and Model. More readable than Dictionary&lt;string, objec&gt;
        /// </summary>
        public class Section
        {
            public string ViewName { get; set; }
            public object Model { get; set; }
        }

        /// <summary>
        /// For being able to use SectionsJSModel with Foreach loop
        /// </summary>
        /// <returns></returns>
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
