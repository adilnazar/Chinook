namespace Chinook.ClientServices.API
{
    public interface IApiServices
    {
        Task<T> GetDataAsync<T>(string url);
        Task<TResponse> PostDataAsync<TRequest, TResponse>(string url, TRequest requestData);
    }
}
