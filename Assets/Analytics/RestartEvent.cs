using Assets.Analytics.Abstract;

namespace Assets.Analytics
{
    internal class RestartEvent : CharacterRelatedEvent
    {
        public RestartEvent() : base("Restart")
        {
        }
    }
}