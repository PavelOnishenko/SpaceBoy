using Unity.Services.Analytics;

namespace Assets.Analytics
{
    internal class CombatFinishedEvent : Event
    {
        public CombatFinishedEvent() : base("CombatFinished")
        {
        }

        public int LevelIndex { set { SetParameter("LevelIndex", value); } }

        public string LevelName { set { SetParameter("LevelName", value); } }

        public string ProtagonistCharacterName { set { SetParameter("ProtagonistCharacterName", value); } }

        public string EnemyCharacterName { set { SetParameter("EnemyCharacterName", value); } }

        public bool PlayerWon { set { SetParameter("PlayerWon", value); } }
    }
}
