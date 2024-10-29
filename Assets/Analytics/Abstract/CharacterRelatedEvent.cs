namespace Assets.Analytics.Abstract
{
    internal abstract class CharacterRelatedEvent : LevelRelatedEvent
    {
        protected CharacterRelatedEvent(string name) : base(name)
        {
        }

        public string ProtagonistCharacterName { set { SetParameter("ProtagonistCharacterName", value); } }

        public string EnemyCharacterName { set { SetParameter("EnemyCharacterName", value); } }
    }
}