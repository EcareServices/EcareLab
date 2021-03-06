﻿using System.IO;
using Windows.Storage;
using LocalDatabase.Db;
using LocalDatabase.WinPhone;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SqlConnection))]
namespace LocalDatabase.WinPhone
{
    public class SqlConnection : ISqLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
