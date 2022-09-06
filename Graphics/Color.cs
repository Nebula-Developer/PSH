using System;

namespace PSH.Graphics {
    public class RGB {
        public int R, G, B;

        public RGB(int r, int g, int b) {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public string ToEscape() {
            return $"\x1b[38;2;{this.R};{this.G};{this.B}m";
        }

        public static string CreateEsc(int r, int g, int b) {
            return $"\x1b[38;2;{r};{g};{b}m";
        } 
    }
}