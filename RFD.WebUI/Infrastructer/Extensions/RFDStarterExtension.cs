using RFD.Entities.Enum;
using System;
using System.Diagnostics;
using System.IO;

namespace RFD.WebUI.Infrastructer.Extensions
{
    public class RFDStarterExtension : IRFDStarterExtension
    {
        public bool Start(string computerName, int transectionId)
        {
            try
            {
                string appName = "RFDDesktop.exe";
                string appPath = @$"d:/tatbikat";
                string copyPath = $@"\\{computerName}\c$\ProgramData\tatbikat";
                ProcessStartInfo startInfo1 = new ProcessStartInfo("cmd.exe");
                startInfo1.Arguments = $@"/c C:\Windows\System32\robocopy.exe  {appPath} {copyPath} /is";
                startInfo1.Verb = "runas";

               
                startInfo1.UseShellExecute = false;
                startInfo1.CreateNoWindow = true;
                var proc = Process.Start(startInfo1);
                proc.WaitForExit(3000);

                if (File.Exists(copyPath + "\\" + appName))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "/PsExec.exe");
                    string args=$@"\\{computerName} -s -i  ""C:\ProgramData\tatbikat\{appName}"" ""{transectionId}""";
                    startInfo.Arguments = args;

                    //startInfo.Arguments = $@"\\{computerName} -s -i  C:\ProgramData\tatbikat\{appName}";
                    startInfo.UseShellExecute = false;
                    startInfo.Verb = "runas";
                    var proc1 = Process.Start(startInfo);
                    proc1.WaitForExit(3000);
                }

                //ProcessStartInfo startInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "/PsExec.exe");
                //startInfo.Arguments = $@"\\{computerName} -s cmd /c dir";
                //startInfo.UseShellExecute = false;
                //startInfo.Verb = "runas";
                //var proc1 = Process.Start(startInfo);
                //proc1.WaitForExit();


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Start(string computerName,ApplicationType applicationType)
        {
            try
            {
                string appName = "RFDDesktop.exe";
                string appPath = @$"d:/tatbikat";
                string copyPath = $@"\\{computerName}\c$\ProgramData\tatbikat";
                ProcessStartInfo startInfo1 = new ProcessStartInfo("cmd.exe");
                startInfo1.Arguments = $@"/c C:\Windows\System32\robocopy.exe  {appPath} {copyPath} /is";
                startInfo1.Verb = "runas";
                startInfo1.UseShellExecute = false;
                startInfo1.CreateNoWindow = true;
                var proc = Process.Start(startInfo1);
                proc.WaitForExit(3000);

                if (File.Exists(copyPath + "\\" + appName))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "/PsExec.exe");
                    string args = $@"\\{computerName} -s -i  C:\ProgramData\tatbikat\{appName}";
                    startInfo.Arguments = args;

                    //startInfo.Arguments = $@"\\{computerName} -s -i  C:\ProgramData\tatbikat\{appName}";
                    startInfo.UseShellExecute = false;
                    startInfo.Verb = "runas";
                    var proc1 = Process.Start(startInfo);
                    proc1.WaitForExit(3000);
                }

                //ProcessStartInfo startInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "/PsExec.exe");
                //startInfo.Arguments = $@"\\{computerName} -s cmd /c dir";
                //startInfo.UseShellExecute = false;
                //startInfo.Verb = "runas";
                //var proc1 = Process.Start(startInfo);
                //proc1.WaitForExit();


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
