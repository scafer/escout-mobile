using escout.Models.Rest;
using SQLite;

namespace escout.Models.Db
{
    public class DbAthlete
    {
        [PrimaryKey, AutoIncrement]
        public int LocalId { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Citizenship { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Position { get; set; }
        public int PositionKey { get; set; }
        public string Agent { get; set; }
        public string CurrentInternational { get; set; }
        public string Status { get; set; }
        public int? ClubId { get; set; }
        public int? ImageId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public int DataExt { get; set; }

        public DbAthlete() { }

        public DbAthlete(Athlete athlete, int gameId)
        {
            DataExt = gameId;
            Id = athlete.Id;
            Key = athlete.Key;
            Name = athlete.Name;
            Fullname = athlete.Fullname;
            BirthDate = athlete.BirthDate;
            BirthPlace = athlete.BirthPlace;
            Citizenship = athlete.Citizenship;
            Height = athlete.Height;
            Weight = athlete.Weight;
            Position = athlete.Position;
            PositionKey = athlete.PositionKey;
            Agent = athlete.Agent;
            CurrentInternational = athlete.CurrentInternational;
            Status = athlete.Status;
            ClubId = athlete.ClubId;
            ImageId = athlete.ImageId;
            Created = athlete.Created;
            Updated = athlete.Updated;
        }
    }
}
