using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace MoreRolesMod
{
    public partial class MoreRolesPlugin
    {

        private async void UpdatReactVersionShowerText(TextRenderer text)
        {
            string strText = "[FFFFFFFF]More Roles Mod v" + Version.ToString(3);
            try
            {

                using (WebClient client = new WebClient())
                {
                    string build = await client.DownloadStringTaskAsync(ArtifactBuildNumberUrl);
                    if (!string.Equals(Version.Build, build))
                    {
                        strText += " ([FF1111FF]Update available[])";
                    }
                }
            }
            catch (WebException ex)
            {
                Log.LogError(string.Format("Failed to retrieve current mod version: {0}", ex.Message));
            }
            text.Text = strText + Environment.NewLine + text.Text; ;

        }
    }
}
