using HitachiQA.Driver;
using System;
using System.Diagnostics;
using System.IO;

namespace HitachiQA.Source.Helpers
{
    public sealed class BatchRunner
    {
        public static void ExecuteBatchFile(string batName)
        {
            string command = "/C notepad.exe";
            Process.Start("cmd.exe", command);

            var process = new Process();
            var startinfo = new ProcessStartInfo("cmd.exe");
            startinfo.Verb = "runas";
            startinfo.WorkingDirectory = @"c:\WINDOWS\system32";
            startinfo.RedirectStandardOutput = true;
            startinfo.UseShellExecute = false;
            process.StartInfo = startinfo;
            process.OutputDataReceived += (sender, argsx) => Console.WriteLine(argsx.Data); // do whatever processing you need to do in this handler
            process.Start();
            process.WaitForExit();
            process.BeginOutputReadLine();
            process.WaitForExit();
        }

        public static void Execute(string batName)
        {
            string cmd = $@"C:\Users\acaldarera\source\repos\HitachiQA\Hitachi-QA\HitachiQA\Data\Batch\{batName}.bat";
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = @"C:\Windows\System32",
                    FileName = "cmd.exe",
                    Verb = "runas",
                    CreateNoWindow = false,
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = true,
                    Arguments = cmd,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("output :: " + e.Data);

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("error :: " + e.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }

        //procInfo.FileName = $@"C:\Users\acaldarera\source\repos\HitachiQA\Hitachi-QA\HitachiQA\Data\Batch\{batName}.bat";
    }
}
