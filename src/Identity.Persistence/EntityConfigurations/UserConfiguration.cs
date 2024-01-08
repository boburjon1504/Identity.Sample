using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User
        {
            Id = Guid.NewGuid(),
            Email = "boburjon67joraboyev@gmail.com",
            FirsName = "Bobur",
            LastName = "Joraboyev",
            RoleId = Guid.Parse("6d3503ab-1a35-47b9-be09-b24ff4fbf6bf"),
            Password="boburjon6767"
        });
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
    }
}
