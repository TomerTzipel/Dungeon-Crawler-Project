using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystems
{

    public enum ActionTextType
    {
        CombatNegative, CombatPositive, Item, Loot, Stun, General, Victory
    }

    public struct ActionText
    {
        public ActionTextType Type { init; get; }
        public string Text { init; get; }

        

        public ActionText(ActionTextType type, string text)
        {
            Type = type;
            Text = text;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case ActionTextType.CombatNegative:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case ActionTextType.Stun:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case ActionTextType.CombatPositive:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case ActionTextType.Item:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case ActionTextType.Loot:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case ActionTextType.Victory:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }
            return Text;
        }
    }

    public class ActionTextPrinter
    {
        private int _numberOfActiveLines;

        private Queue<ActionText> _lines;

        private readonly object _ActionTextPrinterLock = new object();

        public bool DoesNeedReprint { get; private set; } = false;

        public ActionTextPrinter(int numberOfLines)
        {
            _numberOfActiveLines = numberOfLines;
            _lines = new Queue<ActionText>(numberOfLines);
        }

        public void AddLine(ActionText line)
        {
            lock (_ActionTextPrinterLock)
            {
                if (_lines.Count == _numberOfActiveLines)
                {
                    _lines.Dequeue();
                }

                _lines.Enqueue(line);

                DoesNeedReprint = true;
            }
           
        }

        public void Print()
        {
            lock (_ActionTextPrinterLock)
            {
                foreach (var line in _lines)
                {
                    Console.WriteLine(line + "                                                              ");
                    Printer.ColorReset();
                }
                DoesNeedReprint = false;
            }   
        }
    }
}
