using System.Collections.Generic;
using System.Diagnostics;

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

        public IList<long> Mesures { get; private set; }

        public Scope(string name)
        {
            Closed = false;
            Mesures = new List<long>();
            Name = name;
            Timer = new Stopwatch();
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
            Mesures.Add(Timer.ElapsedMilliseconds);
        }
    }
}
