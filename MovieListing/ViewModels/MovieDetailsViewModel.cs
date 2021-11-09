using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MovieListing.ViewModels
{
    public class MovieDetailsViewModel
    {
        public  AsyncCommand GoBackCommand { get; }

        public MovieDetailsViewModel()
        {
            GoBackCommand = new AsyncCommand(GoBack);
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
