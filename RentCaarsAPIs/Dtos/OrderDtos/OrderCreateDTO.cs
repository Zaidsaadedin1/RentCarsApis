using RentCaarsAPIs.Models;

namespace RentCaarsAPIs.Dtos.OrderDtos
{
    public class OrderCreateDTO
    {
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int UserId { get; set; }
        public int CarId { get; set; }
    
    }
}
