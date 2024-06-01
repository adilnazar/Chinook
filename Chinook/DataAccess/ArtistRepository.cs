using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.DataAccess
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ChinookContext DbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory"></param>
        public ArtistRepository(IDbContextFactory<ChinookContext> dbFactory)
        {
            DbContext = dbFactory.CreateDbContext();
        }

        //Get All Artist Avaiable for search string provided if not retrive all
        public async Task<List<Artist>> GetArtistsAsync(string artistSearchString)
        {
            if (string.IsNullOrEmpty(artistSearchString))
            {
                return await DbContext.Artists.Include(x => x.Albums).OrderBy(x=>x.Name).ToListAsync();
            }
            else
            {
                return await DbContext.Artists.Include(x => x.Albums).Where(x => x.Name != null && x.Name.ToLower().Contains(artistSearchString.ToLower())).OrderBy(x => x.Name).ToListAsync();
            }

        }

        /// <summary>
        /// Get Artist Details by Id
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <returns></returns>
        public async Task<Artist> GetArtistByIdAsync(long ArtistId) => await DbContext.Artists.SingleOrDefaultAsync(a => a.ArtistId == ArtistId);

    }
}
