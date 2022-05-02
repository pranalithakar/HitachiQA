using System;
using System.Diagnostics;
using System.IO;

namespace HitachiQA.Source.Helpers
{
    public sealed class BatchRunner
    {
        public static void ExecuteBatchFile(string batName)
        {
            string command = $"/C {batName}.bat";
            string dirName = GetCurrentProjectDirectory();
            ProcessStartInfo procInfo = new ProcessStartInfo()
            {
                Verb = "runas",
                WorkingDirectory = dirName,
                FileName = "cmd.exe",
                Arguments = command,
            };
            var proc = Process.Start(procInfo);
            do { proc.WaitForExit();
            } while (proc.HasExited != true);  
            if(proc.ExitCode != 0)
            {
                throw new Exception($"Batch File Execution Error" + proc.ExitCode);
            }
        }

        private static string GetCurrentProjectDirectory()
        {
            string startPath = Directory.GetCurrentDirectory();
            string ProjectPath = startPath.Substring(0, 63);
            string editedProjectPath = ProjectPath + "\\Data\\Batch";
            return editedProjectPath;
        }
    }
}
