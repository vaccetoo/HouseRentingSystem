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
			string? userId = User.Id();

			if (userId == null)
			{
				return Unauthorized();
			}

			IEnumerable<HouseServiceModel> model;

			if (await _agentService.ExcistByIdAsync(userId))
			{
				int agentId = await _agentService.GetAgentIdAsync(userId) ?? 0;

				model = await _houseService.AllHousesByAgentIdAsync(agentId);
			}
			else
			{
				model = await _houseService.AllHousesByUserIdAsync(userId);
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			if(!await _houseService.ExcistByIdAsync(id))
			{
				return BadRequest();
			}

			var model = await _houseService.DetailsByIdAsync(id);

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
			if(!await _houseService.ExcistByIdAsync(id))
			{
				return BadRequest();
			}

			if(!await _houseService.HasAgentWithIdAsync(id, User.Id()))
			{
				return Unauthorized();
			}

			var model = await _houseService.GetHouseFormModelByIdAsync(id);

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, HouseFormModel model)
		{
			if (!await _houseService.ExcistByIdAsync(id))
			{
				return BadRequest();
			}

			if (!await _houseService.HasAgentWithIdAsync(id, User.Id()))
			{
				return Unauthorized();
			}

			if (!await _houseService.CategoryExcistAsync(model.CategoryId))
			{
				ModelState.AddModelError(nameof(model.CategoryId), "Wrong Category");
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await _houseService.AllCategoriesAsync();

				return View(model);
			}

			await _houseService.EditAsync(id, model);

			return RedirectToAction(nameof(Details), new { id = id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			if(!await _houseService.ExcistByIdAsync(id))
			{
				return BadRequest();
			}

			if (!await _houseService.HasAgentWithIdAsync(id, User.Id()))
			{
				return Unauthorized();
			}

			var house = await _houseService.DetailsByIdAsync(id);

			var model = new HouseDetailsViewModel()
			{
				Id = house.Id,
				Address = house.Address,
				ImageURL = house.ImageURL,
				Title = house.Title,
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(HouseDetailsServiceModel model)
		{
			if (!await _houseService.ExcistByIdAsync(model.Id))
			{
				return BadRequest();
			}

			if (!await _houseService.HasAgentWithIdAsync(model.Id, User.Id()))
			{
				return Unauthorized();
			}

			await _houseService.DeleteAsync(model.Id);

			return RedirectToAction(nameof(Index));
		}

		[HttpPost] 
		public async Task<IActionResult> Rent(int id)
		{
			if (!await _houseService.ExcistByIdAsync(id))
			{
				return BadRequest();
			}

			if (await _agentService.ExcistByIdAsync(User.Id()))
			{
				return Unauthorized();
			}

			if (await _houseService.IsRentedAsync(id))
			{
				return BadRequest();
			}

			await _houseService.RentAsync(id, User.Id());

			return RedirectToAction(nameof(Index));	
		}

		[HttpPost]
		public async Task<IActionResult> Leave(int id)
		{
			if (!await _houseService.ExcistByIdAsync(id))
			{
				return BadRequest();
			}

			if (!await _houseService.IsRentedByUserWithIdAsync(id, User.Id()))
			{
				return Unauthorized();
			}

			await _houseService.LeaveAsync(id);

			return RedirectToAction(nameof(Index));
		}
	}
}
