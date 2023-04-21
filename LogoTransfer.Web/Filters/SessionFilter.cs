using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LogoTransfer.Web.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("CurrentUser") == null)
            {
                filterContext.Result = new RedirectToActionResult("LogIn", "Account", null);
            }
        }
    }
}
