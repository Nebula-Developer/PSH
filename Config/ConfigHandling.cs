using System.IO;
using System;

namespace PSH.ConfigHandling {
    public class Config {
        public static Dictionary<string, dynamic>? ReadConfig(String path, bool useAbsPath = false, String commentChar = ";") {
            String configPath = useAbsPath ? path : Path.Combine(AppContext.BaseDirectory, path);
            Dictionary<string, dynamic> config = new Dictionary<string, dynamic>();

            if (File.Exists(configPath)) {
                String[] lines = File.ReadAllText(configPath).Replace("&&","\n").Split("\n");

                foreach (string line in lines) {
                    if (line.StartsWith(commentChar))
                        continue;

                    String outLine = line.Substring(0, line.Contains(commentChar) ? line.IndexOf(commentChar) - 1 : line.Length).Replace(" ", "");
                    if (outLine.Length == 0)
                        continue;

                    if (!outLine.Contains("="))
                        continue;

                    string key = outLine.Substring(0, outLine.IndexOf("="));
                    string value = outLine.Substring(outLine.IndexOf("=") + 1);

                    config.Add(key, ParseStrValue(value));
                    Console.WriteLine($"Config: {key} = {value} of type {config[key].GetType()}");
                }
            } else {
                return null;
            }

            return config;
        }

        public static dynamic? ParseStrValue(String value) {
            String lowValue = value.ToLower();
            
            // BOOLEANS:
            if (lowValue == "yes" || lowValue == "true" || lowValue == "on")
                return true;
            else if (lowValue == "no" || lowValue == "false" || lowValue == "off")
                return false;
            
            // NUMBERS:
            if (int.TryParse(value, out int intValue))
                return intValue;
            else if (double.TryParse(value, out double doubleValue))
                return doubleValue;

            // STRINGS:
            if (value.StartsWith("\"") && value.EndsWith("\""))
                return value.Substring(1, value.Length - 2);
            else if (value.StartsWith("'") && value.EndsWith("'"))
                if (Char.TryParse(value.Substring(1, value.Length - 2), out char charValue))
                    return charValue;
            
            // ARRAYS:
            if (value.StartsWith("[") && value.EndsWith("]")) {
                String arrayStr = value.Substring(1, value.Length - 2);
                String[] array = arrayStr.Split(",");

                // Handle array types:
                List<dynamic> arrayOut = new List<dynamic>();
                foreach (String arrayValue in array) {
                    dynamic? parsedValue = ParseStrValue(arrayValue);
                    Console.WriteLine("added arr value " + parsedValue ?? "null" + " of type " + parsedValue?.GetType() ?? "null");
                    if (parsedValue != null)
                        arrayOut.Add(parsedValue);
                }

                return arrayOut.ToArray();
            }

            return value;
        }
    }
}