using SQLite;

namespace escout.Helpers
{
    public interface ISqLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
