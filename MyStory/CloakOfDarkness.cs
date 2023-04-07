﻿using WorldModel;
using StandardLibrary;
using Common;

namespace MyStory
{
    public static class LocationId
    {
        public const string Kitchen = "kitchen";
        public const string LivingRoom = "livingRoom";
        public const string FrontPorch = "frontPorch";
        public const string FrontYard = "frontYard";
    }

    public class CloakOfDarkness : IStory
    {
        public World World { get; private set; }
        public Core Core { get; private set; }
        public Player Player => Core.Player;

        public CloakOfDarkness()
        {
            World = new World();
            Core = new Core(World);
        }

        public void InitializeWorld()
        {
            Core.
                WorldManager
                .CreateLocation(LocationId.Kitchen, "Kitchen", "A clean and well-organized kitchen.")
                .CreateLocation(LocationId.LivingRoom, "Living Room", "A cozy living room with a comfortable couch.", LocationId.Kitchen, Direction.West)
                .CreateLocation(LocationId.FrontPorch, "Front Porch", "A small front porch with a wooden bench.", LocationId.LivingRoom, Direction.South)
                .CreateLocation(LocationId.FrontYard, "Front Yard", "A well-maintained front yard with a beautiful garden.", LocationId.FrontPorch, Direction.Out);

            Thing potplant = new Thing("plant", "It's a pot plant");
            potplant.Adjectives = new List<string> { "pot" };

            Thing pot = new Thing("pot", "It's a plant pot");
            pot.Adjectives = new List<string> { "plant" };
        }

        public void OnGameStart()
        {
            throw new NotImplementedException();
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
    }

}