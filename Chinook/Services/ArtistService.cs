using Chinook.ClientModels;
using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Infrastructure.Contracts.Services;

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
        public async Task<List<ArtistModel>> GetArtistsAsync(string artistSearchString)
        {
            try
            {
                return await ArtistRepository.GetArtistsAsync(artistSearchString);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Artist Details by Id
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <returns></returns>
        public async Task<ArtistModel> GetArtistByIdAsync(long artistId)
        {
            try
            {
                return await ArtistRepository.GetArtistByIdAsync(artistId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
