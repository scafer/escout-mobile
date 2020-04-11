using SQLite;

namespace escout.Helpers.Services
{
    public interface ISqLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
