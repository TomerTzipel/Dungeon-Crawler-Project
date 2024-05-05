using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public abstract class Ticker
    {
        protected readonly object _tickerLock = new object();

        public bool IsActive { get; protected set; } = false;
        protected volatile List<ITickable> _tickables = new List<ITickable>(50);

        public bool IsEmpty
        {
            get
            {
                return _tickables.Count == 0;
            }
        }
        public int Count
        {
            get
            {
                return _tickables.Count;
            }
        }
        protected Stopwatch _tickStopwatch = new Stopwatch();

        protected void AddTickable(ITickable tickable)
        {
            lock (_tickerLock)
            {
                var copy = new List<ITickable>(_tickables);
                copy.Add(tickable);
                _tickables = copy;
            }
        }

        protected void RemoveTickable(ITickable tickable)
        {
            lock (_tickerLock)
            {
                var copy = new List<ITickable>(_tickables);
                copy.Remove(tickable);
                _tickables = copy;
            }
        }

        public void Reset()
        {
            _tickables = new List<ITickable>(50);
            _tickStopwatch.Reset();
            IsActive = false;
        }

        public abstract void Start();
       

        protected void Tick(float interval)
        {
            bool movementResult; bool printFlag = false;

            List<ITickable> local = _tickables;

            foreach (var tickable in local)
            {
                movementResult = tickable.Tick(LevelManager.CurrentLevel.Map, interval);

                if (!printFlag && (movementResult || Printer.ActionTextPrinter.DoesNeedReprint))
                {
                    printFlag = true;
                }
            }
            _tickStopwatch.Restart();

            if (printFlag)
            {
                Printer.PrintLevel();
            }
        }
    }
}
