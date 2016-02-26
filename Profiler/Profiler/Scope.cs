using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Profiler
{
    /// <summary>
    /// The public implementation of our scope that we will return
    /// </summary>
    internal sealed class Scope : IScope
    {
        public string Name { get; private set; }

        public Stopwatch Timer { get; private set; }

        public bool Closed { get; private set; }

        private object syncRoot = new object();

        public Scope(string name)
        {
            lock (syncRoot)
            {
                Closed = false;
                Name = name;
                Timer = new Stopwatch();
            }
        }

        public void Close()
        {
            lock (syncRoot)
            {
                Timer.Stop();
                Closed = true;
            }
        }

        public void Start()
        {
            lock (syncRoot)
            {
                Closed = false;
                Timer.Start();
            }
        }

        public void Stop()
        {
            lock (syncRoot)
            {
                Timer.Stop();
            }
        }
    }
}
