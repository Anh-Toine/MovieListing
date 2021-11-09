using System;
using System.Collections.Generic;
using MovieListing.Services;
using Xamarin.Forms;

namespace MovieListing.Views
{
    [QueryProperty(nameof(MovieId), nameof(MovieId))]
    public partial class MovieDetailsPage : ContentPage
    {
        public string MovieId { get; set; }

        public MovieDetailsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool good = int.TryParse(MovieId, out int result);

            if (good)
            {
                BindingContext = await MovieService.GetMovie(result);
            }
        }
    }
}
