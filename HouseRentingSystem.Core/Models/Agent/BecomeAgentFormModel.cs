using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Constants.Validation;
using static HouseRentingSystem.Infrastructure.Constants.Messages;

namespace HouseRentingSystem.Core.Models.Agent
{
	public class BecomeAgentFormModel
	{
		[Required(ErrorMessage = ReqiredMessage)]
		[StringLength(PhoneNumberMaxLength, 
			MinimumLength = PhoneNumberMinLength, 
			ErrorMessage = StringLengthMessage)]
		[Display(Name = "Phone Number")]
		[Phone]
		public string PhoneNumber { get; set; } = null!;
	}
}
