using Essentials.Options;
using MoreRolesMod.Config;
using MoreRolesMod.Locales;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace MoreRolesMod
{
    public partial class MoreRolesPlugin
    {
        Dictionary<int, (GameMode GameMode, string DisplayString)> Presets = new Dictionary<int, (GameMode GameMode, string DisplayString)>
        {
            { 0, (GameMode: GameMode.Custom, DisplayString: Translations.Custom_Game_Preset) },
            { 1, (GameMode: GameMode.Sheriff, DisplayString: Translations.Sheriff_Game_Preset) },
            { 2, (GameMode: GameMode.Morphling, DisplayString: Translations.Morphling_Game_Preset) }
        };

        public CustomStringOption GamePreset { get; private set; }
        public CustomNumberOption SheriffSpawnChance { get; private set; }
        public CustomNumberOption MorphlingSpawnChance { get; private set; }

        public LobbyConfig GetLobbyConfig()
        {
            LobbyConfig config = new LobbyConfig();
            config.SheriffSpawnChance = SheriffSpawnChance.GetValue();
            config.MorphlingSpawnChance = MorphlingSpawnChance.GetValue();
            return config;
        }

        public void InitializeGameOptions()
        {
            GamePreset = CustomOption.AddString(id: nameof(GamePreset), name: Translations.Game_Preset, saveValue: true, values: Presets.Values.Select(x => x.DisplayString).ToArray());
            SheriffSpawnChance = CustomOption.AddNumber(id: nameof(SheriffSpawnChance), name: Translations.Sheriff_Spawn_Chance, saveValue: true, value: 100f, min: 0f, max: 100f, increment: 10f);
            MorphlingSpawnChance = CustomOption.AddNumber(id: nameof(MorphlingSpawnChance), name: Translations.Morphling_Spawn_Chance, saveValue: true, value: 100f, min: 0f, max: 100f, increment: 10f);
            GamePreset.OnValueChanged += OnGamePresetChange;
        }

        private void OnGamePresetChange(object sender, OptionOnValueChangedEventArgs e)
        {
            DisableAllSettings();
            switch (Presets[(int)e.Value].GameMode)
            {
                case GameMode.Sheriff:
                    EnableSheriffSettings();
                    break;
                case GameMode.Morphling:
                    EnableSheriffSettings();
                    EnableMorphlingSettings();
                    break;
                case GameMode.Custom:
                default:
                    EnableAllSettings();
                    break;
            }
        }

        private void EnableAllSettings()
        {
            EnableSheriffSettings();
            EnableMorphlingSettings();
        }


        private void DisableAllSettings()
        {
            SetOptionEnabled(SheriffSpawnChance, false);
            SetOptionEnabled(MorphlingSpawnChance, false);
        }

        private void SetOptionEnabled(CustomOption option, bool enabled)
        {
            option.HudVisible = enabled;
            option.MenuVisible = enabled;

        }

        private void EnableMorphlingSettings()
        {
            SetOptionEnabled(MorphlingSpawnChance, true);
        }

        private void EnableSheriffSettings()
        {
            SetOptionEnabled(SheriffSpawnChance, true);
        }
    }
}
