using View.Window;

namespace Signals
{
    public class GameSettingsViewSignals
    {
        public class Apply
        {
            public string Name { get; }

            public GameSettingsViewArgs GameSettingsViewArgs { get; }
            public Apply(string name, GameSettingsViewArgs gameSettingsViewArgs)
            {
                Name = name;

                GameSettingsViewArgs = gameSettingsViewArgs;
            }
        }
    }
}