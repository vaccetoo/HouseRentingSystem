using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentingSystem.Infrastructure.Data.Configuration
{
	internal class AgentConfiguration : IEntityTypeConfiguration<Agent>
	{
		public void Configure(EntityTypeBuilder<Agent> builder)
		{
			var data = new SeedDataBase();

			builder.HasData(data.Agent);	
		}
	}
}
