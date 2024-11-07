using Unity.Services.Analytics;

namespace Assets.Analytics.Abstract
{
    public abstract class LevelRelatedEvent : Event
    {
        protected LevelRelatedEvent(string name) : base(name)
        {
        }

        public int LevelIndex { set { SetParameter("LevelIndex", value); } }

        public string LevelName { set { SetParameter("LevelName", value); } }
    }
}
