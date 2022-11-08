using AutoMapper;
using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Services.Exceptions;
using MaintenanceCheckinCheckout.Application.Validation.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;
using MaintenanceCheckinCheckout.Domain.Models.Cars;
using Microsoft.Extensions.Logging;

namespace MaintenanceCheckinCheckout.Application.Services.UseCases.Car
{
    public sealed class UpdateCarUseCase : IUpdateCarUseCase
    {

        private readonly ICarReadOnlyRepository _carReadOnlyRepository;
        private readonly ICarWriteOnlyRepository _carWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCarUseCase> _logger;

        public UpdateCarUseCase(ICarReadOnlyRepository carReadOnlyRepository, ICarWriteOnlyRepository carWriteOnlyRepository, IMapper mapper, ILogger<UpdateCarUseCase> logger)
        {
            _carReadOnlyRepository = carReadOnlyRepository;
            _carWriteOnlyRepository = carWriteOnlyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Cars> Execute(CarUpdateRequest request)
        {
            _logger.LogInformation("UpdateCarUseCase - Iniciando atualização de carro", request);
            Cars car = await _carReadOnlyRepository.GetById(request.Id);
            if (car == null)
                throw new CarNotFoundException($"O carro {request.Description} não existe");

            var resultValidation = new CarRequestValidator().Validate(car);

            if (!resultValidation.IsValid)
            {
                _logger.LogError("UpdateCarUseCase - Erro ao tentar atualizar carro", resultValidation.Errors);
                throw new ServiceException(resultValidation.ToString());
            }

            var carToBeUpdated = _mapper.Map<Cars>(request);
            await _carWriteOnlyRepository.Update(carToBeUpdated);

            _logger.LogInformation("UpdateCarUseCase - Finalizando atualização de carro", carToBeUpdated);

            return carToBeUpdated;
        }
    }
}
