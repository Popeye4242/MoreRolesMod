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
        Dictionary<GameMode, string> Presets = new Dictionary<GameMode, string>
        {
            { GameMode.Custom,  Translations.Custom_Game_Preset },
            { GameMode.Sheriff,  Translations.Sheriff_Game_Preset },
            { GameMode.Morphling,  Translations.Morphling_Game_Preset }
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
            //CustomOption.RaiseValueChanged(false);
            GamePreset = CustomOption.AddString(id: nameof(GamePreset), name: Translations.Game_Preset, saveValue: true, values: Presets.Values.ToArray());
            GamePreset.SetToDefault(true);
            SheriffSpawnChance = CustomOption.AddNumber(id: nameof(SheriffSpawnChance), name: Translations.Sheriff_Spawn_Chance, saveValue: false, value: 100f, min: 0f, max: 100f, increment: 10f);
            MorphlingSpawnChance = CustomOption.AddNumber(id: nameof(MorphlingSpawnChance), name: Translations.Morphling_Spawn_Chance, saveValue: false, value: 100f, min: 0f, max: 100f, increment: 10f);
            GamePreset.OnValueChanged += OnGamePresetChange;
            foreach (var property in GetType().GetProperties())
            {
                if (typeof(CustomOption).IsAssignableFrom(property.PropertyType))
                {
                    var option = property.GetValue(this) as CustomOption;
                    option.OnValueChanged += (object sender, OptionOnValueChangedEventArgs e) =>
                    {
                        System.Console.WriteLine("{0}: {1}", option.Name, e.Value);
                    };
                }
            }
        }

        private void OnGamePresetChange(object sender, OptionOnValueChangedEventArgs e)
        {
            DisableAllSettings();
            switch ((GameMode)e.Value)
            {
                case GameMode.Sheriff:
                    MakeSheriffGame();
                    break;
                case GameMode.Morphling:
                    MakeMorphlingGame();
                    break;
                case GameMode.Custom:
                default:
                    MakeCustomGame();
                    break;
            }
        }

        private void MakeCustomGame()
        {
            EnableOptions(nameof(MorphlingSpawnChance));
            EnableOptions(nameof(SheriffSpawnChance));
        }

        private void MakeSheriffGame()
        {
            EnableOptions(nameof(SheriffSpawnChance));
        }

        private void MakeMorphlingGame()
        {
            EnableOptions(nameof(MorphlingSpawnChance));
            EnableOptions(nameof(SheriffSpawnChance));
        }

        #region Property Utility
        private void DisableAllSettings()
        {
            DisableOption(nameof(MorphlingSpawnChance));
            DisableOption(nameof(SheriffSpawnChance));
        }

        private void EnableOptions(string propertyName)
        {
            var gameMode = (GameMode)GamePreset.GetValue();
            var prop = GetType().GetProperty(propertyName);
            var option = prop.GetValue(this) as CustomOption;
            if (option == null)
                throw new ArgumentException(propertyName + " is not a custom option");

            var value = GetDefaultValue(gameMode, propertyName);
            option.SetValue(value);
            System.Console.WriteLine("Set {0} to {1}", option.Name, value);
            option.HudVisible = true;
            option.MenuVisible = true;
        }
        private void DisableOption(string propertyName)
        {
            var prop = GetType().GetProperty(propertyName);
            var option = prop.GetValue(this) as CustomOption;
            if (option == null)
                throw new ArgumentException(propertyName + " is not a custom option");

            if (option is CustomStringOption || option is CustomNumberOption)
            {
                option.SetValue(0);
            }
            else if (option is CustomToggleOption)
            {
                option.SetValue(false);
            }

            option.HudVisible = false;
            option.MenuVisible = false;
        }

        private object GetDefaultValue(GameMode gameMode, string propertyName)
        {
            return LobbyConfig.DefaultValues[propertyName][gameMode];
        }
        #endregion
    }
}
