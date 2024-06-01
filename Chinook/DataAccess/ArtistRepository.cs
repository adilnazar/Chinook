using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Chinook.ClientModels;
using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Models;


namespace Chinook.DataAccess
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ChinookContext DbContext;
        private readonly IMapper Mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory"></param>
        public ArtistRepository(IDbContextFactory<ChinookContext> dbFactory, IMapper mapper)
        {
            DbContext = dbFactory.CreateDbContext();
            Mapper = mapper;
        }

        //Get All Artist Avaiable for search string provided if not retrive all
        public async Task<List<ArtistModel>> GetArtistsAsync(string artistSearchString)
        {
            try
            {
                IQueryable<Artist> query = DbContext.Artists.Include(x => x.Albums).OrderBy(x => x.Name);
                if (!string.IsNullOrEmpty(artistSearchString))
                {
                    query = query.Where(x => x.Name != null && x.Name.ToLower().Contains(artistSearchString.ToLower()));
                }

                var artists = await query.ToListAsync();

                return Mapper.Map<List<ArtistModel>>(artists);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// Get Artist Details by Id
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <returns></returns>
        public async Task<ArtistModel> GetArtistByIdAsync(long ArtistId)
        {
            var artist = await DbContext.Artists.SingleOrDefaultAsync(a => a.ArtistId == ArtistId);
            return Mapper.Map<ArtistModel>(artist);
        }

    }
}
