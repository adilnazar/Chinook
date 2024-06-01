using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Infrastructure.Contracts.Services;
using Chinook.Models;
using NuGet.Protocol.Core.Types;

namespace Chinook.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository ArtistRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public ArtistService(IArtistRepository repository)
        {
            ArtistRepository = repository;
        }

        /// <summary>
        /// Get All Artist Avaiable for search string provided if not retrive all
        /// </summary>
        /// <param name="artistSearchString"></param>
        /// <returns></returns>
        public async Task<List<Artist>> GetArtistsAsync(string artistSearchString)
        {
            return await ArtistRepository.GetArtistsAsync(artistSearchString);
        }

        /// <summary>
        /// Get Artist Details by Id
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <returns></returns>
        public async Task<Artist> GetArtistByIdAsync(long artistId) => await ArtistRepository.GetArtistByIdAsync(artistId);
    }
}
