namespace MovieRental.DTOs
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public int DaysRented { get; set; }
        public DTOs.MovieDTO? Movie { get; set; }

        public int MovieId { get; set; }

        public string PaymentMethod { get; set; }
        public double PaymentPrice { get; set; }

        public int CustomerId { get; set; }

        public Customer.Customer? Customer { get; set; }
    }
}