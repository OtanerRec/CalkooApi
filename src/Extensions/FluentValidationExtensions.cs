using FluentValidation.Results;

namespace CalkooApi.Extensions
{
    /*This extension method converts FluentValidation.Results.ValidationResult to Dictionary like this. 
     * Otherwise we can’t return the Results.ValidationProblem from the API endpoint.*/
    public static class FluentValidationExtensions
    {
        public static IDictionary<string, string[]> ToDictionary(this ValidationResult validationResult) => 
            validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray());
    }
}
