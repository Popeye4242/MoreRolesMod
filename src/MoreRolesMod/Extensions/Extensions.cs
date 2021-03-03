using System;
using System.Collections.Generic;
using System.Text;

namespace MoreRolesMod
{
    public static class Extensions
    {
        public static bool IsRpcOfType(this byte rpc, Type enumType)
        {
            return Enum.IsDefined(enumType, rpc);
        }
    }
}
