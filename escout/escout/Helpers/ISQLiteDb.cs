using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace escout
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
