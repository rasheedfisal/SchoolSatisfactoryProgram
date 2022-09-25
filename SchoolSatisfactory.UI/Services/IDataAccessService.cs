using SchoolSatisfactory.UI.Models;
using System.Security.Claims;

namespace SchoolSatisfactory.UI.Services
{
    public interface IDataAccessService
    {
        Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal);
        Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act);
        Task<bool> GetIdentityMenuItemsAsync(ClaimsPrincipal ctx, string PageName, string Area);
        Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string id);
        Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<Guid> permissionIds);
    }
}