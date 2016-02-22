using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Profiler;

namespace Tests
{
    [TestClass]
    public class SpecialIScope
    {
        /// <summary>
        /// Testing that we don't crash on other implementation of IScope
        /// </summary>
        [TestMethod]
        public void SpecialIScopeTest()
        {
            SpecialScope t = new SpecialScope();
            Profiler.Profiler.Close(t);
        }
    }

    public class SpecialScope : IScope
    {
        public string TestVariable { get; set; }

        public SpecialScope()
        {
            TestVariable = nameof(SpecialScope);
        }

        public void Start()
        {
            // do stuff
        }

        public void Stop()
        {
            // do stuff
        }
    }
}
