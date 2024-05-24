namespace RentCaarsAPIs.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
