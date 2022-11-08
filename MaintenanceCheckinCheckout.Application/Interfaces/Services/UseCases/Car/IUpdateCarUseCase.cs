using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;
using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car
{
    public interface IUpdateCarUseCase
    {
        Task<Cars> Execute(CarUpdateRequest request);
    }
}
