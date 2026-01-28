using KlantenDienstData.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
namespace KlantenDienstWeb.Security;
public sealed class SecurityManager
{
    public async Task SignIn(HttpContext httpContext, Personeelslid personeelslid, bool isCookiePersistent)
    {
        ClaimsIdentity claimsIdentity = new(GetUserClaims(personeelslid), CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
        if (isCookiePersistent)
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            { ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60), IsPersistent = true });
        else
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    }
    public async Task SignOut(HttpContext httpContext)
    {
        await httpContext.SignOutAsync();
    }
    private IEnumerable<Claim> GetUserClaims(Personeelslid personeelslid)
    {
        List<Claim> claims = [ new Claim(ClaimTypes.Name, string.Join(' ', personeelslid.Voornaam, personeelslid.Familienaam)),
            new Claim("PersoneelslidId", personeelslid.PersoneelslidId.ToString()),
            new Claim("PersoneelslidAccountId", personeelslid.PersoneelslidAccountId.ToString()),
            new Claim("PersoneelslidId", personeelslid.PersoneelslidAccountId.ToString())
        ];
        return claims;
    }
}