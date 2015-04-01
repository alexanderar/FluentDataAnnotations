namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class TestModel
    {
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]

        public string NewPassword { get; set; }

        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }

        public int HiddenTest { get; set; }

        public DateTime Time { get; set; }

        public bool? NulableBoolean { get; set; }

        public int SelectedIds { get; set; }

        public IEnumerable<SelectListItem> Ids { get; set; }

        public string Phone { get; set; }

        public bool ApplyAnnotations { get; set; }
    }
}