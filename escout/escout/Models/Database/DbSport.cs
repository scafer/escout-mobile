﻿using escout.Models.Rest;
using escout.Services.Database;

namespace escout.Models.Database
{
    public class DbSport : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public int DataExt { get; set; }

        public DbSport() { }

        public DbSport(Sport sport, int gameId)
        {
            DataExt = gameId;
            Id = sport.Id;
            Name = sport.Name;
            ImageId = sport.ImageId;
            Created = sport.Created;
            Updated = sport.Updated;
        }
    }
}
