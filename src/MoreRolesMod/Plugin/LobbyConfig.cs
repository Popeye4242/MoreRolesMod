using Essentials.Options;
using MoreRolesMod.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod
{
    public partial class MoreRolesPlugin
    {
        public CustomNumberOption SheriffSpawnChance = CustomOption.AddNumber(id: nameof(SheriffSpawnChance), name: "Sheriff Spawn Chance", saveValue: true, value: 100f, min: 0f, max: 100f, increment: 10f);
        public CustomNumberOption SheriffKillCooldown = CustomOption.AddNumber(id: nameof(SheriffKillCooldown), name: "Sheriff Kill Cooldown", saveValue: true, value: 30f, min: 10f, max: 60f, increment: 2.5f);

        public LobbyConfig GetLobbyConfig()
        {
            LobbyConfig config = new LobbyConfig();
            config.SheriffSpawnChance = SheriffSpawnChance.GetValue();
            config.SheriffKillCooldown = SheriffKillCooldown.GetValue();
            return config;
        } 
    }
}
