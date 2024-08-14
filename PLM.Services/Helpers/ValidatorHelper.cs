namespace PLM.BusinessLogic.Helpers;
/// <summary>
/// Provides extension methods for model validation.
/// </summary>
internal static class ValidatorHelper
{
    /// <summary>
    /// Validates the specified model using data annotations.
    /// </summary>
    /// <param name="model">The model to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> object containing the validation result.</returns>
    internal static ValidationResult ValidateModel(this object model)
    {
        var validationContext = new ValidationContext(model, null, null);
        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

        return new ValidationResult
        {
            IsValid = isValid,
            ErrorMessages = validationResults.Select(r => r.ErrorMessage).ToList()
        };
    }
}

/// <summary>
/// Represents the result of a model validation.
/// </summary>
internal class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> ErrorMessages { get; set; } = [];
}