namespace MovieRental.Mappers
{
    public interface IMovieMapper
    {
        Movie.Movie MapToDomainModel(DTOs.MovieDTO movieDto);
        DTOs.MovieDTO MapToDto(Movie.Movie movie);
    }
}
