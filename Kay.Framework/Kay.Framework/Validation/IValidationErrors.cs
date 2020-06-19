using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kay.Framework.Validation
{
    interface IValidationErrors
    {
        IList<ValidationResult> ValidationErrors { get; }
    }
}
