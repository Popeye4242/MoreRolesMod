using Essentials.Options;
using MoreRolesMod.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod
{
    public partial class MoreRolesPlugin
    {
        public CustomStringOption GamePreset = CustomOption.AddString(id: nameof(GamePreset), name: DisplayStrings.GamePreset, saveValue: true, values: new[]
        {
            DisplayStrings.Sheriff,
            DisplayStrings.Morphling,
            DisplayStrings.CustomGamePreset
        });

        public CustomNumberOption SheriffSpawnChance = CustomOption.AddNumber(id: nameof(SheriffSpawnChance), name: "Sheriff Spawn Chance", saveValue: true, value: 100f, min: 0f, max: 100f, increment: 10f);

        public CustomNumberOption MorphlingSpawnChance = CustomOption.AddNumber(id: nameof(MorphlingSpawnChance), name: "Morphling Spawn Chance", saveValue: true, value: 100f, min: 0f, max: 100f, increment: 10f);

        public LobbyConfig GetLobbyConfig()
        {
            LobbyConfig config = new LobbyConfig();
            GamePreset.OnValueChanged += OnGamePresetChange;
            config.SheriffSpawnChance = SheriffSpawnChance.GetValue();
            config.MorphlingSpawnChance = MorphlingSpawnChance.GetValue();
            return config;
        }

        private void OnGamePresetChange(object sender, OptionOnValueChangedEventArgs e)
        {
            System.Console.WriteLine(e.Value);
        }
    }
}
