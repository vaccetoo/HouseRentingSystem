using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace HouseRentingSystem.Web.Attributes
{
	public class MustBeAnAgentAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			IAgentService? agentService = context.HttpContext.RequestServices.GetService<IAgentService>();

			if (agentService == null)
			{
				context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}

			if (agentService != null &&
				!agentService.ExcistByIdAsync(context.HttpContext.User.Id()).Result)
			{
				context.Result = new RedirectToActionResult(nameof(AgentController.Index), "Agent", null);
			}

			base.OnActionExecuting(context);
		}
	}
}
