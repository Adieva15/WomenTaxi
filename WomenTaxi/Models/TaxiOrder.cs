namespace WomenTaxi.Models
{
    public class TaxiOrder
    {
        // модель для бд
        public int Id { get; set; }
        public string PickupAddress { get; set; } = "";
        public string DestinarionAddress { get; set; } = "";
        public string Phone { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Новый";
        public decimal Price { get; set; }
    }
}
