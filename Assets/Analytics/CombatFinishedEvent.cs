using Assets.Analytics.Abstract;

namespace Assets.Analytics
{
    internal class CombatFinishedEvent : CharacterRelatedEvent
    {
        public CombatFinishedEvent() : base("CombatFinished")
        {
        }

        public bool PlayerWon { set { SetParameter("PlayerWon", value); } }
    }
}