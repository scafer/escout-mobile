using System;
using System.IO;
using escout.Droid.Persistence;
using escout.Helpers;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteDb))]

namespace escout.Droid.Persistence
{
    public class SqLiteDb : ISqLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "escoutdb.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}