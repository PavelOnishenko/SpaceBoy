using Assets.Analytics.Abstract;

namespace Assets.Analytics
{
    internal class ExitToMenuEvent : CharacterRelatedEvent
    {
        public ExitToMenuEvent(string name) : base("ExitToMenu")
        {
        }
    }
}