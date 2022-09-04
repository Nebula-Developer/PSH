using System;

namespace PSH {
    public class PearlShell {
        public static void Main(string[] args) {
            Dictionary<String, dynamic> config = ConfigHandling.Config.ReadConfig("/Volumes/PSH/config.conf", true) ?? new Dictionary<String, dynamic>();
        }
    }
}