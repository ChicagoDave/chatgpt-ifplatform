using Common;

namespace GameEngine
{
    public class GameEngine
    {
        private IStory _story;

        public GameEngine(IStory story)
        {
            _story = story;
            _story.InitializeWorld();
        }

        public void Run()
        {
            // Your game loop implementation here
        }
    }


}
