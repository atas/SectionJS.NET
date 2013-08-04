using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SectionJS.NET;

namespace SectionJS.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SectionJsModelTest
    {
        [TestMethod]
        public void ViewNametest()
        {
            var model = new SectionJSModel {"view1", "view2"};

            Assert.AreEqual(model.Sections.Count, 2);

            int i = 0;
            foreach (var section in model)
            {
                Assert.AreEqual(section.ViewName, "view" + ++i);
            }
        }

        [TestMethod]
        public void ViewNameAndModelTest()
        {
            var model = new SectionJSModel { {"view1", "model1"}, {"view2", "model2"} };

            Assert.AreEqual(model.Sections.Count, 2);

            int i = 0;
            foreach (var section in model)
            {
                Assert.AreEqual(section.ViewName, "view" + ++i);
                Assert.AreEqual(section.Model, "model" + i);
            }
        }
    }
}
