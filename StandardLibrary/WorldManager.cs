using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using WorldModel;

namespace StandardLibrary
{
    public class WorldManager
    {
        private World _world;

        public WorldManager(World world)
        {
            _world = world;
        }

        public Player InitializePlayer(Player player)
        {
            player = new Player("Player", "You are a scruffy adventurer.");
            _world.AddNode(player.Id, player, GraphProperty.Create(Props.Name, player.Name));

            return player;
        }

        public WorldManager AddLocation(string name, string description, string? targetId = null, Direction? direction = null)
        {
            Location location = new Location(name, description);
            string sourceId = IdGenerator.GetBase62();
            _world.AddNode(sourceId, location, GraphProperty.Create(Props.Name, name));

            if (targetId != null && direction != null)
            {
                if (!_world.Nodes.ContainsKey(targetId))
                {
                    throw new ArgumentException($"Node with id '{targetId}' not found.");
                }

                if (direction != null)
                    _world.ConnectNodes(sourceId, targetId, GraphProperty.Create(Props.LeadsTo, direction.Value.ToString()), GraphProperty.Create(Props.LeadsTo, GetOppositeDirection(direction.Value).ToString()));
            }

            return this;
        }

        private void AddThing(Location location, IThing thing)
        {
            _world.AddNode(thing.Id, thing, GraphProperty.Create(Props.Name, thing.Name));
            _world.ConnectNodes(thing.Id, location.Id, GraphProperty.Create(Props.RelatedTo, Props.IsIn), GraphProperty.Create(Props.RelatedTo, Props.Contains));
        }

        private void ConnectLocations(string StartId, string EndId, Direction direction)
        {
            _world.ConnectNodes(StartId, EndId, GraphProperty.Create(Props.LeadsTo, direction.ToString()), GraphProperty.Create(Props.LeadsTo, GetOppositeDirection(direction).ToString()));
        }

        public void SetDirectionFailureResponse(Node location, string direction, string failureResponse)
        {
            location.Properties.Add(GraphProperty.Create(direction + "_failure_response", failureResponse));
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

    }

    public static class Props
    {
        public const string LeadsTo = "LeadsTo";
        public const string RelatedTo = "RelatedTo";
        public const string IsIn = "IsIn";
        public const string Contains = "Contains";
        public const string Name = "Name";
    }
}

