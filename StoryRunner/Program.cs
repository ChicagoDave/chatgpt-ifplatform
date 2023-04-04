using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using StandardLibrary;
using GameEngine;
using Common;

namespace StoryRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: StoryRunner <story DLL path>");
                return;
            }

            string storyPath = args[0];

            // Load the story assembly
            Assembly storyAssembly = Assembly.LoadFrom(storyPath);

            // Find the first type that implements IStory
            Type? storyType = null;
            foreach (var type in storyAssembly.GetTypes())
            {
                if (typeof(IStory).IsAssignableFrom(type))
                {
                    storyType = type;
                    break;
                }
            }

            if (storyType == null)
            {
                Console.WriteLine("Error: Could not find a type that implements IStory in the specified assembly.");
                return;
            }

            // Create the service provider and configure services
            var services = new ServiceCollection();

            services.AddSingleton<IStory>((IStory)Activator.CreateInstance(storyType));
            services.AddSingleton<GameEngine.GameEngine>();

            var serviceProvider = services.BuildServiceProvider();

            // Run the game engine
            var engine = serviceProvider.GetService<GameEngine.GameEngine>();
            if (engine != null)
            {
                engine.Run();
            }
        }
    }
}
