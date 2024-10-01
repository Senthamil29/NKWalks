using Microsoft.AspNetCore.Identity;

namespace NKWalks.API.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
