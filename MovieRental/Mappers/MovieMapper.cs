namespace MovieRental.Mappers
{
    public class MovieMapper : IMovieMapper
    {
        public DTOs.MovieDTO MapToDto(Movie.Movie movie)
        {
            return new DTOs.MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title
            };
        }

        public Movie.Movie MapToDomainModel(DTOs.MovieDTO movieDTO)
        {
            return new Movie.Movie
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title
            };
        }
    }
}
