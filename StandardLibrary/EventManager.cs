using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary
{
    public class EventManager
    {
        public enum GameEvent
        {
            GameStart,
            PreTurn,
            PostTurn,
            GameEndWin,
            GameEndLose
        }

        private Dictionary<GameEvent, List<Action>> _eventHandlers;

        public EventManager()
        {
            _eventHandlers = new Dictionary<GameEvent, List<Action>>();
            foreach (GameEvent gameEvent in Enum.GetValues(typeof(GameEvent)))
            {
                _eventHandlers[gameEvent] = new List<Action>();
            }
        }

        public void RegisterEventHandler(GameEvent gameEvent, Action handler)
        {
            _eventHandlers[gameEvent].Add(handler);
        }

        public void UnregisterEventHandler(GameEvent gameEvent, Action handler)
        {
            _eventHandlers[gameEvent].Remove(handler);
        }

        public void TriggerEvent(GameEvent gameEvent)
        {
            foreach (var handler in _eventHandlers[gameEvent])
            {
                handler();
            }
        }
    }
}
