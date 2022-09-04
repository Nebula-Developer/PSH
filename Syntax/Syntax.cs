using System;

namespace PSH.Syntax {
    public class PearlSyntax {
        public static List<String> PathItems = new List<String>();
        public static List<String> CustomItems = new List<String>();

        public static void FetchPathItems() {
            String path = Environment.GetEnvironmentVariable("PATH") ?? "";
            String[] PathDirs = path.Split(Environment.OSVersion.Platform == PlatformID.Win32NT ? ';' : ':');

            foreach (String dir in PathDirs) {
                if (Directory.Exists(dir)) {
                    foreach (String file in Directory.GetFiles(dir)) {
                        PathItems.Add(Path.GetFileName(file));
                        Console.WriteLine(Path.GetFileName(file));
                    }
                }
            }
        }
    }
}