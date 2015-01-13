using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    using FluentDataAnnotations;

    public class SetPasswordViewModelAnnotations : FluentDataAnnotation<SetPasswordViewModel> 
    {
        public SetPasswordViewModelAnnotations()
        {
            For(t => t.ConfirmPassword).SetDisplayName(() => "Fake Confirm Password");              
            For(t => t.NewPassword).SetDisplayName(() => "Fake New Password");
        }
    }
}