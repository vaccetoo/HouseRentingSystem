namespace HouseRentingSystem.Core.Models.House
{
	public class HouseDetailsViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;

		public string ImageURL { get; set; } = string.Empty;
	}
}
