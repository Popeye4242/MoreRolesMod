using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace MoreRolesMod
{
    public partial class MoreRolesPlugin
    {

        private void UpdatReactVersionShowerText(TextRenderer text)
        {
            System.Console.WriteLine("Updating version shower");
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            string strText = "[FFFFFFFF]More Roles Mod v" + version.ToString(3);
            try
            {

                using (WebClient client = new WebClient())
                {
                    string build = client.DownloadString(ArtifactBuildNumberUrl);
                    if (!string.Equals(version.Build, build))
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
