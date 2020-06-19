using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = Kay.Framework.Exceptions.ValidationException;

namespace Kay.Framework.AspNetCore.Mvc.Attributes
{
  public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<ValidationResult>();
                foreach (var state in context.ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(new ValidationResult(error.ErrorMessage, new[] {state.Key}));
                    }
                }

                throw new ValidationException("Invalid Request!", errors);
            }
        }
    }
}
