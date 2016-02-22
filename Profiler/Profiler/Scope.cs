using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Scope(string name)
        {
            Closed = false;
            Name = name;
        }

        public void Close()
        {
            Timer.Stop();
            Closed = true;
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}
