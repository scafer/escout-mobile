using escout.Models.Db;

namespace escout.Models
{
    public class DbGameWithClub
    {
        public DbGame dbGame { get; set; }
        public DbClub dbClubHome { get; set; }
        public DbClub dbClubVisitor { get; set; }

        public DbGameWithClub(DbGame dbGame)
        {
            this.dbGame = dbGame;
            this.dbClubHome = new DbClub();
            this.dbClubVisitor = new DbClub();
        }
    }
}
