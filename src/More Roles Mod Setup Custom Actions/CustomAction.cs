using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace More_Roles_Mod_Setup_Custom_Actions
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult SetTestEnvironmentVariable(Session session)
        {
            session.Log("Begin CustomAction1");
            using (var file = File.CreateText("D:\test.txt"))
            {
                file.WriteLine("text");
                file.Flush();
            }
            Environment.SetEnvironmentVariable("test", DateTime.Now.ToString(), EnvironmentVariableTarget.Machine);
            return ActionResult.Success;
        }
    }
}
