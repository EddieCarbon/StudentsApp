

using System.Security.Claims;
using System.Text.Json;

namespace StudentsApp.Client
{
	public class CustomAuthStateProvider : AuthenticationStateProvider
	{
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImVmNDBlYzk4LWUzODItNDkzNS1hMzk3LTFiYWQ4NDA3ZGM5YSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJrdWJhIiwianRpIjoiMjRkZmY0YTYtNTk3MC00ODg0LWE0YzktZjljYjEzNDU3YTAyIiwiZXhwIjoxNjk3MDQ3OTc3fQ.nE9QHDJKNMpEzwQ63RXEY_nYMqxI0JW2vxbBde1vkcM";

			var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
			//var identity = new ClaimsIdentity();

			var user = new ClaimsPrincipal(identity);
			var state = new AuthenticationState(user);

			NotifyAuthenticationStateChanged(Task.FromResult(state));
			
			return state;
		}

		public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
			return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
		}

		private static byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}
	}
}
