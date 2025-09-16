using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CarRental.Models;

namespace CarRental.Attributes
{
    public class CustomerOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var role = session.GetString("CustomerRole");
            
            if (string.IsNullOrEmpty(role) || role != UserRole.Customer.ToString())
            {
                context.Result = new RedirectToActionResult("Login", "Customer", null);
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}


