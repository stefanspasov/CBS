namespace CBS.Logic.Filters
{
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    internal class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // TODO Extension point: Authorization could be implemented here. E.g. suitable for FinalizeReservation call. 
        }
    }
}
