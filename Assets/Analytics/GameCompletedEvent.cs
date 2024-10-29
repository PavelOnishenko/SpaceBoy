using Assets.Analytics.Abstract;

namespace Assets.Analytics
{
    internal class GameCompletedEvent : LevelRelatedEvent
    {
        public GameCompletedEvent() : base("GameCompleted")
        {
        }
    }
}