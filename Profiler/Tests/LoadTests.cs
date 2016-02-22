using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Profiler;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class LoadTests
    {
        /// <summary>
        /// Testing the load of 1000 scopes
        /// </summary>
        [TestMethod]
        public void LoadTest()
        {
            List<IScope> scopes = new List<IScope>();
            for (int i = 0; i < 1000; i++)
            {
                scopes.Add(Profiler.Profiler.Start($"scope{i}"));
            }
            for (int i = 0; i < 1000; i++)
            {
                scopes[i].Stop();
            }
            for (int i = 0; i < 1000; i++)
            {
                Profiler.Profiler.Close($"scope{i}");
            }
        }

        /// <summary>
        /// Testing the load of 10000 scopes
        /// </summary>
        [TestMethod]
        public void LoadTestTwo()
        {
            List<IScope> scopes = new List<IScope>();
            for (int i = 0; i < 10000; i++)
            {
                scopes.Add(Profiler.Profiler.Start($"scope{i}"));
            }
            for (int i = 0; i < 10000; i++)
            {
                scopes[i].Stop();
            }
            for (int i = 0; i < 10000; i++)
            {
                Profiler.Profiler.Close($"scope{i}");
            }
        }

        /// <summary>
        /// Testing the load of 1000 scopes and exporting to CSV
        /// </summary>
        [TestMethod]
        public void LoadTestAndExport()
        {
            List<IScope> scopes = new List<IScope>();
            for (int i = 0; i < 1000; i++)
            {
                scopes.Add(Profiler.Profiler.Start($"scope{i}"));
            }
            for (int i = 0; i < 1000; i++)
            {
                scopes[i].Stop();
            }
            for (int i = 0; i < 1000; i++)
            {
                Profiler.Profiler.Close($"scope{i}");
            }
            Profiler.Profiler.ExportCsv();
        }

        /// <summary>
        /// Testing the load of 10000 scopes and exporting to CSV
        /// </summary>
        [TestMethod]
        public void LoadTestAndExportTwo()
        {
            List<IScope> scopes = new List<IScope>();
            for (int i = 0; i < 10000; i++)
            {
                scopes.Add(Profiler.Profiler.Start($"scope{i}"));
            }
            for (int i = 0; i < 10000; i++)
            {
                scopes[i].Stop();
            }
            for (int i = 0; i < 10000; i++)
            {
                Profiler.Profiler.Close($"scope{i}");
            }
            Profiler.Profiler.ExportCsv();
        }
    }
}
