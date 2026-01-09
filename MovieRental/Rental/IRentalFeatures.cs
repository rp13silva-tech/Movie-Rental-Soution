namespace MovieRental.Rental;

public interface IRentalFeatures
{
	Task<DTOs.RentalDTO> Save(DTOs.RentalDTO rental);
	Task<IEnumerable<Rental>> GetRentalsByCustomerId(int customerId);
}