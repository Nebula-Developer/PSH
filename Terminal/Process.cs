using System.Diagnostics;

namespace PSH.Terminal {
    public class TermProc {
        public static void Start(string command) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "/bin/bash";
            startInfo.Arguments = "-c \"" + command + "\"";
            startInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            process.OutputDataReceived += (sender, e) => {
                Console.Out.Flush();
                if (e.Data != null) {
                    Console.Out.Write(e.Data);
                    Console.Out.Write("\n\n");
                    Console.Out.Flush();
                }
            };

            process.ErrorDataReceived += (sender, e) => {
                Console.Out.Flush();
                if (e.Data != null) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Out.Write(e.Data);
                    Console.Out.Write("\n");
                    Console.Out.Flush();
                    Console.ResetColor();
                }
            };
            process.WaitForExit();
            process.Close();
            process.Dispose();
        }
    }
}