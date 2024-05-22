using System.ComponentModel.DataAnnotations;

public class RequireBothOrNoneAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public RequireBothOrNoneAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = value as IFormFile;
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
            throw new ArgumentException("Property with this name not found");

        var comparisonValue = property.GetValue(validationContext.ObjectInstance) as IFormFile;

        if ((currentValue == null && comparisonValue != null) || (currentValue != null && comparisonValue == null))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}

