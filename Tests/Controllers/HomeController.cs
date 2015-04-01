// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="">
//   
// </copyright>
// <summary>
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebApplication1.Controllers
{
    using System;
    using System.Web.Mvc;

    using WebApplication1.Models;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Index()
        {
            var model = new TestModel
                            {
                                ConfirmPassword = "Confirm 12345", 
                                NewPassword = "new 12345", 
                                OldPassword = "old 12345", 
                                HiddenTest = 123, 
                                Time = DateTime.Now, 
                                NulableBoolean = true, 
                                SelectedIds = 5, 
                                Phone = "029917064", 
                                ApplyAnnotations = true, 
                            };
            return this.View(model);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Index(TestModel model)
        {
            return this.View(model);
        }

        #endregion
    }
}