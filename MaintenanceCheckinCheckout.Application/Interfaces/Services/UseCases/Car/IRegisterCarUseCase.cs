using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;
using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Application.Interfaces.Service.UseCases.Car
{
    public interface IRegisterCarUseCase
    {
        Task<Cars> Execute(CarRegisterRequest request);
    }
}
