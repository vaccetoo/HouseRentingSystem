using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static HouseRentingSystem.Infrastructure.Constants.Messages;

namespace HouseRentingSystem.Web.Controllers
{
	public class AgentController : BaseController
	{
		private readonly IAgentService _agentService;

		public AgentController(IAgentService agentService)
		{
			_agentService = agentService;
		}

		[HttpGet]
		[NotAnAgent]
		public IActionResult Index()
		{
			var model = new BecomeAgentFormModel();

			return View(model);
		}

		[HttpPost]
		[NotAnAgent]
		public async Task<IActionResult> Index(BecomeAgentFormModel model)
		{
			if (await _agentService.ExcistByPhoneNumberAsync(model.PhoneNumber))
			{
				ModelState.AddModelError(model.PhoneNumber, PhoneNumberErrorMessage);
			}

			if (await _agentService.HasRentsAsync(User.Id()))
			{
				ModelState.AddModelError("Error", AgentRentsErrorMessage);
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			await _agentService.CreateAsync(User.Id(), model.PhoneNumber);

			return RedirectToAction(nameof(HouseController.Index), "House");
		}
	}
}
