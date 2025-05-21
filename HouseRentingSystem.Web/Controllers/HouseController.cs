using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentingSystem.Web.Controllers
{
	public class HouseController : BaseController
	{
		private readonly IHouseService _houseService;
		private readonly IAgentService _agentService;

		public HouseController(IHouseService houseService,
			IAgentService agentService)
		{
			_houseService = houseService;
			_agentService = agentService;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index(AllHousesQueryModel queryModel)
		{
			var serviceModel = await _houseService.AllAsync(queryModel);

			queryModel.Houses = serviceModel.Houses;
			queryModel.TotalHousesCount = serviceModel.TotalHousesCount;
			queryModel.Categories = await _houseService.AllCategoriesNamesAsync();

			return View(queryModel);
		}

		[HttpGet]
		public async Task<IActionResult> Mine()
		{
			var model = new AllHousesQueryModel();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var model = new HouseDetailsViewModel();

			return View(model);
		}

		[HttpGet]
		[MustBeAnAgent]
		public async Task<IActionResult> Add()
		{
			var model = new HouseFormModel()
			{
				Categories = await _houseService.AllCategoriesAsync()
			};

			return View(model);
		}

		[HttpPost]
		[MustBeAnAgent]
		public async Task<IActionResult> Add(HouseFormModel model)
		{
			if (!await _houseService.CategoryExcistAsync(model.CategoryId))
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Wrong Category");
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await _houseService.AllCategoriesAsync();
				return View(model);
			}

			string? userId = User.Id();

			if (userId == null)
			{
				return Unauthorized();
			}

			int? agentId = await _agentService.GetAgentIdAsync(userId);

			int newHouseId = await _houseService.CreateAsync(model, agentId ?? 0);

			return RedirectToAction(nameof(Details), new { id = newHouseId });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var model = new HouseFormModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, HouseFormModel model)
		{
			return RedirectToAction(nameof(Details), new { id = 1 });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var model = new HouseDetailsViewModel();

			return View(model);	
		}

		[HttpPost]
		public async Task<IActionResult> Delete(HouseDetailsViewModel model)
		{
			return RedirectToAction(nameof(Index));
		}

		[HttpPost] 
		public async Task<IActionResult> Rent(int id)
		{
			return RedirectToAction(nameof(Mine));
		}

		[HttpPost]
		public async Task<IActionResult> Leave(int id)
		{
			return RedirectToAction(nameof(Mine));
		}
	}
}
