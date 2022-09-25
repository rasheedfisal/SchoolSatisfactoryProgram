using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolSatisfactory.UI.Services;

namespace SchoolSatisfactory.UI.Handlers
{
    public class AuthorizationRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }
        public AuthorizationRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }

    }
    public class PermissionHandler : AuthorizationHandler<AuthorizationRequirement>
    {
        private readonly IDataAccessService _dataAccessService;

        public PermissionHandler(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext? context, AuthorizationRequirement? requirement)
        {
            if (context?.Resource is HttpContext ctx)
            {
                //var ctx = context.Resource as HttpContext;
                var _controller = ctx.GetRouteValue("controller");
                var _action = ctx.GetRouteValue("action");
                var _page = ctx.GetRouteValue("page");
                var _area = ctx.GetRouteValue("area");

                //asp.net 3.1 old
                //httpContext.RoutePattern.RequiredValues.TryGetValue("controller", out var _controller);
                //httpContext.RoutePattern.RequiredValues.TryGetValue("action", out var _action);

                //httpContext.RoutePattern.RequiredValues.TryGetValue("page", out var _page);
                //httpContext.RoutePattern.RequiredValues.TryGetValue("area", out var _area);

                // Check if a parent action is permitted then it'll allow child without checking for child permissions
                if (!string.IsNullOrWhiteSpace(requirement?.PermissionName) && !requirement.PermissionName.Equals("Authorization"))
                {
                    _action = requirement.PermissionName;
                }

                if (context.User.Identity.IsAuthenticated && _controller != null && _action != null && _page == null &&
                    await _dataAccessService.GetMenuItemsAsync(context.User, _controller.ToString(), _action.ToString()))
                {
                    context?.Succeed(requirement);
                }
                if (context.User.Identity.IsAuthenticated && _page != null &&
                    await _dataAccessService.GetIdentityMenuItemsAsync(context.User, _page.ToString(), _area.ToString()))
                {
                    context.Succeed(requirement);
                }
            }

            await Task.CompletedTask;
        }
    }
}
