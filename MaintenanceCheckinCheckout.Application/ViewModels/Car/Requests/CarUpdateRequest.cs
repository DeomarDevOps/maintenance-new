namespace MaintenanceCheckinCheckout.Application.ViewModels.Car.Requests
{
    public sealed class CarUpdateRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Plate { get; set; }
    }
}
