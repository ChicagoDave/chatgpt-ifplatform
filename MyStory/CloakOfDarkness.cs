using WorldModel;
using StandardLibrary;
using Common;

namespace MyStory
{
    public static class ObjectNames
    {
        public const string onStartMessage = "Hurrying through the rainswept November night, you're glad to see the bright lights of the Opera House. It's surprising that there aren't more people about but, hey, what do you expect in a cheap demo game...?";
        public const string FoyerOfTheOperaHouse = "Foyer of the Opera House";
        public const string FoyerBar = "Foyer Bar";
        public const string CloakRoom = "Cloak Room";
        public const string Cloak = "cloak";
    }

    public class CloakOfDarkness : IStory
    {
        public World World { get; private set; }
        public Core Core { get; private set; }
        public Player Player => Core.Player;

        public bool UseScoring { get; set; } = true;

        public int Score { get; private set; } = 0;

        public CloakOfDarkness()
        {
            World = new World();
            Core = new Core(World);
        }

        public void InitializeWorld()
        {
            Location foyerOfTheOperaHouse = new Location(ObjectNames.FoyerOfTheOperaHouse, "A clean and well-organized kitchen.");
            foyerOfTheOperaHouse.Lit = () => true;


                //.CreateLocation(ObjectNames.FoyerBar, "A cozy living room with a comfortable couch.", ObjectNames.FoyerOfTheOperaHouse, Direction.North)
                //.CreateLocation(ObjectNames.CloakRoom, "A small front porch with a wooden bench.", ObjectNames.FoyerOfTheOperaHouse, Direction.East);

            Thing cloak = new Thing(ObjectNames.Cloak, "A handsome cloak, of velvet trimmed with satin, and slightly splattered with raindrops. Its blackness is so deep that it almost seems to suck light from the room.");
            cloak.Adjectives = new List<string>() { "handsome", "dark", "satin", "black" };
            cloak.Wearable = ()=> true;            
        }

        public void OnGameStart()
        {
            Console.WriteLine(ObjectNames.onStartMessage);
        }

        public void OnPreTurn()
        {
            throw new NotImplementedException();
        }

        public void OnPostTurn()
        {
            throw new NotImplementedException();
        }

        public void OnGameEndWin()
        {
            throw new NotImplementedException();
        }

        public void OnGameEndLose()
        {
            throw new NotImplementedException();
        }

        public void IncrementScore(int? amount)
        {
            if (amount == null)
                Score = Score++;
            else
                Score = (int)(Score + amount);
        }

        public void DecrementScore(int? amount)
        {
            if (amount == null)
                Score = Score--;
            else
                Score = (int)(Score - amount);
        }
    }

}
