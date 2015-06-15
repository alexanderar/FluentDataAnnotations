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

        public double Double { get; set; }

        public bool? NulableBoolean { get; set; }

        public int SelectedIds { get; set; }

        public int SelectedIds2 { get; set; }

        public int SelectedIds3 { get; set; }

        public string Country { get; set; }

        public IList<SelectListItem> Countries{ get; set; }

        public string State { get; set; }

        public IList<SelectListItem> Ids { get; set; }

        public TestEnum EnumerableEnum { get; set; }

        public IList<TestEnum> EnumerableEnums { get; set; }

        public string Phone { get; set; }

        public bool ApplyAnnotations { get; set; }
    }
}