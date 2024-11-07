using Assets.Analytics.Abstract;

namespace Assets.Analytics
{
    internal class LevelUnblockedEvent : LevelRelatedEvent
    {
        public LevelUnblockedEvent() : base("LevelUnblocked")
        {
        }
    }
}