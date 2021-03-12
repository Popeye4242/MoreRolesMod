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

        public static void Postfix(Il2CppReferenceArray<GameData.PlayerInfo> FMAOEJEHPAO)
        {
            System.Console.WriteLine("Resetting Game");
            // make sure the lobby config was loaded before calculating chances
            Rpc<ResetGameRpc>.Instance.Send(data: true, immediately: true);

            int sheriffSpawnChance = Random.Next(0, 100);
            bool shouldSheriffSpawn = sheriffSpawnChance < GameManager.Config.SheriffSpawnChance;
            
            if (shouldSheriffSpawn)
            {
                System.Console.WriteLine("Spawning Sheriff");
                var crewmate = Crewmates.ElementAt(Random.Next(0, Crewmates.Count()));
                Rpc<SetInfectedRpc>.Instance.Send(data: (crewmate, Role.Sheriff), immediately: true);
            }
        }
    }
}
