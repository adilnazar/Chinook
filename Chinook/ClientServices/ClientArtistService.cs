using Chinook.ClientModels;
using Chinook.ClientServices.API;
using Chinook.ClientServices.Interfaces;
using Chinook.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Chinook.ClientServices
{
    public class ClientArtistService : IClientArtistService
    {
        private readonly IApiServices ApiServices;

        public ClientArtistService(IApiServices apiServices)
        {
            ApiServices = apiServices;
        }

        /// <summary>
        /// Get Artists list From Server
        /// </summary>
        /// <param name="search">The search term</param>
        /// <returns>A list of artists</returns>
        public async Task<List<ArtistModel>> GetArtistsAsync(string search)
        {
            return await ApiServices.GetDataAsync<List<ArtistModel>>($"api/artist?search={search}");
        }

        /// <summary>
        /// Get Artist detail by Id from Server
        /// </summary>
        /// <param name="artistId">The artist Id</param>
        /// <returns>An artist model</returns>
        public async Task<ArtistModel> GetArtistByIdAsync(long artistId)
        {
            return await ApiServices.GetDataAsync<ArtistModel>($"api/artist/{artistId}");
        }
    }
}
