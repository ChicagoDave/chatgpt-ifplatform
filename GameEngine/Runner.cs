using Common;
using ParserLibrary;
using GrammarLibrary;
using StandardLibrary;
using WorldModel;

namespace GameEngine
{
    public class GameEngine
    {
        private IStory _story;
        private Grammar _grammar;
        private Parser _parser;

        public GameEngine(IStory story)
        {
            _story = story;
            _story.InitializeWorld();
            _grammar = new Grammar(_story.World);
            _parser = new Parser(_grammar);
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the game!");

            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                try
                {
                    ParserResults parserResults = _parser.Parse(input);
                    if (parserResults.Success)
                    {
                        parserResults.Action?.Invoke(parserResults.Tokens);
                    }
                    else
                    {
                        if (parserResults.Errors != null & parserResults.Errors.Count > 0)
                            Console.WriteLine(parserResults.Errors[0].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

    }


}
