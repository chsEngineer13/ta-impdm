using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service
{
    public class Result
    {
        public enum ErrorCode { None, HttpCanRetry, HttpFail, Exception };

        public bool Success { get; }
        public ErrorCode ErrorMessageCode { get; }
        public string ErrorMessage { get; }

        private Result(bool success, ErrorCode errorCode = Result.ErrorCode.None, string error = "")
        {
            this.Success = success;
            this.ErrorMessageCode = errorCode;
            this.ErrorMessage = error;
        }

        public static Result OK()
        {
            return new Result(true);
        }

        public static Result Error(ErrorCode errorCode, string errorMessage)
        {
            if (errorMessage == null)
                throw new ArgumentNullException(nameof(errorMessage));

            return new Result(false, errorCode, errorMessage);
        }
    }
}
