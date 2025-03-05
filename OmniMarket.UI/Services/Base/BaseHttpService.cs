
namespace OmniMarket.UI.Services.Base
{
    public class BaseHttpService(IClient client, ILocalStorageService localStorage)
    {
        protected ApiResponse<T> ConvertApiExceptions<T>(ApiException exception)
        {
            if (exception.StatusCode == 400)
            {
                return new ApiResponse<T>(false, "Validation errors have occurred.", default, exception.Response);
            }
            else if (exception.StatusCode == 401)
            {
                return new ApiResponse<T>(false, "Unauthorized access. Please log in.", default);
            }
            else if (exception.StatusCode == 403)
            {
                return new ApiResponse<T>(false, "Forbidden access. You don't have permission.", default);
            }
            else if (exception.StatusCode == 404)
            {
                return new ApiResponse<T>(false, "Not Found.", default);
            }
            else
            {
                return new ApiResponse<T>(false, "Something went wrong, please try again.", default);
            }
        }

        protected void AddBearerToken()
        {
            if (localStorage.Exists("token"))
            {
                client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", localStorage.GetStorageValue<string>("token"));
            }
        }
    }
}