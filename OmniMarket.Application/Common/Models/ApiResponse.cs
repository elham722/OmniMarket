
namespace OmniMarket.Application.Common.Models
{
    public class ApiResponse<T>(bool status, string message, T data)
    {
        public bool Status { get; set; } = status;
        public string Message { get; set; } = message;
        public T Data { get; set; } = data;

        public static ApiResponse<T> Success(T data, string message = "درخواست با موفقیت انجام شد")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> Error(string message, T data = default)
        {
            return new ApiResponse<T>(false, message, data);
        }
    }
}
