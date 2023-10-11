
using System.Security.Claims;

namespace WebClient
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImVmNDBlYzk4LWUzODItNDkzNS1hMzk3LTFiYWQ4NDA3ZGM5YSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJrdWJhIiwianRpIjoiODlmNmNkZTctMDQzNi00MTc1LWIxZDgtMDhlYzdmNWVmYWRmIiwiZXhwIjoxNjk3MDQ0NjcyfQ.gfNrbSjjrzB9TGdioYGcA4SW0WC5UwxXc26YGIy6O48";
        
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
