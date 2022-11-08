using FluentValidation.Results;

namespace MaintenanceCheckinCheckout.Application.Interfaces
{
    public interface IMinimalValidator
    {
        ValidationResult Validate<T>(T model);
    }
}
