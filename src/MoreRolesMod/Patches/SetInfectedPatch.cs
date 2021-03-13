using HarmonyLib;
using MoreRolesMod.Roles;
using Reactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnhollowerBaseLib;

namespace MoreRolesMod.Patches
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    public class SetInfectedPatch
    {
        public static Random Random = new Random(Guid.NewGuid().GetHashCode());

        static IEnumerable<PlayerControl> Crewmates => PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsImpostor && !x.HasAnyRole());
        static IEnumerable<PlayerControl> Impostors => PlayerControl.AllPlayerControls.ToArray().Where(x => x.Data.IsImpostor && !x.HasAnyRole());

        public static void Postfix(Il2CppReferenceArray<GameData.PlayerInfo> FMAOEJEHPAO)
        {
            // make sure the lobby config was loaded before calculating chances
            Rpc<ResetGameRpc>.Instance.Send(data: true, immediately: true);


            var roles = new List<(Role Role, float Chance, bool TeamImpostor)>
            {
                (Role.Sheriff, GameManager.Config.SheriffSpawnChance, false),
                (Role.Morphling, GameManager.Config.MorphlingSpawnChance, true)
            };
            foreach (var role in roles)
            {

                int roleSpawnChance = Random.Next(0, 100);
                bool shouldRoleSpawn = roleSpawnChance < role.Chance;

                if (shouldRoleSpawn)
                {
                    var team = role.TeamImpostor ? Impostors : Crewmates;
                    if (!team.Any()) 
                        continue;
                    
                    var lotteryWinner = team.ElementAt(Random.Next(0, team.Count()));
                    Rpc<SetInfectedRpc>.Instance.Send(data: (lotteryWinner, role.Role), immediately: true);
                }
            }
        }
    }
}
