using Kay.Framework.Exceptions.Common;
using Kay.Framework.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;

namespace Kay.Framework.Exceptions
{
    public class ValidationException : BaseException, IValidationErrors
    {
        public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        public IList<ValidationResult> ValidationErrors { get; } = new List<ValidationResult>();

        public ValidationException(string errorMessage, int errorNumber)
            : base(errorMessage, errorNumber)
        {

        }

        public ValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            ValidationErrors = new List<ValidationResult>();
        }

        public ValidationException(string errorMessage)
            : base(errorMessage)
        {
            ValidationErrors = new List<ValidationResult>();
        }

        public ValidationException(string errorMessage, IList<ValidationResult> validationErrors)
            : base(errorMessage)
        {
            ValidationErrors = validationErrors;
        }

        public ValidationException(string message, int errorNumber, IList<ValidationResult> validationErrors)
            : base(message, errorNumber)
        {
            ValidationErrors = validationErrors;
        }


        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<ValidationResult>();
        }
    }
}
