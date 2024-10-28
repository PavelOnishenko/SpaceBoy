using Unity.Services.Analytics;

namespace Assets.Analytics
{
    internal class ExitToMenuEvent : Event
    {
        public ExitToMenuEvent(string name) : base("ExitToMenu")
        {
        }

        public int LevelIndex { set { SetParameter("LevelIndex", value); } }

        public string LevelName { set { SetParameter("LevelName", value); } }

        public string ProtagonistCharacterName { set { SetParameter("ProtagonistCharacterName", value); } }

        public string EnemyCharacterName { set { SetParameter("EnemyCharacterName", value); } }
    }
}