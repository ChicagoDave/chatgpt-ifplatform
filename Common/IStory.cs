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

        bool UseScoring { get; set; }
        int Score { get; }
        void IncrementScore(int? amount);
        void DecrementScore(int? amount);

        World World { get; }
        Core Core { get; }
        Player Player { get; }
        void InitializeWorld();
    }
}
