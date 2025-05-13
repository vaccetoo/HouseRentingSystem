using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastructure.Constants.Validation;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
	[Comment("Agent")]
	public class Agent
	{
		[Comment("Agent unique identifier")]
		[Key]
		public int Id { get; set; }

		[Comment("Agent phone number")]
		[Required]
		[MaxLength(PhoneNumberMaxLength)]
		public string PhoneNumber { get; set; } = string.Empty;

		[Comment("IdentityUser identifier")]
		[Required]
		public string UserId { get; set; } = string.Empty;
		[ForeignKey(nameof(UserId))]
		public IdentityUser User { get; set; } = null!;
	}
}