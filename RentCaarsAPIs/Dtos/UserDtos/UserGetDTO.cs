using RentCaarsAPIs.Models;

namespace RentCaarsAPIs.Dtos.UserDtos
{
    public class UserGetDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
