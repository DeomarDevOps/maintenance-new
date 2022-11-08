using AutoMapper;
using MaintenanceCheckinCheckout.Application.Interfaces.Service.UseCases.Car;
using MaintenanceCheckinCheckout.Application.Services.Exceptions;
using MaintenanceCheckinCheckout.Application.Validation.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;
using MaintenanceCheckinCheckout.Domain.Models.Cars;
using Microsoft.Extensions.Logging;

namespace MaintenanceCheckinCheckout.Application.Services.UseCases.Car
{
    public sealed  class RegisterCarUseCase : IRegisterCarUseCase
    {
        private readonly ICarWriteOnlyRepository carWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterCarUseCase> _logger;

        public RegisterCarUseCase(ICarWriteOnlyRepository carWriteOnlyRepository, IMapper mapper, ILogger<RegisterCarUseCase> logger)
        {
            this.carWriteOnlyRepository = carWriteOnlyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Cars> Execute(CarRegisterRequest request)
        {
            _logger.LogInformation("RegisterCarUseCase - Adicionando novo carro", request);
            var car = _mapper.Map<Cars>(request);

            var resultValidation = new CarRequestValidator().Validate(car);

            if (!resultValidation.IsValid)
            {
                _logger.LogError("RegisterCarUseCase - erro ao tentar adicionar carro", request, resultValidation.Errors);
                throw new ServiceException(resultValidation.ToString());
            }

            await carWriteOnlyRepository.Add(car);
            _logger.LogInformation("RegisterCarUseCase - sucesso ao adicionar novo carro", request);
            return car;
        }
    }
}
