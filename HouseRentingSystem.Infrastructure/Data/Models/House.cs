using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static HouseRentingSystem.Infrastructure.Constants.Validation;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
	[Comment("House for rent")]
	public class House
	{
		[Comment("House unique identifier")]
		[Key]
		public int Id { get; set; }

		[Comment("House title")]
		[Required]
		[MaxLength(HouseTitleMaxLength)]
		public string Title { get; set; } = string.Empty;

		[Comment("House address")]
		[Required]
		[MaxLength(HouseAddressMaxLength)]
		public string Address { get; set; } = string.Empty;

		[Comment("House description")]
		[Required]
		[MaxLength(HouseDescriptionMaxLength)]
		public string Description { get; set; } = string.Empty;

		[Comment("Image of the house")]
		[Required]
		[MaxLength(UrlMaxLength)]
		public string ImageURL { get; set; } = string.Empty;

		[Comment("Monthly price renting")]
		[Required]
		[Precision(Precision, Scale)]	
		public decimal PricePerMonth { get; set; }

		[Comment("Category unique identifier")]
		[Required]
		public int CategoryId { get; set; }
		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } = null!;

		[Comment("Agent unique identifier")]
		[Required]
		public int AgentId { get; set; }
		[ForeignKey(nameof(AgentId))]
		public Agent Agent { get; set; } = null!;

		[Comment("Rentar (IdentityUser) unique identifier")]
		public string? RenterId { get; set; }
		[ForeignKey(nameof(RenterId))]
		public IdentityUser? Renter { get; set; }
	}
}