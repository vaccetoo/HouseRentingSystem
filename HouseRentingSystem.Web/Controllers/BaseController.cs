using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Web.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{
	}
}
