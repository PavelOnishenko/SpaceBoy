using Unity.Services.Analytics;

namespace Assets.Analytics
{
    internal class LevelUnblockedEvent : Event
    {
        public LevelUnblockedEvent() : base("LevelUnblocked")
        {
        }

        public int LevelIndex { set { SetParameter("LevelIndex", value); } }

        public string LevelName { set { SetParameter("LevelName", value); } }
    }
}
