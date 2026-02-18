using Microsoft.AspNetCore.Identity;

namespace TestWebApp.Models
{
    public class appUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}