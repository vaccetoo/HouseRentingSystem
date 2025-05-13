using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Constants.Validation;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
	[Comment("House category")]
	public class Category
	{
		[Comment("Category unique identifier")]
		[Key]
		public int Id { get; set; }

		[Comment("Category name")]
		[Required]
		[MaxLength(CategoryNameMaxLength)]	
		public string Name { get; set; } = string.Empty;

		public IEnumerable<House> Houses { get; set; } = new List<House>();
	}
}
