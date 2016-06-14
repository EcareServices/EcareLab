using System;
using System.IO;
using LocalDatabase.Db;
using LocalDatabase.iOS;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SqlConnection))]
namespace LocalDatabase.iOS
{
    public class SqlConnection : ISqLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
