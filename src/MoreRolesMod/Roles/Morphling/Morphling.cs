using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Roles.Morphling
{
    public static class Morphling
    {
        public static PlayerControl SampledPlayer { get; internal set; }

        public static bool IsMorphed { get; internal set; }
        public static float MorphTime { get; internal set; }
        public static PlayerControl MorphlingPlayer { get; internal set; }

        internal static void ResetGame()
        {
            SampledPlayer = null;
            MorphlingPlayer = null;
            MorphTime = default;
            IsMorphed = false;
        }
    }
}
