using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web.Mvc;

    using FluentDataAnnotations;

    public class ChangePasswordViewModelAnnotations : FluentDataAnnotation<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelAnnotations()
        {
            For(x => x.ConfirmPassword).SetDisplayName("Confirm Password")
                .SetReadOnly(
                () => !Thread.CurrentPrincipal.Identity.Name.Equals(
                    "alex.art84@gmail.com",
                    StringComparison.OrdinalIgnoreCase))
                .SetShowForEdit(() => true)
                .SetShowForDisplay(() => true)
                .SetDisplayFormat("Custom format {0}", true);

            For(x => x.HiddenTest).SetHiddenInput();

            For(x => x.Time).SetDisplayFormat("{0:MM.dd.yyyy hh:mm:ss}", true).SetReadOnly(true, false);

            For(x => x.NulableBoolean).SetReadOnly(true);

            For(m => m.Phone).SetReadOnly(true)
                .SetDisplayName("Phone").ApplyValueTransform((s) => Regex.Replace(s, @"(?<=\d{1})\d(?=\d{3})", "*", RegexOptions.Compiled));

            For(x => x.OldPassword).SetDisplayName(() => "Fluent Old Password (Function)").SetShowForEdit(() => true)
                .SetShowForDisplay(() => true)
                .SetDataType(System.ComponentModel.DataAnnotations.DataType.Password);

            For(x => x.NewPassword)
                .SetDisplayName(Resource.TestAnnotation)
                .SetReadOnly(
                () => Thread.CurrentPrincipal.Identity.Name.Equals(
                    "alex.art84@gmail.com",
                    StringComparison.OrdinalIgnoreCase))
                .SetShowForEdit(() => true)
                .SetShowForDisplay(() => true);

            For(p => p.SelectedIds).SetDisplayName("Dropdown").SetDropDown(GetIdsList).SetReadOnly(
                () => Thread.CurrentPrincipal.Identity.Name.Equals(
                    "alex.art84@gmail.com",
                    StringComparison.OrdinalIgnoreCase));
        }

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
    }
}