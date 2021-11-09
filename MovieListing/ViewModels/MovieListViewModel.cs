using System;
using System.Linq;
using System.Threading.Tasks;
using MovieListing.Models;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Windows.Input;
using MovieListing.Services;
using MovieListing.Views;

namespace MovieListing.ViewModels
{
    public class MovieListViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Movie> MovieList { get; set; }

        public ObservableRangeCollection<Grouping<string,Movie>> MovieGroups { get; set; }

        public string PageTitle { get; set; }
        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand AddCommand { get; }

        public AsyncCommand<Movie> RemoveCommand { get; }

        public AsyncCommand<Movie> FavoriteMovieCommand { get; }

        public ICommand LoadmoreCommand { get; }

        public ICommand DelayLoadmoreCommand { get; }

        public ICommand ClearCommand { get; }

        public ICommand SelectCommand { get; }

        private Movie previouslySelectedMovie;

        private Movie selectedMovie;

        public Movie SelectedMovie
        {
            //get
            //{
            //    return selectedMovie;
            //}
            //set
            //{
            //    if(value != null)
            //    {
            //        Application.Current.MainPage.DisplayAlert("Selected Movie", value.Title, "OK");
            //        previouslySelectedMovie = value;
            //        value = null; // To set SelectedItem to null
            //    }
            //    selectedMovie = value; // Set selectedMovie to null
            //    OnPropertyChanged(); // Notify any change made(deselect the movie)
            //}
            get => selectedMovie;
            set => SetProperty(ref selectedMovie, value);
        }

        private Movie movie;
        public Movie Movie
        {
            get => movie;
            set
            {
                movie = value;
                PageTitle = value?.Title;
                OnPropertyChanged("PageTitle");
            }
        }


        public MovieListViewModel() 
	    {
            Title = "Movie List";

            MovieList = new ObservableRangeCollection<Movie>();

            RefreshCommand = new AsyncCommand(Refresh);

            FavoriteMovieCommand = new AsyncCommand<Movie>(Favorite);

            LoadmoreCommand = new Command(Loadmore);

            DelayLoadmoreCommand = new Command(DelayLoadmore);

            ClearCommand = new Command(Clear);

            AddCommand = new AsyncCommand(AddMovie);

            SelectCommand = new AsyncCommand(Selected);

            RemoveCommand = new AsyncCommand<Movie>(RemoveMovie);

            MovieGroups = new ObservableRangeCollection<Grouping<string, Movie>>();

            
            CreateGroups();

            InitMovies();
        }

        private async Task Selected()
        {
            if (SelectedMovie == null)
                return;

            var route = $"{nameof(MovieDetailsPage)}?MovieId={SelectedMovie.Id}";

            await Shell.Current.GoToAsync(route);

            SelectedMovie = null;
        }

        private void InitMovies()
        {
            MovieList.Add(new Movie
            {
                Title = "The Shawshank Redemption",
                Genre = "Drama",
                Image = "https://m.media-amazon.com/images/M/MV5BMDFkYTc0MGEtZmNhMC00ZDIzLWFmNTEtODM1ZmRlYWMwMWFmXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_UY67_CR0,0,45,67_AL_.jpg"
            });
            MovieList.Add(new Movie
            {
                Title = "The Godfather",
                Genre = "Drama",
                Image = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_UY67_CR1,0,45,67_AL_.jpg"
            });
            MovieList.Add(new Movie
            {
                Title = "The Matrix",
                Genre = "SciFi",
                Image = "https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_UX45_CR0,0,45,67_AL_.jpg"
            });
            MovieList.Add(new Movie
            {
                Title = "The Intouchables",
                Genre = "Comedy",
                Image = "https://m.media-amazon.com/images/M/MV5BMTYxNDA3MDQwNl5BMl5BanBnXkFtZTcwNTU4Mzc1Nw@@._V1_UY67_CR0,0,45,67_AL_.jpg"
            });
            MovieList.Add(new Movie
            {
                Title = "The Dark Knight",
                Genre = "Superhero",
                Image = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_UY67_CR0,0,45,67_AL_.jpg"
            });
            MovieList.Add(new Movie
            {
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                Genre = "Fantasy",
                Image = "https://m.media-amazon.com/images/M/MV5BN2EyZjM3NzUtNWUzMi00MTgxLWI0NTctMzY4M2VlOTdjZWRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_UY67_CR0,0,45,67_AL_.jpg"
            });
            MovieList.Add(new Movie
            {
                Title = "Inception",
                Genre = "SciFi",
                Image = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_UY67_CR0,0,45,67_AL_.jpg"
            });
            MovieList.Add(new Movie
            {
                Title = "Saving Private Ryan",
                Genre = "Drama",
                Image = "https://m.media-amazon.com/images/M/MV5BZjhkMDM4MWItZTVjOC00ZDRhLThmYTAtM2I5NzBmNmNlMzI1XkEyXkFqcGdeQXVyNDYyMDk5MTU@._V1_UY67_CR0,0,45,67_AL_.jpg"
            });

        }

        private void Clear()
        {
            MovieList.Clear();
            MovieGroups.Clear();
        }

        private void DelayLoadmore()
        {
            int count = MovieList.Count;
            if (count <= 10)
            {
                return;
            }
            else
            {
                Loadmore();
            }
        }
        
        private void Loadmore()
        {
            int count = MovieList.Count;
            if (count < 20)
            {
                
                MovieList.Add(new Movie
                {
                    Title = "Megamind",
                    Genre = "Animated",
                    Image = "https://m.media-amazon.com/images/M/MV5BMTAzMzI0NTMzNDBeQTJeQWpwZ15BbWU3MDM3NTAyOTM@._V1_.jpg",
                });
                MovieList.Add(new Movie
                {
                    Title = "Footloose",
                    Genre = "Drama",
                    Image = "https://m.media-amazon.com/images/M/MV5BYTE2NjQ1MjQtOTdmNi00NjJmLWJlYTItNmFhYzBmMWM4NTQ1XkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_.jpg",
                });
                MovieList.Add(new Movie
                {
                    Title = "Aladdin",
                    Genre = "Fantasy",
                    Image = "https://m.media-amazon.com/images/M/MV5BY2Q2NDI1MjUtM2Q5ZS00MTFlLWJiYWEtNTZmNjQ3OGJkZDgxXkEyXkFqcGdeQXVyNTI4MjkwNjA@._V1_FMjpg_UX1000_.jpg",
                });
                MovieList.Add(new Movie
                {
                    Title = "Foo",
                    Genre = "Null",
                    Image = "https://yt3.ggpht.com/ytc/AKedOLRtJMw3bOqLjOW62ES2PNKpwK42j3bAQ2-TDqFSxg=s900-c-k-c0x00ffffff-no-rj",
                });
                MovieList.Add(new Movie
                {
                    Title = "Bar",
                    Genre = "Null",
                    Image = "https://yt3.ggpht.com/ytc/AKedOLRtJMw3bOqLjOW62ES2PNKpwK42j3bAQ2-TDqFSxg=s900-c-k-c0x00ffffff-no-rj",
                });
               
            }

        }
        private async Task AddMovie()
        {
            //var title = await App.Current.MainPage.DisplayPromptAsync("Title", "Enter the movie title: ", "OK", "Cancel");
            //var genre = await App.Current.MainPage.DisplayPromptAsync("Genre", "Enter the movie genre: ", "OK", "Cancel");
            //var image = await App.Current.MainPage.DisplayPromptAsync("Image", "Enter the movie image: ", "OK", "Cancel");

            //await MovieService.AddMovie(title, genre, image);
            //await Refresh();
            var route = $"{nameof(AddMoviePage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task RemoveMovie(Movie movie)
        {
            await MovieService.RemoveMovie(movie);
            await Refresh();
        }

        private async Task Favorite(Movie movie)
        {
            if (movie == null)
            {
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Favorite", movie.Title, "OK");
        }

        private async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            MovieList.Clear();

            var movies = await MovieService.GetMovies();

            MovieList.ReplaceRange(movies);

            //InitMovies();

            CreateGroups();
            IsBusy = false;
        }

        private void CreateGroups()
        {
            MovieGroups.Clear();

            MovieGroups.Add(new Grouping<string, Movie>("Comedy", MovieList.Where(m => m.Genre.Contains("Comedy"))));
            MovieGroups.Add(new Grouping<string, Movie>("Drama", MovieList.Where(m => m.Genre.Contains("Drama"))));
            MovieGroups.Add(new Grouping<string, Movie>("Fantasy", MovieList.Where(m => m.Genre.Contains("Fantasy"))));
            MovieGroups.Add(new Grouping<string, Movie>("SciFi", MovieList.Where(m => m.Genre.Contains("SciFi"))));
            MovieGroups.Add(new Grouping<string, Movie>("Superhero", MovieList.Where(m => m.Genre.Contains("Superhero"))));
            MovieGroups.Add(new Grouping<string, Movie>("Action", MovieList.Where(m => m.Genre.Contains("Action"))));
        }

        
    }
}
