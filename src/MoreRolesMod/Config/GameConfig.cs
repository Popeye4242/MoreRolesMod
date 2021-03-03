using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod.Config
{
    public class GameConfig
    {
        #region Custom Properties
        public ConfigEntry<string> Ip { get; set; }
        public ConfigEntry<ushort> Port { get; set; }
        #endregion
    }
}
