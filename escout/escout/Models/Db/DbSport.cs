using escout.Helpers;
using escout.Models.Rest;

namespace escout.Models.Db
{
    class DbSport : BaseModel
    {
        public int gameId { get; set; }
        public string Name { get; set; }
        public string ImageId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }

        public DbSport() { }

        public DbSport(Sport sport, int gameId)
        {
            this.gameId = gameId;
            Name = sport.Name;
            ImageId = sport.ImageId;
            Created = sport.Created;
            Updated = sport.Updated;
        }
    }
}
