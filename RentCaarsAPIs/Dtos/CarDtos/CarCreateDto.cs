namespace RentCaarsAPIs.Dtos.CarDtos
{
    public class CarCreateDto
    {
        public string Model { get; set; }
        public string Brand { get; set; }          // Brand of the car, e.g., Toyota, Ford
        public string LicenseNumber { get; set; }
        public bool IsAvailable { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }             // Model year of the car
        public float PricePerDay { get; set; }  // Rental price per day
        public string ImageUrl { get; set; }      // URL to an image of the car

        // You can also add more detailed specifications if required:
        public double Mileage { get; set; }       // Current mileage
        public string Description { get; set; }   // Additional details about the car
        public int UserId { get; set; }

    }
}
