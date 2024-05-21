using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Identity.Configuration;

public class UserClaimsConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.HasData(
            new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                ClaimType = ClaimTypes.Email,
                ClaimValue = "admin@localhost.com"
            },
            new IdentityUserClaim<string>
            {
                Id = 2,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                ClaimType = ClaimTypes.Name,
                ClaimValue = "admin@localhost.com"
            },
            new IdentityUserClaim<string>
            {
                Id = 3,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                ClaimType = ClaimTypes.NameIdentifier,
                ClaimValue = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            },
            new IdentityUserClaim<string>
            {
                Id = 4,
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                ClaimType = ClaimTypes.Role,
                ClaimValue = "Admin"
            }
        );
    }
}
