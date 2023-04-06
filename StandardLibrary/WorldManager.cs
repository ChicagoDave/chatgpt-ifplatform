using System;
using System.Runtime.CompilerServices;
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
            _world.AddNode(player.Id, player, GraphProperty.Create("name", player.Name));

            return player;
        }

        public WorldManager CreateLocation(string id, string name, string description, string? targetId = null, Direction? direction = null)
        {
            Location location = new Location(name, description);
            _world.AddNode(id, location, GraphProperty.Create("name",name));

            if (targetId != null && direction != null)
            {
                if (!_world.Nodes.ContainsKey(targetId))
                {
                    throw new ArgumentException($"Node with id '{targetId}' not found.");
                }

                if (direction != null)
                    _world.ConnectNodes(id, targetId, GraphProperty.Create("leadsto", direction.Value.ToString()), GraphProperty.Create("leadsto", GetOppositeDirection(direction.Value).ToString()));
            }

            return this;
        }

        private void CreateThing(Location location, IThing thing)
        {
            _world.AddNode(thing.Id, thing, GraphProperty.Create("name", thing.Name));
            _world.ConnectNodes(thing.Id, location.Id, GraphProperty.Create("relation","IsIn"), GraphProperty.Create("relation", "Contains"));
        }

        private void ConnectLocations(string StartId, string EndId, Direction direction)
        {
            _world.ConnectNodes(StartId, EndId, GraphProperty.Create("leadsto", direction.ToString()), GraphProperty.Create("leadsto", GetOppositeDirection(direction).ToString()));
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
}

