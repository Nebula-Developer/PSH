using System;
using PSH.Input;
using PSH.ConfigHandling;

namespace PSH {
    public class Pearl {
        /// <summary>
        /// Current (and only) shell.
        /// </summary>
        public static PearlShell ShellInstance = new PearlShell(new PearlInput(), new Config());

        public static void Main(string[] args) {
            ShellInstance.Init();
        }
    }
}