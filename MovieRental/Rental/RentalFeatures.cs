using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Mappers;
using MovieRental.PaymentProviders;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		private readonly MbWayProvider _mbWayProvider;
		private readonly PayPalProvider _payPalProvider;
		private readonly IRentalMapper _rentalMapper;

        public RentalFeatures(MovieRentalDbContext movieRentalDb, IRentalMapper rentalMapper, MbWayProvider mbWayProvider, PayPalProvider payPalProvider)
		{
			_movieRentalDb = movieRentalDb;
            _rentalMapper = rentalMapper;
            _mbWayProvider = mbWayProvider;
			_payPalProvider = payPalProvider;
        }

		public async Task<DTOs.RentalDTO> Save(DTOs.RentalDTO rentalDto)
		{
			if (await IsPaymentSucceded(rentalDto))
			{
                Rental? rental = GetRentalById(rentalDto.Id);

                if (rental == null)
                {
                    _movieRentalDb.Rentals.Add(_rentalMapper.MapToDomainModel(rentalDto));
                    await _movieRentalDb.SaveChangesAsync();
                    return rentalDto;
                }
                else
                {
					// Extensibility
					if (rental.PaymentMethod.Contains(rentalDto.PaymentMethod))
					{
						return _rentalMapper.MapToDto(rental);
					}
					else
					{
						rental.PaymentMethod = $"{rental.PaymentMethod}, {rentalDto.PaymentMethod}";
                        await _movieRentalDb.SaveChangesAsync();
						return _rentalMapper.MapToDto(rental);
                    }
                }
            }
			else
			{
				throw new Exception("Rental was not added to the system");
			}
		}

		public async Task<IEnumerable<Rental>> GetRentalsByCustomerId(int customerId)
		{
			return await _movieRentalDb.Rentals
				.Where(rental => rental.CustomerId == customerId)
				.ToListAsync();
		}

		private Rental? GetRentalById(int rentalId)
		{
			return _movieRentalDb.Rentals
                .Where(rental => rental.Id == rentalId)
                .FirstOrDefault();
		}

		private async Task<bool> IsPaymentSucceded(DTOs.RentalDTO rentalDto)
		{
            // Payment Failure Handling
            if (rentalDto.PaymentMethod == Constants.Constants.MBWAY)
            {
                return await _mbWayProvider.Pay(rentalDto.PaymentPrice);
            }
            else if (rentalDto.PaymentMethod == Constants.Constants.PAYPAL)
            {
                return await _payPalProvider.Pay(rentalDto.PaymentPrice);
            }
			else
			{
				return false;
			}
        }
	}
}
