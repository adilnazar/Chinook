﻿using Chinook.ClientModels;
using Chinook.ClientServices.Interfaces;
using Chinook.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Chinook.ClientServices
{
    public class ClientArtistService : IClientArtistService
    {
        private readonly HttpClient HttpClient;
        private readonly ILogger<ClientArtistService> Logger;

        public ClientArtistService(HttpClient httpClient, NavigationManager navigationManager, ILogger<ClientArtistService> logger)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(navigationManager.BaseUri);
            Logger = logger;
        }

        public async Task<List<ArtistModel>> GetArtistsAsync(string search)
        {
            try
            {
                var response = await HttpClient.GetAsync($"api/artist?search={search}");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                Logger.LogInformation("Response Content: {0}", responseContent);

                var artists = JsonSerializer.Deserialize<List<ArtistModel>>(responseContent);
                return artists ?? new List<ArtistModel>();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Request error.");
                return new List<ArtistModel>();
            }
        }
    }
}
