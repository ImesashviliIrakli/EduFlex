using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Identity.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        var hasher = new PasswordHasher<IdentityUser>();
        builder.HasData(
             new IdentityUser
             {
                 Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                 Email = "admin@localhost.com",
                 NormalizedEmail = "ADMIN@LOCALHOST.COM",
                 UserName = "admin@localhost.com",
                 NormalizedUserName = "ADMIN@LOCALHOST.COM",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                 EmailConfirmed = true
             }
        );
    }
}

