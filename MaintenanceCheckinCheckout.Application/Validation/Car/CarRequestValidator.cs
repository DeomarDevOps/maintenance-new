using FluentValidation;
using MaintenanceCheckinCheckout.Application.ViewModels.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;
using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Application.Validation.Car
{
    public class CarRequestValidator : AbstractValidator<Cars>
    {
        public CarRequestValidator()
        {
            RuleFor(m => m.Plate).NotEmpty().WithMessage("Número da placa obrigatório");
            RuleFor(m => m.Description).NotEmpty().WithMessage("Descrição obrigatório");
        }
    }
}
