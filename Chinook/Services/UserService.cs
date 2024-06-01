using Chinook.Infrastructure.Contracts.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Chinook.Services
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider AuthenticationStateProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authenticationStateProvider"></param>
        public UserService(AuthenticationStateProvider authenticationStateProvider)
        {
            AuthenticationStateProvider = authenticationStateProvider;
        }

        /// <summary>
        /// Retrieves the unique identifier of the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirst(u => u.Type.Contains(ClaimTypes.NameIdentifier))?.Value;
            return userId;
        }
    }
}
