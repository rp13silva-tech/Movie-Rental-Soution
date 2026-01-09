using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Rental;

namespace MovieRental.Mappers
{
    public class RentalMapper(MovieRentalDbContext movieRentalDb) : IRentalMapper
    {
        public DTOs.RentalDTO MapToDto(Rental.Rental rental)
        {
            return new DTOs.RentalDTO
            {
                Id = rental.Id,
                DaysRented = rental.DaysRented,
                MovieId = rental.MovieId,
                PaymentMethod = rental.PaymentMethod,
                PaymentPrice = rental.PaymentPrice,
                CustomerId = rental.CustomerId,
                Customer = rental.Customer
            };
        }

        public Rental.Rental MapToDomainModel(DTOs.RentalDTO rentalDTO)
        {
            var rental = new Rental.Rental
            {
                Id = rentalDTO.Id,
                DaysRented = rentalDTO.DaysRented,
                MovieId = rentalDTO.MovieId,
                PaymentMethod = rentalDTO.PaymentMethod,
                PaymentPrice = rentalDTO.PaymentPrice,
                CustomerId = rentalDTO.CustomerId,
                Customer = rentalDTO.Customer
            };

            if (rentalDTO.Movie != null)
            {
                rental.Movie = movieRentalDb.Movies.Find(rentalDTO.Movie.Id);
            }

            return rental;
        }
    }
}