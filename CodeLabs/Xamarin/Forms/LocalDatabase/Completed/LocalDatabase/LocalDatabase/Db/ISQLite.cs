using SQLite;

namespace LocalDatabase.Db
{
    public interface ISqLite
    {
        SQLiteConnection GetConnection();
    }
}
