namespace MovieRental.Movie;

public interface IMovieFeatures
{
	Movie Save(Movie movie);
	List<DTOs.MovieDTO> GetAll();
}