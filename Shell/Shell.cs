using System;
using PSH.Input;
using PSH.ConfigHandling;

namespace PSH {
    public class PearlShell {
        public PearlInput ShellInput;
        public Config ShellConfig;

        public PearlShell(PearlInput Input, Config Config) {
            ShellInput = Input;
            ShellConfig = Config;
        }

        public virtual void Init() {
            Console.WriteLine("Pearl Shell v0.0.0");
        }
    }
}