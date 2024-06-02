using Chinook.Infrastructure.Contracts;

namespace Chinook.ClientServices.Interfaces
{
    public interface IUserService : IBase
    {
        /// <summary>
        /// Retrieves the unique identifier of the currently authenticated user.
        /// </summary>
        /// <returns></returns>
        Task<string> GetUserId();
    }
}
