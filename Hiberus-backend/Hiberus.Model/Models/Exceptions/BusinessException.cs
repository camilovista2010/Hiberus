

namespace Hiberus.Model.Models.Exceptions
{
    public class BusinessException: Exception
    {       
        public const string RESOURCE_NOT_FOUND = "RESOURCE_NOT_FOUND";
        public const string ERROR_PARAMETERS = "ERROR_PARAMETERS";
        public const string UNEXPECTED_ERROR_CODE = "UNEXPECTED_ERROR_CODE";
        public const string TOKEN_ERROR_CODE = "TOKEN_ERROR_CODE";
        public const string FORBIDDEN_RESOURCE_CODE = "FORBIDDEN_RESOURCE_CODE";

        public BusinessException(string code)
        {
            this.code = code;
        }

        public BusinessException(string code, string message)
            : base(message)
        {
            this.code = code;
        }

        public BusinessException(string code, string message, Exception inner)
            : base(message, inner)
        {
            this.code = code;
        }

        public string code { get; set; }
        
    }
}
