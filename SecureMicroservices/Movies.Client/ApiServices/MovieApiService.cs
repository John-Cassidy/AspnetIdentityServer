using IdentityModel.Client;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService {

        public async Task<IEnumerable<Movie>> GetMovies() {

            // 1 - Get Token from Identity Server
            // 2 - Send Request to Protected API
            // 3 - Deserialize Object to MovieList           

            // 1. "retrieve" our api credentials. This must be registered on Identity Server!
            var apiClientCredentials = new ClientCredentialsTokenRequest {
                Address = "https://localhost:5005/connect/token",

                ClientId = "movieClient",
                ClientSecret = "secret",

                // This is the scope our Protected API requires. 
                Scope = "movieAPI"
            };

            // creates a new HttpClient to talk to our IdentityServer (localhost:5005)
            var client = new HttpClient();

            // just checks if we can reach the Discovery document. Not 100% needed but..
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
            if (disco.IsError) {
                return null; // throw 500 error
            }

            // 2. Authenticates and get an access token from Identity Server
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
            if (tokenResponse.IsError) {
                return null;
            }

            // Another HttpClient for talking now with our Protected API
            var apiClient = new HttpClient();

            // 3. Set the access_token in the request Authorization: Bearer <token>
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // 4. Send a request to our Protected API
            var response = await apiClient.GetAsync("https://localhost:5001/api/movies");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };

            var movieList = JsonSerializer.Deserialize<List<Movie>>(content, options);
            return movieList;


            //var movieList = new List<Movie>();
            //movieList.Add(
            //            new Movie {
            //                Id = 1,
            //                Genre = "Comics",
            //                Title = "Software Inc",
            //                Rating = "9.0",
            //                ImageUrl = "image/src",
            //                ReleaseDate = DateTime.Now,
            //                Owner = "jpc"
            //            });
            //return await Task.FromResult(movieList);
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
