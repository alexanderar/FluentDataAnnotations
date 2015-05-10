// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangePasswordViewModelAnnotations.cs" company="">
//   
// </copyright>
// <summary>
//   The change password view model annotations.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using FluentDataAnnotations;

    using WebApplication1.Models;

    /// <summary>
    /// The change password view model annotations.
    /// </summary>
    public class TestModelAnnotations : FluentDataAnnotation<TestModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestModelAnnotations"/> class.
        /// </summary>
        public TestModelAnnotations()
        {            
            Func<string> action = () =>
                {
                    HttpContextWrapper httpContextWrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
                    UrlHelper urlHelper =
                        new UrlHelper(
                            new RequestContext(httpContextWrapper, RouteTable.Routes.GetRouteData(httpContextWrapper)));

                    return urlHelper.Action("Get2IdsList", "Home");
                };

            this.When(
                p => p.ApplyAnnotations, 
                () =>
                    {
                        this.For(p => p.Phone).SetReadOnly(true, false).SetDisplayName("Phone(Not Allowed to edit)");
                        this.For(x => x.ConfirmPassword)
                            .SetDisplayName("Confirm Password")
                            .SetReadOnly(
                                () =>
                                !Thread.CurrentPrincipal.Identity.Name.Equals(
                                    "alex.art84@gmail.com", 
                                    StringComparison.OrdinalIgnoreCase))
                            .SetShowForEdit(() => true)
                            .SetShowForDisplay(() => true)
                            .SetDisplayFormat("Custom format {0}", true);

                        this.For(x => x.HiddenTest).SetHiddenInput();

                        this.For(x => x.Time).SetDisplayFormat("{0:MM.dd.yyyy hh:mm:ss}", true).SetReadOnly(true, false);

                        this.For(x => x.NulableBoolean).SetReadOnly(true);

                        this.For(m => m.Phone)
                            .SetDisplayName("Phone")
                            .ApplyValueTransform(
                                s => Regex.Replace(s, @"(?<=\d{1})\d(?=\d{3})", "*", RegexOptions.Compiled));

                        this.For(x => x.OldPassword)
                            .SetDisplayName(() => "Fluent Old Password (Function)")
                            .SetShowForEdit(() => true)
                            .SetShowForDisplay(() => true)
                            .SetDataType(System.ComponentModel.DataAnnotations.DataType.Password);

                        this.For(x => x.NewPassword)
                            .SetDisplayName(Resource.TestAnnotation)
                            .SetReadOnly(
                                () =>
                                Thread.CurrentPrincipal.Identity.Name.Equals(
                                    "alex.art84@gmail.com", 
                                    StringComparison.OrdinalIgnoreCase))
                            .SetShowForEdit(() => true)
                            .SetShowForDisplay(() => true);

                        this.For(p => p.SelectedIds)
                            .SetDisplayName("Dropdown")
                            .SetDropDown(p => p.Ids, "Please select Id");

                        this.For(p => p.SelectedIds2)
                            .SetDisplayName("CascadeDropdown")
                            .SetCascadingDropDown(m => m.SelectedIds, action, "id", "Please select cascade", true);

                        this.For(p => p.SelectedIds3)
                           .SetDisplayName("CascadeDropdown")
                           .SetCascadingDropDown(m => m.SelectedIds2, action, "id", "Please select cascade 2", true);
                    });

        }

        #endregion

        #region Methods

        /// <summary>
        /// The get ids list.
        /// </summary>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        private IList<SelectListItem> GetIdsList()
        {
            var selectItems = new List<SelectListItem>();
            for (int i = 0; i < 10; i++)
            {
                selectItems.Add(new SelectListItem { Text = "value " + i, Value = i.ToString() });
            }

            selectItems.FirstOrDefault(i => i.Value.Equals("5", StringComparison.OrdinalIgnoreCase)).Selected = true;

            return selectItems;
        }

        #endregion
    }
}