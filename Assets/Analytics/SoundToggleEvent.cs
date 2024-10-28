using Unity.Services.Analytics;

namespace Assets.Analytics
{
    internal class SoundToggleEvent : Event
    {
        public SoundToggleEvent() : base("SoundToggle")
        {
        }

        public bool SoundOn { set { SetParameter("SoundOn", value); } }
    }
}