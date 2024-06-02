using Chinook.ClientModels;
using Chinook.ClientServices.API;
using Chinook.ClientServices.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Chinook.ClientServices
{
    public class ClientPlaylistService : IClientPlaylistService
    {
        private readonly IApiServices ApiServices;
        private readonly IUserService UserService;
        private readonly NavigationManager NavigationManager;

        public ClientPlaylistService(IApiServices apiServices, IUserService userService, NavigationManager navigationManager)
        {
            ApiServices = apiServices;
            UserService = userService;
            NavigationManager = navigationManager;

        }

        /// <summary>
        /// Get Playlists list From Server
        /// </summary>
        /// <param name="search">The search term</param>
        /// <returns>A list of playlists</returns>
        public async Task<List<Playlist>> GetPlaylistsAsync()
        {
            var userId = await UserService.GetUserId();
            if (userId == null ) {
                NavigationManager.NavigateTo("/Identity/Account/Login");
                return new List<Playlist>();
            }
            return await ApiServices.GetDataAsync<List<Playlist>>($"api/playlist/{userId}");
        }

        /// <summary>
        /// Get Playlist detail by Id from Server
        /// </summary>
        /// <param name="playlistId">The playlist Id</param>
        /// <returns>An playlist model</returns>
        public async Task<Playlist> GetPlaylistByIdAsync(long playlistId)
        {
            var userId = await UserService.GetUserId();
            return await ApiServices.GetDataAsync<Playlist>($"api/playlist/GetPlaylistById/{userId}/{playlistId}");
        }
    }
}
