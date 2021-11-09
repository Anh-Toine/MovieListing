using System;
using System.Threading.Tasks;
using MovieListing.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MovieListing.ViewModels
{
    public class AddMovieViewModel : ViewModelBase
    {
        public AddMovieViewModel()
        {
            Title = "Add Movie";
            SaveCommand = new AsyncCommand(Save);
        }

        string movieTitle, genre, image;

        public string MovieTitle
        {
            get => movieTitle;
            set => SetProperty(ref movieTitle, value);
        }

        public string Genre
        {
            get => genre;
            set => SetProperty(ref genre, value);
        }

        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public AsyncCommand SaveCommand { get; }

        private async Task Save()
        {
            if(string.IsNullOrWhiteSpace(movieTitle) ||
                string.IsNullOrWhiteSpace(genre) ||
                string.IsNullOrWhiteSpace(image))
            {
                return;
            }
            else
            {
                await MovieService.AddMovie(movieTitle, genre, image);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
