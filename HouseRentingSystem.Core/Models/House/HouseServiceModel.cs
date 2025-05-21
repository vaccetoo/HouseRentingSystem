using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Constants.Validation;
using static HouseRentingSystem.Infrastructure.Constants.Messages;

namespace HouseRentingSystem.Core.Models.House
{
	public class HouseServiceModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = ReqiredMessage)]
		[StringLength(HouseTitleMaxLength,
			MinimumLength = HouseTitleMinLength,
			ErrorMessage = StringLengthMessage)]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = ReqiredMessage)]
		[StringLength(HouseAddressMaxLength,
			MinimumLength = HouseAddressMinLength,
			ErrorMessage = StringLengthMessage)]
		public string Address { get; set; } = string.Empty;

		[Required(ErrorMessage = ReqiredMessage)]
		[StringLength(UrlMaxLength,
			ErrorMessage = StringLengthMessage)]
		[Display(Name = "Image URL")]
		public string ImageURL { get; set; } = string.Empty;

		[Required(ErrorMessage = ReqiredMessage)]
		[Range(MinPricePerMonth,
			MaxPricePerMonth,
			ConvertValueInInvariantCulture = true,
			ErrorMessage = PriceErrorMessage)]
		[Display(Name = "Price per month")]
		public decimal PricePerMonth { get; set; }

		[Display(Name = "Is rented")]
		public bool IsRented { get; set; }	
	}
}
