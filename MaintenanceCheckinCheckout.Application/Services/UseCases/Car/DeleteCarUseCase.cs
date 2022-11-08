using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Services.Exceptions;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace MaintenanceCheckinCheckout.Application.Services.UseCases.Car
{
    public class DeleteCarUseCase : IDeleteCarUseCase
    {
        private readonly ICarWriteOnlyRepository _carWriteOnlyRepository;
        private readonly ICarReadOnlyRepository _carReadOnlyRepository;
        private readonly ILogger<DeleteCarUseCase> _logger;

        public DeleteCarUseCase(ICarWriteOnlyRepository carWriteOnlyRepository,
            ICarReadOnlyRepository carReadOnlyRepository,
            ILogger<DeleteCarUseCase> logger)
        {
            _carWriteOnlyRepository = carWriteOnlyRepository;
            _carReadOnlyRepository = carReadOnlyRepository;
            _logger = logger;
        }
        public async Task Execute(Guid id)
        {
            _logger.LogInformation("DeleteCarUseCase - Iniciando", id);

            var car = await _carReadOnlyRepository.GetById(id);
            if (car == null)
            {
                _logger.LogError("DeleteCarUseCase - O carro não existe", id);
                throw new CarNotFoundException($"O carro {id} não existe");
            }

            await _carWriteOnlyRepository.Delete(car);
            _logger.LogInformation("DeleteCarUseCase - finalizando com sucesso", id);
        }
    }
}
