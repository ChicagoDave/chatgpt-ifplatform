using System;
using System.Collections.Generic;
using GrammarLibrary;
using WorldModel;

namespace StandardLibrary
{
    public class Core
    {
        private World _world;
        private Grammar _grammar;

        public Player Player { get; private set; }

        public Core(World world)
        {
            _grammar = new Grammar(world);
            _world = world;

            Player = new Player("Player", "You are a scruffy adventurer.");
            _world.AddNode(Player.Id, Player);

            // Standard actions
            var standardTake = _grammar.Verb("take", Take).Noun.End;
            var metaExamine = _grammar.Verb("examine", Examine).Noun.End;
            var standardGoInNoun = _grammar.Verb("go", Go).In.Noun.End;
            var standardGoNoun = _grammar.Verb("go", Go).On.Noun.End;

            // Meta actions
            _grammar.MetaVerb("score", Score);
            _grammar.MetaVerb("restore", Restore);
            _grammar.MetaVerb("restart", Restart);

            // Override the "go" action
            var goInNoun = new List<Token>
            {
                new Token(TokenType.Verb, "go", Go, ActionType.Standard),
                new Token(TokenType.Preposition, "in"),
                new Token(TokenType.Noun)
            };

            _grammar.OverrideActionDelegate("go", goInNoun, CustomGo);
            _world = world;
        }

        public Core CreateLocation(string id, string name, string description, string? connectedToId = null, Direction? direction = null)
        {
            Location location = new Location(name, description);
            AddLocation(location, connectedToId, direction);

            if (connectedToId != null && direction.HasValue)
            {
                Direction oppositeDirection = GetOppositeDirection(direction.Value);
                ConnectLocations(connectedToId, id, oppositeDirection);
            }

            // If there is only one location (the one just created), connect the Player to the location with IsIn and Hosts edge types
            if (_world.Nodes.Values.Count(node => node.Data is Location) == 1)
            {
                _world.ConnectNodes(Player.Id, id, EdgeType.IsIn, "Player is in the location");
                _world.ConnectNodes(id, Player.Id, EdgeType.Hosts, "Location hosts the player");
            }

            return this;
        }

        private void AddLocation(Location location, string? connectedToId, Direction? direction)
        {
            _world.AddNode(location.Id, location);

            if (connectedToId != null && direction.HasValue)
            {
                ConnectLocations(location.Id, connectedToId, direction.Value);
            }
        }

        private void AddThing(Location location, IThing thing)
        {
            _world.AddNode(thing.Id, thing);
            _world.ConnectNodes(thing.Id, location.Id, EdgeType.IsIn, thing.Name);
        }

        private void ConnectLocations(string id1, string id2, Direction direction)
        {
            EdgeType edgeType = EdgeType.LeadsTo;
            _world.ConnectNodes(id1, id2, edgeType, direction.ToString());
        }

        public Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.Northeast:
                    return Direction.Southwest;
                case Direction.East:
                    return Direction.West;
                case Direction.Southeast:
                    return Direction.Northwest;
                case Direction.South:
                    return Direction.North;
                case Direction.Southwest:
                    return Direction.Northeast;
                case Direction.West:
                    return Direction.East;
                case Direction.Northwest:
                    return Direction.Southeast;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.In:
                    return Direction.Out;
                case Direction.Out:
                    return Direction.In;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction provided.");
            }
        }

        private void Take(List<Token>? tokens)
        {
            Console.WriteLine("You take the item.");
        }

        private void Examine(List<Token>? tokens)
        {
            Console.WriteLine("You examine the item.");
        }

        private void Go(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }

        private void CustomGo(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere using a custom action.");
        }

        private void Score(List<Token>? tokens)
        {
            Console.WriteLine("Your score is 0.");
        }

        private void Restore(List<Token>? tokens)
        {
            Console.WriteLine("Game restored.");
        }

        private void Restart(List<Token>? tokens)
        {
            Console.WriteLine("Game restarted.");
        }
    }
}
