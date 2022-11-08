using AutoMapper;
using MaintenanceCheckinCheckout.Application.Interfaces.Services.UseCases.Car;
using MaintenanceCheckinCheckout.Application.ViewModels.Car.Results;
using MaintenanceCheckinCheckout.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace MaintenanceCheckinCheckout.Application.Services.UseCases.Car
{
    public class GetCarsUseCase : IGetCarsUseCase
    {
        private readonly ICarReadOnlyRepository _carReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCarsUseCase> _logger;
        public GetCarsUseCase(ICarReadOnlyRepository carReadOnlyRepository,
            IMapper mapper,
            ILogger<GetCarsUseCase> logger)
        {
            _carReadOnlyRepository = carReadOnlyRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IList<CarResult>> Execute()
        {
            _logger.LogInformation("GetCarsUseCase - Iniciando busca por todos carros");
            var cars = await _carReadOnlyRepository.GetAll();

            var result = _mapper.Map<List<CarResult>>(cars);

            _logger.LogInformation("GetCarsUseCase - Finalizando busca por todos carros, total de registros:", result.Count());
            return result;
        }
    }
}
