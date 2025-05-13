namespace HouseRentingSystem.Infrastructure.Constants
{
	public static class Validation
	{
		// Category validation constants
		public const int CategoryNameMinLength = 1;
		public const int CategoryNameMaxLength = 50;

		// House validation constants
		public const int HouseTitleMinLength = 10;
		public const int HouseTitleMaxLength = 50;

		public const int HouseAddressMinLength = 30;
		public const int HouseAddressMaxLength = 150;

		public const int HouseDescriptionMinLength = 50;
		public const int HouseDescriptionMaxLength = 500;

		public const int UrlMaxLength = 1250;

		public const decimal MinPricePerMonth = 0M;
		public const decimal MaxPricePerMonth = 2000M;

		// Common validation constants
		public const int Precision = 18;
		public const int Scale = 2;

		public const int PhoneNumberMinLength = 7;
		public const int PhoneNumberMaxLength = 15;
	}
}
