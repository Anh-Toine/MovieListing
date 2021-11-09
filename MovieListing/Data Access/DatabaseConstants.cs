using System;
using System.IO;
using Xamarin.Essentials;

namespace MovieListing.DataAccess
{
    public static class DatabaseConstants
    {
        public const string DatabaseFileName = "MovieSQLite2.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
