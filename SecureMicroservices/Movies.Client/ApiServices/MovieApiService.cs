using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService {

        public async Task<IEnumerable<Movie>> GetMovies() {
            var movieList = new List<Movie>();
            movieList.Add(
                        new Movie {
                            Id = 1,
                            Genre = "Comics",
                            Title = "Software Inc",
                            Rating = "9.0",
                            ImageUrl = "image/src",
                            ReleaseDate = DateTime.Now,
                            Owner = "jpc"
                        });
            return await Task.FromResult(movieList);
        }

        public Task<Movie> GetMovie(string id) {
            throw new NotImplementedException();
        }
        public Task<Movie> CreateMovie(Movie movie) => throw new NotImplementedException();
        public Task DeleteMovie(int id) => throw new NotImplementedException();
        public Task<UserInfoViewModel> GetUserInfo() => throw new NotImplementedException();
        public Task<Movie> UpdateMovie(Movie movie) => throw new NotImplementedException();
    }
}
