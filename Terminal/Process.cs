using System.Diagnostics;

namespace PSH.Terminal {
    public class TermProc {
        public static void Start(string command) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "/bin/bash";
            startInfo.Arguments = "-c \"" + command + "\"";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.RedirectStandardError = false;
            startInfo.RedirectStandardInput = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            process.OutputDataReceived += (sender, e) => {
                if (e.Data != null) {
                    System.Console.WriteLine(e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) => {
                if (e.Data != null) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(e.Data);
                    Console.ResetColor();
                }
            };

            process.WaitForExit();
            process.Close();
            process.Dispose();
        }
    }
}