using Assets.Analytics.Abstract;

namespace Assets.Analytics
{
    internal class CombatStartedEvent : CharacterRelatedEvent
    {
        public CombatStartedEvent() : base("CombatStarted")
        {
        }
    }
}