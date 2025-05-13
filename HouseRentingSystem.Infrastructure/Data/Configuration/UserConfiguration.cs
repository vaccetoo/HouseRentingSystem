using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystem.Infrastructure.Data.Configuration
{
	internal class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
	{
		public void Configure(EntityTypeBuilder<IdentityUser> builder)
		{
			var data = new SeedDataBase();

			builder.HasData(new List<IdentityUser>() 
			{ 
				data.AgentUser, 
				data.GuestUser
			});
		}
	}
}
