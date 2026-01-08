using MovieRental.Data;
using MovieRental.Mappers;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		private readonly IMovieMapper _movieMapper;

		public MovieFeatures(MovieRentalDbContext movieRentalDb, IMovieMapper movieMapper)
		{
			_movieRentalDb = movieRentalDb;
			_movieMapper = movieMapper;
		}

		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

		public List<DTOs.MovieDTO> GetAll()
		{
            List<Movie> movies = _movieRentalDb.Movies.ToList();
            List<DTOs.MovieDTO> moviesDtos = new ();

			foreach (var movie in movies)
			{
				moviesDtos.Add(_movieMapper.MapToDto(movie));
			}

            return moviesDtos;
        }


	}
}
