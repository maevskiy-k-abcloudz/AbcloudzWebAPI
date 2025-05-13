using System.ComponentModel.DataAnnotations;
using AbcloudzWebAPI.Contracts.Exceptions;

namespace AbcloudzWebAPI.Infrastructure.Attributes;

public class RequiredNonEmptyAttribute : ValidationAttribute
{
    public RequiredNonEmptyAttribute()
    {
        ErrorMessage = "{0} is required and cannot be empty or default.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var propertyName = validationContext.DisplayName ?? validationContext.MemberName;

        if (value == null)
        {
            throw new BusinessException(string.Format(ErrorMessage, propertyName));
        }

        if (value is string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new BusinessException(string.Format(ErrorMessage, propertyName));
            }
        }
        else
        {
            var type = value.GetType();
            var defaultValue = type.IsValueType ? Activator.CreateInstance(type) : null;
            if (Equals(value, defaultValue))
            {
                throw new BusinessException(string.Format(ErrorMessage, propertyName));
            }
        }

        return ValidationResult.Success;
    }
}