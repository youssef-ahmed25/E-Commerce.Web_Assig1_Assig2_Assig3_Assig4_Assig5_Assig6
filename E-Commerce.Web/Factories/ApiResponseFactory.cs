using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {

           var errors = context.ModelState
               .Where(m => m.Value.Errors.Any())
               .Select(m => new ValidationError()
               {
                   Field = m.Key,
                   Errors = m.Value.Errors.Select(e => e.ErrorMessage)
               });
           var response = new ValidationErrorToReturn()
           {
                ValidationErrors = errors
           };

            return new BadRequestObjectResult(response);

        }
    }
}
