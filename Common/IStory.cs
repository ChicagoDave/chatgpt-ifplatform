using StandardLibrary;
using WorldModel;

namespace Common
{
    public interface IStory
    {
        void OnGameStart();
        void OnPreTurn();
        void OnPostTurn();
        void OnGameEndWin();
        void OnGameEndLose();
        World World { get; }
        Core Core { get; }
        Player Player { get; }
        void InitializeWorld();
    }
}
