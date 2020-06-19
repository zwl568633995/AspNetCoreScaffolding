using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kay.Framework.AspNetCore.Http
{
    /// <summary>
    /// 参考 Microsoft.AspNetCore.Mvc.ProblemDetails
    /// </summary>
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorDetails { get; set; }

        public ValidationResult[] ValidationErrors { get; set; }

        public ErrorResponse(
            string errorMessage,
            string errorDetails = null,
            string errorCode = null)
        {
            ErrorMessage = errorMessage;
            ErrorDetails = errorDetails;
            ErrorCode = errorCode;
        }

        public ErrorResponse(
            string errorMessage,
            string errorDetails = null,
            string errorCode = null,
            ValidationResult[] validationErrors = null)
        {
            ErrorMessage = errorMessage;
            ErrorDetails = errorDetails;
            ErrorCode = errorCode;
            ValidationErrors = validationErrors;
        }
    }
}
