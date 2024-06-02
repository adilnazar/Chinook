namespace Chinook.ClientServices.API
{
    public interface IApiServices
    {
        Task<T> GetDataAsync<T>(string url);
    }
}
