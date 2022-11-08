﻿using MaintenanceCheckinCheckout.Domain.Models.Cars;

namespace MaintenanceCheckinCheckout.Domain.Interfaces.Repositories
{

    public interface ICarWriteOnlyRepository
    {
        Task Add(Cars car);
        Task Update(Cars car);
        Task Delete(Cars car);
    }
}
