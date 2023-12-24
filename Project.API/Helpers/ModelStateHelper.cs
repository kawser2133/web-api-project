using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Project.API.Helpers
{
    public static class ModelStateHelper
    {
        public static string GetErrors(ModelStateDictionary modelState)
        {
            var errors = modelState
                .Where(e => e.Value.Errors.Any())
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => string.Join(", ", kvp.Value.Errors.Select(error => error.ErrorMessage))
                );

            return string.Join(", ", errors.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        }
    }

}
