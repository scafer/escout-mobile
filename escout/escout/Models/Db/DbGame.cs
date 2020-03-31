using escout.Helpers;
using escout.Models.Rest;
using SQLite;

namespace escout.Models.Db
{
    class DbGame
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string HomeColor { get; set; }
        public string VisitorColor { get; set; }
        public int HomeScore { get; set; }
        public int VisitorScore { get; set; }
        public int HomePenaltyScore { get; set; }
        public int VisitorPenaltyScore { get; set; }
        public int Status { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public int HomeId { get; set; }
        public int VisitorId { get; set; }
        public int? CompetitionId { get; set; }
        public int? ImageId { get; set; }
        public int UserId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }

        public DbGame()
        {
            TimeStart = "test";
            TimeEnd = "test";
            HomeColor = "test";
            VisitorColor = "test";
            HomeScore = 1;
            VisitorScore = 1;
            HomePenaltyScore = 1;
            VisitorPenaltyScore = 1;
            Status = 1;
            Type = "test";
            Location = "test";
            HomeId = 1;
            VisitorId = 1;
            CompetitionId = 1;
            ImageId = 1;
            UserId = 1;
            Created = "test";
            Updated = "test";
        }

        public DbGame(Game game)
        {
            TimeStart = game.TimeStart;
            TimeEnd = game.TimeEnd;
            HomeColor = game.HomeColor;
            VisitorColor = game.VisitorColor;
            HomeScore = game.HomeScore;
            VisitorScore = game.VisitorScore;
            HomePenaltyScore = game.HomePenaltyScore;
            VisitorPenaltyScore = game.VisitorPenaltyScore;
            Status = game.Status;
            Type = game.Type;
            Location = game.Location;
            HomeId = game.HomeId;
            VisitorId = game.VisitorId;
            CompetitionId = game.CompetitionId;
            ImageId = game.ImageId;
            UserId = game.UserId;
            Created = game.Created;
            Updated = game.Updated;
        }
    }
}
