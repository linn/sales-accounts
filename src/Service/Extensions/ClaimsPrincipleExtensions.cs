namespace Linn.SalesAccounts.Service.Extensions
{
    using System.Linq;
    using System.Security.Claims;

    public static class ClaimsPrincipleExtensions
    {
        public static string GetEmployeeUri(this ClaimsPrincipal principal)
        {
            return principal?.Claims?.FirstOrDefault(c => c.Type == "employee")?.Value;
        }
    }
}
