namespace escout.Models.Rest
{
    class GameWithClub
    {
        public Game Game { get; set; }
        public Club ClubHome { get; set; }
        public Club ClubVisitor { get; set; }

        public GameWithClub(Game Game)
        {
            this.Game = Game;
            ClubHome = new Club();
            ClubVisitor = new Club();
        }
    }
}
