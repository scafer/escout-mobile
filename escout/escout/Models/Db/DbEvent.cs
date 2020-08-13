using escout.Models.Rest;
using SQLite;

namespace escout.Models.Db
{
    class DbEvent
    {
        [PrimaryKey, AutoIncrement]
        public int LocalId { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SportId { get; set; }
        public int? ImageId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public int DataExt { get; set; }

        public DbEvent() { }

        public DbEvent(Event evt, int gameId)
        {
            DataExt = gameId;
            Id = evt.Id;
            Key = evt.Key;
            Name = evt.Name;
            Description = evt.Description;
            SportId = evt.SportId;
            ImageId = evt.ImageId;
            Created = evt.Created;
            Updated = evt.Updated;
        }
    }
}
