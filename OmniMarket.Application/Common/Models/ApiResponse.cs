// OmniMarket.Application/Common/Models/ApiResponse.cs
namespace OmniMarket.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string ValidationErrors { get; set; }
        public T Data { get; set; }

        // سازنده بدون پارامتر
        public ApiResponse()
        {
        }

        public ApiResponse(bool status, string message, T data, string validationErrors = null)
        {
            Status = status;
            Message = message;
            Data = data;
            ValidationErrors = validationErrors;
        }

        public static ApiResponse<T> Success(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> Error(string message, string validationErrors = null)
        {
            return new ApiResponse<T>(false, message, default, validationErrors);
        }
    }
}