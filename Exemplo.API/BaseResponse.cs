using System;
using System.Diagnostics.CodeAnalysis;

namespace Exemplo.API
{
    public class BaseResponse<T> where T : class
    {
        protected BaseResponse()
        {
            Result = default;
            ErrorMessages = Array.Empty<string>();
        }

        public string[] ErrorMessages { get; set; }
        public T Result { get; set; }

        public static BaseResponse<T> Error(params string[] error) => new BaseResponse<T>
        {
            ErrorMessages = (string[])error.Clone()
        };

        public static BaseResponse<T> Success([AllowNull] T result) => new BaseResponse<T>
        {
            Result = result,
            ErrorMessages = Array.Empty<string>()
        };
    }
}