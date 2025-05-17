using HouseRentingSystem.Core.Models.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Controllers
{
	public class AgentController : BaseController
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var model = new BecomeAgentFormModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Index(BecomeAgentFormModel model)
		{
			return RedirectToAction(nameof(HouseController.Index), "House");
		}
	}
}
