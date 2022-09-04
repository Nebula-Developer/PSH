using System;

namespace PSH.Input {
    public class PearlInput {
        public virtual string Prefix(bool Color = false) {
            return Directory.GetCurrentDirectory() + "> ";
        }

        public void MoveCaret(int x, String str) {
            x = x < 0 ? 0 : x;
            x = x > str.Length ? str.Length : x;
            x += Prefix().Length;

            Console.SetCursorPosition(x, Console.CursorTop);
        }

        public void Move(int x) {
            Console.SetCursorPosition(x, Console.CursorTop);
        }

        public string input = "";
        public List<String> history = new List<String>();
        public int y = Console.CursorTop;
        public int index = 0;
        public bool reading = true;

        public void SetVariables() {
            this.input = "";
            this.y = Console.CursorTop;
            this.index = 0;
            this.reading = true;
        }

        public void SetY() {
            this.y = Console.CursorTop;
        }

        public void CaretBack() {
            if (this.index > 0) this.index--;
        }

        public void CaretForward() {
            if (this.index < this.input.Length) this.index++;
        }

        public string Read() {
            SetVariables();

            while (reading) {
                Move(0);
                String prefixStr = Prefix() + input;
                Console.Write(prefixStr + new String(' ', Console.WindowWidth - 1 - prefixStr.Length));
                MoveCaret(index, input);

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key) {
                    case ConsoleKey.LeftArrow:
                        CaretBack();
                        break;

                    case ConsoleKey.RightArrow: 
                        CaretForward();
                        break;

                    case ConsoleKey.Enter:
                        reading = false;
                        break;

                    case ConsoleKey.Backspace:
                        if (index > 0) {
                            input = input.Remove(index - 1, 1);
                            CaretBack();
                        }
                        break;

                    default:
                        if ((int)key.KeyChar >= 32 && (int)key.KeyChar <= 126) {
                            input = input.Insert(index, key.KeyChar.ToString());
                            index++;
                        }
                        break;
                }
            }

            return input;
        }
    }
}