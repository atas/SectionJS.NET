using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SectionJS.NET;

namespace SectionJS.Test
{
    [TestClass]
    public class SectionJSManagerTest
    {
        protected readonly Func<string, ViewResult> ViewCallback = str => new ViewResult { ViewName = str };

        protected Controller Controller;

        /// <summary>
        /// Create a new controller everytime before a test runs
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            Controller = new Mock<Controller>().Object;
        }

        /// <summary>
        /// Dispose the controller after everytime a test method finishes
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            Controller.Dispose();
        }

        /// <summary>
        /// A simple test to see if the SectionJSManager is working
        /// </summary>
        [TestMethod]
        public void SimpleTest()
        {
            const string sectionName = "sectionViewName";
            //Use the manager
            var manager = new SectionJSManager(Controller.ViewBag, ViewCallback);
            manager.Add(sectionName);
            var result = manager.ToViewResult();

            //Get SectionList
            Dictionary<string, object> sectionList = Controller.ViewBag._SectionList;

            //We have a section, it is not null
            Assert.IsNotNull(Controller.ViewBag._SectionList);

            //We have added 1 section
            Assert.AreEqual(sectionList.Count, 1);

            //We have added partialViewName
            Assert.IsTrue(sectionList.ContainsKey(sectionName));

            //The main view name is SectionJS should be assigned
            Assert.AreEqual(manager.SectionJSViewFile, result.ViewName);
        }

        /// <summary>
        /// Tests Multiple Add() method for multiple sections
        /// </summary>
        [TestMethod]
        public void MultipleSectionAddTest()
        {
            const string firstSection = "anotherView";
            const string secondSection = "theotherView";
            //Use the manager
            var manager = new SectionJSManager(Controller.ViewBag, ViewCallback);
            manager.Add(firstSection);
            manager.Add(secondSection);
            manager.ToViewResult();

            //Get SectionList
            Dictionary<string, object> sectionList = Controller.ViewBag._SectionList;

            //Asserts

            //We have a section, it is not null
            Assert.IsNotNull(Controller.ViewBag._SectionList);

            //We have added 2 sections
            Assert.AreEqual(sectionList.Count, 2);

            //We have added partialViewName
            Assert.IsTrue(sectionList.ContainsKey(firstSection));
            Assert.IsTrue(sectionList.ContainsKey(secondSection));
        }

        /// <summary>
        /// Tests using a custom view file
        /// </summary>
        [TestMethod]
        public void CustomViewFileTest()
        {
            const string customViewName = "customViewFile";

            //Use the manager
            var manager = new SectionJSManager(Controller.ViewBag, ViewCallback)
            {
                SectionJSViewFile = customViewName
            };
            manager.Add("dummy"); //just to make sure we have added some section, 
                                  //maybe we throw exception in the future if there's no section

            var result = manager.ToViewResult();

            //The main view name is SectionJS should be assigned
            Assert.AreEqual(customViewName, result.ViewName);
        }

        /// <summary>
        /// Tests using AddAbs method for adding sections with absolute path
        /// </summary>
        [TestMethod]
        public void AbsSectionTest()
        {
            const string firstSection = "anotherView";
            const string secondSection = "theotherView";

            //Use the manager
            var manager = new SectionJSManager(Controller.ViewBag, ViewCallback)
            {
                SectionJSViewFile = "CustomViewFile"
            };
            manager.AddAbs(firstSection);
            manager.AddAbs(secondSection);

            //Get SectionList
            Dictionary<string, object> sectionList = Controller.ViewBag._SectionList;

            //Asserts

            //We have a section, it is not null
            Assert.IsNotNull(Controller.ViewBag._SectionList);

            //We have added 2 sections
            Assert.AreEqual(sectionList.Count, 2);

            //We have added partialViewName
            Assert.IsTrue(sectionList.ContainsKey("~/Views/" + firstSection));
            Assert.IsTrue(sectionList.ContainsKey("~/Views/" + secondSection));
        }
    }
}
