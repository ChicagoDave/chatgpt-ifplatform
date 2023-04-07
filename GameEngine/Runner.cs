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
        private EventManager _eventManager;

        public GameEngine(IStory story)
        {
            _story = story;
            _story.InitializeWorld();
            _grammar = new Grammar(_story.World);
            _parser = new Parser(_grammar, _story.World);
            _eventManager = new EventManager();

            // Subscribe to events
            _eventManager.RegisterEventHandler(EventManager.GameEvent.GameStart, _story.OnGameStart);
            _eventManager.RegisterEventHandler(EventManager.GameEvent.PreTurn, _story.OnPreTurn);
            _eventManager.RegisterEventHandler(EventManager.GameEvent.PostTurn, _story.OnPostTurn);
            _eventManager.RegisterEventHandler(EventManager.GameEvent.GameEndWin, _story.OnGameEndWin);
            _eventManager.RegisterEventHandler(EventManager.GameEvent.GameEndLose, _story.OnGameEndLose);

            // Trigger GameStart event
            _eventManager.TriggerEvent  (EventManager.GameEvent.GameStart);
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the game!");

            while (true)
            {
                // Trigger PreTurn event
                _eventManager.TriggerEvent(EventManager.GameEvent.PreTurn);

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

                // Trigger PostTurn event
                _eventManager.TriggerEvent(EventManager.GameEvent.PostTurn);
            }
        }
    }
}
