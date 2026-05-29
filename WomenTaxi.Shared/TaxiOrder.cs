namespace WomenTaxi.Shared
{
    public class TaxiOrder
    {
        public int Id { get; set; }
        public string PickupAddress { get; set; } = string.Empty;
        public string DestinationAddress { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Новый";
        public decimal Price { get; set; }
    }
}
