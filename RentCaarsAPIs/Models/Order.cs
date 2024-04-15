namespace RentCaarsAPIs.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
