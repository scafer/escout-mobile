using escout.Models.Rest;
using escout.Services.Database;

namespace escout.Models.Database
{
    class DbCompetition : BaseModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Edition { get; set; }
        public int SportId { get; set; }
        public int? ImageId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public int DataExt { get; set; }

        public DbCompetition() { }

        public DbCompetition(Competition competition, int gameId)
        {
            DataExt = gameId;
            Id = competition.Id;
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
