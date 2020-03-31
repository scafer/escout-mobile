using escout.Helpers;
using escout.Models.Rest;
using SQLite;

namespace escout.Models.Db
{
    class DbCompetition
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int gameId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Edition { get; set; }
        public int SportId { get; set; }
        public int? ImageId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }

        public DbCompetition() { }

        public DbCompetition(Competition competition, int gameId)
        {
            this.gameId = gameId;
            Key = competition.Key;
            Name = competition.Name;
            Edition = competition.Edition;
            SportId = competition.SportId;
            ImageId = competition.ImageId;
            Created = competition.Created;
            Updated = competition.Updated;
        }
    }
}
