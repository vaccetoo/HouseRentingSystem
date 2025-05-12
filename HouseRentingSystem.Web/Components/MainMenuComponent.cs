using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Components
{
	public class MainMenuComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return await Task.FromResult<IViewComponentResult>(View());
		}
	}
}
