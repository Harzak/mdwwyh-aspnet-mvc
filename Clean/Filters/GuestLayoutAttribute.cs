using System.Web.Mvc;

namespace Clean.Filters
{
    public sealed class GuestLayoutAttribute : ActionFilterAttribute
    {
        private const string GuestLayout = "~/Views/Shared/_LayoutGuest.cshtml";

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                return;
            }

            ViewResult viewResult = filterContext.Result as ViewResult;
            if (viewResult != null)
            {
                viewResult.MasterName = GuestLayout;
            }
        }
    }
}
