using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MovieListing.DataAccess;
using MovieListing.Models;
using SQLite;
using Xamarin.Essentials;

namespace MovieListing.Services
{
    public class MovieService
    {
        private static IRepository<Movie> repo = null;
        // static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (repo != null)
                return;

            repo = new Repository<Movie>();
            //var database = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            //var db = new SQLiteAsyncConnection(database);
        }

        public static async Task AddMovie(string title, string genre, string image)
        {
            await Init();

            await repo.SaveAsync(new Movie
            {
                Title = title,
                Genre = genre,
                Image = image
            });
            
        }

        public static async Task RemoveMovie(Movie movie)
        {
            await Init();

            await repo.DeleteAsync(movie);
        }

        public static async Task<IEnumerable<Movie>> GetMovies()
        {
            await Init();

            var movies = await repo.GetAllAsync();

            return movies;
        }

        public static async Task<Movie> GetMovie(int id)
        {
            await Init();

            return await repo.GetById(id);
        }
    }
}
