// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="">
//   
// </copyright>
// <summary>
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.UI;

namespace WebApplication1.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                                //SelectedIds = 5,
                                //SelectedIds2 = 55,
                                //SelectedIds3 = 553,
                                Phone = "029917064", 
                                ApplyAnnotations = true,
                                Ids = GetIdsList(),
                                EnumerableEnum = TestEnum.Test2,
                                EnumerableEnums = new List<TestEnum> { TestEnum.Test1, TestEnum.Test3 }
                            };
            return this.View(model);
        }

        private IList<SelectListItem> GetIdsList()
        {
            var selectItems = new List<SelectListItem>();
            for (int i = 1; i < 11; i++)
            {
                selectItems.Add(new SelectListItem { Text = "value " + i, Value = i.ToString() });
            }

            return selectItems;
        }

        public ActionResult Get2IdsList(int id)
        {
            var selectItems = new List<SelectListItem>();
            for (int i = 1; i < 11; i++)
            {
                var val = id * 10 + i;
                selectItems.Add(new SelectListItem { Text = "value " + val, Value = val.ToString() });
            }

            return Json(selectItems, JsonRequestBehavior.AllowGet);
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
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index(TestModel model)
        {
            model.Ids = GetIdsList();
            model.ApplyAnnotations = true;
            return this.View(model);
        }

        #endregion
    }
}