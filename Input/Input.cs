using System;
using System.Collections.Generic;
using System.Collections;

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

        public void RemoveChar() {
            if (this.index > 0) {
                this.input = this.input.Remove(this.index - 1, 1);
                this.index--;
            }
        }

        public string Read() {
            SetVariables();

            while (reading) {
                Move(0);
                String prefixStr = Prefix() + input;
                Console.Write(prefixStr + new String(' ', Console.WindowWidth - 1 - prefixStr.Length));
                MoveCaret(index, input);

                ConsoleKeyInfo key = Console.ReadKey(true);
                int code = (int)key.KeyChar;

                switch (code) {
                    case 23:
                        if (index <= 0) break;

                        if (input[index - 1] == ' ') {
                            while (index > 0 && input[index - 1] == ' ') {
                                RemoveChar();
                            }
                            break;
                        }

                        // Keep deleting at index until we hit a space
                        while (index > 0 && input[index - 1] != ' ') {
                            RemoveChar();
                        }
                        break;

                    case 21:
                        if (index <= 0) break;

                        while (index > 0) {
                            RemoveChar();
                        }
                        break;
                    
                    case 1:
                        index = 0;
                        break;

                    case 5:
                        index = input.Length;
                        break;
                }

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
                            index--;
                        }
                        break;

                    default:
                        if ((key.KeyChar == 'f' || key.KeyChar == 'b') && key.Modifiers == ConsoleModifiers.Alt) {
                            bool right = key.KeyChar == 'f';
                            
                            if (right) {
                                if (index >= input.Length) break;

                                if (input[index] == ' ') {
                                    while (index < input.Length && input[index] == ' ') {
                                        CaretForward();
                                    }
                                    break;
                                }

                                // Keep moving forward until we hit a space
                                while (index < input.Length && input[index] != ' ') {
                                    CaretForward();
                                }
                            } else {
                                if (index <= 0) break;

                                if (input[index - 1] == ' ') {
                                    while (index > 0 && input[index - 1] == ' ') {
                                        CaretBack();
                                    }
                                    break;
                                }

                                // Keep moving backward until we hit a space
                                while (index > 0 && input[index - 1] != ' ') {
                                    CaretBack();
                                }
                            }
                        } else if ((int)key.KeyChar >= 32 && (int)key.KeyChar <= 126) {
                            input = input.Insert(index, key.KeyChar.ToString());
                            index++;
                        }
                        break;
                }
                Console.WriteLine("\n" + Syntax.PearlSyntax.SearchFileSyntax(input) + "\n");
            }

            return input;
        }
    }
}