using System;
using System.IO;
using escout.Helpers;
using escout.iOS.Persistence;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteDb))]

namespace escout.iOS.Persistence
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