using escout.Models.Database;

namespace escout.Models
{
    public class DbGameWithClub
    {
        public DbGame DbGame { get; set; }
        public DbClub DbClubHome { get; set; }
        public DbClub DbClubVisitor { get; set; }

        public DbGameWithClub(DbGame dbGame)
        {
            DbGame = dbGame;
            DbClubHome = new DbClub();
            DbClubVisitor = new DbClub();
        }
    }
}
