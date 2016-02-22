using System;

namespace Profiler
{
    /// Mesures scope's speed
    public static class Profiler
    {
        /// Retrieve an IScope by its name in the current context
        /// Param: Scope name
        public static IScope Get(string name) => Context.Instance.GetScope(name);

        /// Start or continue an IScope in the current context
        /// Param: Scope name
        /// Return: The IScope created or continued
        public static IScope Start(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name needs to be a valid string");
            }
            return Context.Instance.AddNewScopeAndStartIt(name);
        }

        /// Close the IScope
        /// Param: The scope
        public static void Close(IScope scope) => Context.Instance.CloseScope(scope);

        /// Close the scope by its name in the current context
        /// Param: Scope name
        public static void Close(string name) => Context.Instance.CloseScope(name);

        /// Close the last opened scope
        public static void Close() => Context.Instance.CloseScope();

        /// Export all measures in a CSV format
        public static void ExportCsv() => Context.Instance.ExportCSV();
    }
}
