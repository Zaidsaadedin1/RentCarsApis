using RentCaarsAPIs.Models;

namespace RentCaarsAPIs.Dtos.OrderDtos
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }

        public User User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
