using System.IO;
using LocalDatabase.Db;
using LocalDatabase.Droid;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SqlConnection))]
namespace LocalDatabase.Droid
{
    public class SqlConnection : ISqLite
    {
        public SQLiteConnection GetConnection() {
            var sqliteFilename = "TodoSQLite.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}