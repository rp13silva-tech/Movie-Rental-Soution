namespace MovieRental.Mappers
{
    public interface IRentalMapper
    {
        Rental.Rental MapToDomainModel(DTOs.RentalDTO rentalDto);
        DTOs.RentalDTO MapToDto(Rental.Rental rental);
    }
}
