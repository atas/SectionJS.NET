using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace SectionJS.NET
{
    public class SectionJSManager
    {
        /// <summary>
        /// The file which will be outputted.
        /// </summary>
        public string SectionJSViewFile = "SectionJS";

        /// <summary>
        /// Where is the Views folder located? Default ~/Views/
        /// If you change, don't forget trailing slash please.
        /// </summary>
        public const string ViewFolder = "~/Views/";

        /// <summary>
        /// The View() method that is in the controller
        /// because it is protected and couldn't be accessed without passing it
        /// </summary>
        protected Func<string, object, ViewResult> ViewDelegate;

        /// <summary>
        /// Model to pass SectionView
        /// </summary>
        protected SectionJSModel Model = new SectionJSModel();

        /// <summary>
        /// The manager class for handling sections
        /// </summary>
        /// <param name="viewDelegate">The View method of the Controller
        /// (it is protected, not accesible, so give it as a delegate)</param>
        public SectionJSManager(Func<string, object, ViewResult> viewDelegate)
        {
            ViewDelegate = viewDelegate;
        }

        /// <summary>
        /// Adds a partial view section into the ViewResult
        /// </summary>
        /// <param name="partialViewName"></param>
        /// <param name="model"></param>
        [NotNull]
        public SectionJSManager Add([AspMvcPartialView] string partialViewName, object model = null)
        {
            Model.Add(partialViewName, model);

            return this;
        }

        /// <summary>
        /// Adds a partial view section into the ViewResult with Absolute Path from ~/Views/
        /// </summary>
        /// <param name="partialViewPath"></param>
        /// <param name="model"></param>
        [NotNull]
        public SectionJSManager AddAbs([PathReference(ViewFolder)] string partialViewPath, object model = null)
        {
            Model.Add("~/Views/" + partialViewPath, model);

            return this;
        }

        /// <summary>
        /// Converts the Sections into a ViewResult
        /// </summary>
        /// <returns></returns>
        [NotNull]
        public ViewResult ToViewResult()
        {
            return ViewDelegate(SectionJSViewFile, Model);
        }
    }
}
