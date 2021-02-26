using SQLite;

namespace escout.Services.Database
{
    public class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int LocalId { get; set; }
    }
}
