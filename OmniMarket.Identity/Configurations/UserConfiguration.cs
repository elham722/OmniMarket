
namespace OmniMarket.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
           
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired() 
                .HasMaxLength(50); 

            // Seed Data
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "05446344-f9cc-4566-bd2c-36791b4e28ed",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    FirstName = "Elham",
                    LastName = "Heydari",
                    UserName = "elham72",
                    NormalizedUserName = "ELHAM72",
                    PasswordHash = hasher.HashPassword(null, "H@di1234"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "2ec9f480-7288-4d0f-a1cd-53cc89968b45",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "User",
                    UserName = "user@localhost.com",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                }
            );
        }
    }
}