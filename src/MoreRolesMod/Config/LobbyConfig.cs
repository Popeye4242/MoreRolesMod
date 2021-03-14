using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Config
{
    public class LobbyConfig
    {

        public static IReadOnlyDictionary<string, IReadOnlyDictionary<GameMode, object>> DefaultValues { get; } = new Dictionary<string, IReadOnlyDictionary<GameMode, object>>
        {
            {
                nameof(SheriffSpawnChance), new Dictionary<GameMode, object>
                {
                    {GameMode.Custom, 100 },
                    {GameMode.Sheriff, 100 },
                    {GameMode.Morphling, 100 }
                }
            },
            {
                nameof(MorphlingSpawnChance), new Dictionary<GameMode, object>
                {
                    {GameMode.Custom, 100 },
                    {GameMode.Sheriff, 0 },
                    {GameMode.Morphling, 100 }
                }
            }
        };
        public float SheriffSpawnChance { get; internal set; }

        public float MorphlingSpawnChance { get; internal set; }
    }
}
