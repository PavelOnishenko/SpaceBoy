using Unity.Services.Analytics;

namespace Assets.Analytics
{
    internal class GameCompletedEvent : Event
    {
        public GameCompletedEvent() : base("GameCompleted")
        {
        }

        public int LevelIndex { set { SetParameter("LevelIndex", value); } }

        public string LevelName { set { SetParameter("LevelName", value); } }
    }
}