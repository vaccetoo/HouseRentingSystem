namespace System.Security.Claims
{
	public static class ClaimsPrincipalExtensons
	{
		public static string? Id(this ClaimsPrincipal user)
		{
			return user.FindFirstValue(ClaimTypes.NameIdentifier);
		}
		
	}
}
