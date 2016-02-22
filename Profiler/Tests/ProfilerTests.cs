using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Profiler;

namespace Tests
{
    [TestClass]
    public class ProfilerTests
    {
        /// <summary>
        /// Testing that we can't create an IScope with a null string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "We could start an IScope with a null string")]
        public void StartWithNull()
        {
            IScope scope = Profiler.Profiler.Start(null);
        }

        /// <summary>
        /// Testing that we can't create an IScope with String.Empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "We could start an IScope with an empty string")]
        public void StartWithEmptyString()
        {
            IScope scope = Profiler.Profiler.Start(String.Empty);
        }

        /// <summary>
        /// Testing that we can create a IScope
        /// </summary>
        [TestMethod]
        public void StartWithName()
        {
            IScope scope = Profiler.Profiler.Start(nameof(StartWithName));
            Assert.AreNotEqual(scope, null, "We couldn't create an IScope");
        }

        /// <summary>
        /// We can't have scope with different names, this test should return the same scope
        /// </summary>
        [TestMethod]
        public void StartTwiceWithSameName()
        {
            IScope scope = Profiler.Profiler.Start(nameof(StartTwiceWithSameName));
            IScope scope2 = Profiler.Profiler.Start(nameof(StartTwiceWithSameName));
            Assert.AreEqual(scope, scope2, "Start returned 2 different instances instead of the same");
        }

        /// <summary>
        /// Testing the Get method
        /// </summary>
        [TestMethod]
        public void GetWithNullString()
        {
            IScope scope = Profiler.Profiler.Get(null);
            Assert.AreEqual(scope, null, "Get failed to return the proper value with null string parameter");
        }

        /// <summary>
        /// Testing the Get method
        /// </summary>
        [TestMethod]
        public void GetWithEmptyString()
        {
            IScope scope = Profiler.Profiler.Get(String.Empty);
            Assert.AreEqual(scope, null, "Get failed to return the proper value with empty string parameter");
        }

        /// <summary>
        /// Testing the Get method
        /// </summary>
        [TestMethod]
        public void GetWithUnvalidScope()
        {
            IScope scope = Profiler.Profiler.Get(nameof(GetWithUnvalidScope));
            Assert.AreEqual(scope, null, "Get failed to return the proper value while trying to get an IScope that doesn't exist");
        }

        /// <summary>
        /// Testing the Get method
        /// </summary>
        [TestMethod]
        public void GetWithValidScope()
        {
            IScope scope = Profiler.Profiler.Start(nameof(GetWithValidScope));
            IScope scope2 = Profiler.Profiler.Get(nameof(GetWithValidScope));
            Assert.AreNotEqual(scope, null, "Get failed to return a valid IScope");
            Assert.AreNotEqual(scope2, null, "Get failed to return a valid IScope");
            Assert.AreEqual(scope, scope2, "Get failed to return a valid IScope");
        }

        /// <summary>
        /// Testing the Close(IScope scope) method
        /// </summary>
        [TestMethod]
        public void CloseWithNullIScope()
        {
            Profiler.Profiler.Close((IScope)null);
            // Will fail if an exception is thrown
        }

        /// <summary>
        /// Testing the Close(IScope scope) method
        /// </summary>
        [TestMethod]
        public void CloseWithValidIScope()
        {
            IScope scope = Profiler.Profiler.Start(nameof(CloseWithValidIScope));
            Profiler.Profiler.Close(scope);
            // Will fail if an exception is thrown
        }

        /// <summary>
        /// Testing Scope.Start()
        /// </summary>
        [TestMethod]
        public void IScopeCanStart()
        {
            IScope scope = Profiler.Profiler.Start(nameof(IScopeCanStart));
            scope.Start();
            // Will fail if an exception is thrown
        }

        /// <summary>
        /// Testing Scope.Stop()
        /// </summary>
        [TestMethod]
        public void IScopeCanStop()
        {
            IScope scope = Profiler.Profiler.Start(nameof(IScopeCanStop));
            scope.Stop();
            // Will fail if an exception is thrown
        }

        /// <summary>
        /// Testing Close() method
        /// </summary>
        [TestMethod]
        public void CloseLastIScope()
        {
            Profiler.Profiler.Close();
        }

        /// <summary>
        /// Testing Close() method
        /// </summary>
        [TestMethod]
        public void CloseLastScopeWithEmptyString()
        {
            Profiler.Profiler.Close(String.Empty);
            // Will fail if an exception is thrown
        }

        /// <summary>
        /// Testing Close() method
        /// </summary>
        [TestMethod]
        public void CloseLastScopeWithNullString()
        {
            Profiler.Profiler.Close((string)null);
            // Will fail if an exception is thrown
        }

        /// <summary>
        /// Checking Close() method
        /// </summary>
        [TestMethod]
        public void CloseLastIScopeTwo()
        {
            IScope scope = Profiler.Profiler.Start(nameof(CloseLastIScopeTwo));
            for (int i = 0; i < 1000; i++)
                Profiler.Profiler.Close();
        }

        /// <summary>
        /// Test the exportCSV() method
        /// </summary>
        [TestMethod]
        public void ExportCSV()
        {
            Profiler.Profiler.ExportCsv();
        }
    }
}
