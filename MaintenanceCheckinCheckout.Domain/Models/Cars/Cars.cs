namespace MaintenanceCheckinCheckout.Domain.Models.Cars
{
    public sealed class Cars : IEntity
    {
        public Guid Id { get; private set; }
        public string Description { get; set; }
        public string Plate { get; set; }

        public Cars(string description, string plate)
        {
            Id = Guid.NewGuid();
            Description = description;
            Plate = plate;
        }

        private Cars() { }
    }
}
