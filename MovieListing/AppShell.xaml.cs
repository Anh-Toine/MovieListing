using System;
using System.Collections.Generic;
using MovieListing.Views;
using Xamarin.Forms;

namespace MovieListing
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddMoviePage), typeof(AddMoviePage));
            Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));
        }

    }
}
