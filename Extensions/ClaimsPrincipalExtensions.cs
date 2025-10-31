using System.Security.Claims;

namespace AlfabetizaFeso.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int? GetUserId(this ClaimsPrincipal? user)
    {
        var id = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(id, out var parsed) ? parsed : null;
    }
}