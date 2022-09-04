using System.IO;
using System;

namespace PSH.ConfigHandling {
    public class Config {
        public static Dictionary<string, string>? ReadConfig(String path, bool useAbsPath = false) {
            String configPath = useAbsPath ? path : Path.Combine(AppContext.BaseDirectory, path);
            Dictionary<string, string> config = new Dictionary<string, string>();

            if (File.Exists(configPath)) {
                foreach (string line in File.ReadLines(configPath)) {
                    if (line.StartsWith("#"))
                        continue;

                    String outLine = line.Substring(0, line.IndexOf("#") - 1).Replace(" ", "");

                    string key = outLine.Substring(0, outLine.IndexOf("="));
                    string value = outLine.Substring(outLine.IndexOf("=") + 1);
                    config.Add(key, value);
                    Console.WriteLine("Added key: " + key + " with value: " + value);
                }
            } else {
                return null;
            }

            return config;
        }
    }
}