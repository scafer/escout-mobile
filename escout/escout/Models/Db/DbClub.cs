using escout.Models.Rest;
using SQLite;

namespace escout.Models.Db
{
    public class DbClub
    {
        [PrimaryKey, AutoIncrement]
        public int LocalId { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public string Country { get; set; }
        public string Founded { get; set; }
        public string Colors { get; set; }
        public string Members { get; set; }
        public string Stadium { get; set; }
        public string Address { get; set; }
        public string Homepage { get; set; }
        public int? ImageId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public int DataExt { get; set; }

        public DbClub() { }

        public DbClub(Club club, int gameId)
        {
            DataExt = gameId;
            Id = club.Id;
            Key = club.Key;
            Name = club.Name;
            Fullname = club.Fullname;
            Country = club.Country;
            Founded = club.Founded;
            Colors = club.Colors;
            Members = club.Members;
            Stadium = club.Stadium;
            Address = club.Address;
            Homepage = club.Homepage;
            ImageId = club.ImageId;
            Created = club.Created;
            Updated = club.Updated;
        }
    }
}
