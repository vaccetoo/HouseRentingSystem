using HouseRentingSystem.Core.Enumerations;

namespace HouseRentingSystem.Core.Models.House
{
	public class AllHousesQueryModel
	{
		public string? Category { get; set; } = null;

		public string? SearchTerm { get; set; } = null!;

		public HouseSorting Sorting { get; set; } = HouseSorting.Newest;

		public int CurrentPage { get; set; } = 1;

		public int HousesPerPage { get; set; } = 3;

		public int TotalHousesCount { get; set; }

		public IEnumerable<string> Categories { get; set; } 
			= new List<string>();

		public IEnumerable<HouseServiceModel> Houses { get; set; } 
			= new List<HouseServiceModel>();
	}
}
