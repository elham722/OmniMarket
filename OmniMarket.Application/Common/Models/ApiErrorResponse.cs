
namespace OmniMarket.Application.Common.Models
{
    public class ApiErrorResponse(string message, List<ErrorDetail> errors = null)
    {
        public bool Status { get; set; } = false;
        public string Message { get; set; } = message;
        public List<ErrorDetail> Errors { get; set; } = errors ?? new List<ErrorDetail>();
    }

    public class ErrorDetail(string propertyName, string errorMessage)
    {
        public string PropertyName { get; set; } = propertyName;
        public string ErrorMessage { get; set; } = errorMessage;
    }
}