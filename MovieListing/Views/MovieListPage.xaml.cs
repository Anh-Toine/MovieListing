using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieListing.Models;
using MovieListing.ViewModels;
using Xamarin.Forms;

namespace MovieListing.Views
{
    public partial class MovieListPage : ContentPage
    {
        public MovieListPage()
        {
            InitializeComponent();
            MovieListViewModel mlvm = new MovieListViewModel();
            BindingContext = mlvm;
        }

        async void MenuItem_Clicked(System.Object sender, System.EventArgs e)
        {
            var movie = ((MenuItem)sender).BindingContext as Movie;
            if (movie == null)
            {
                return;
            }
            await DisplayAlert("Movie Favorited", movie.Title, "OK");

        }

        //async void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        //{
        //    var movie = ((ListView)sender).SelectedItem as Movie;
        //    if(movie == null)
        //    {
        //        return;
        //    }
        //    await DisplayAlert("Movie Selected", movie.Title, "OK");
        //}

        //void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        //{
        //    ((ListView)sender).SelectedItem = null;
        //}
    }
    }
