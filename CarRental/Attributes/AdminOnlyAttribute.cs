using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CarRental.Models;

namespace CarRental.Attributes
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var role = session.GetString("AdminRole");
            
            if (string.IsNullOrEmpty(role) || role != UserRole.Admin.ToString())
            {
                context.Result = new RedirectToActionResult("Login", "Admin", null);
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}


