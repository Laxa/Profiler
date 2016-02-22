using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiler
{
    /// Mesures scope's speed
    public static class Profiler
    {
        /// Retrieve an IScope by its name in the current context
        /// Param: Scope name
        static IScope Get(string name)
        {
            throw new NotImplementedException();
        }

        /// Start or continue an IScope in the current context
        /// Param: Scope name
        /// Return: The IScope created or continued
        static IScope Start(string name)
        {
            throw new NotImplementedException();
        }

        /// Close the IScope
        /// Param: The scope
        static void Close(IScope scope)
        {
            throw new NotImplementedException();
        }

        /// Close the scope by its name in the current context
        /// Param: Scope name
        static void Close(string name)
        {
            throw new NotImplementedException();
        }

        /// Close the last opened scope
        static void Close()
        {
            throw new NotImplementedException();
        }

        /// Export all measures in a CSV format
        static void ExportCsv()
        {
            throw new NotImplementedException();
        }
    }
}
