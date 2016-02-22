using System;
using System.Collections.Generic;
using System.Linq;

namespace Profiler
{
    /// <summary>
    /// This is our internal profiler class where we are going to handle all the informations that need to be kep
    /// This is a singleton
    /// </summary>
    internal sealed class Context
    {
        private static volatile Context instance;
        // We are going to use this as a lock in case of multi-threading
        private static object syncRoot = new Object();

        internal IList<Scope> Scopes { get; set; }

        private Context()
        {
            Scopes = new List<Scope>();
        }

        internal static Context Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Context();
                    }
                }
                return instance;
            }
        }

        internal IScope GetScope(string name) => Scopes.Where(x => x.Name.Equals(name)).FirstOrDefault();

        internal IScope GetLastScope() => Scopes.Last();

        internal void CloseScope(string name) => Scopes.Where(x => x.Name.Equals(name)).FirstOrDefault()?.Close();

        internal void CloseScope(IScope scope) => Scopes.Where(x => x.Equals(scope)).FirstOrDefault()?.Close();

        internal void CloseScope() => Scopes.LastOrDefault()?.Close();

        internal IScope AddNewScopeAndStartIt(string name)
        {
            Scope scope = (Scope)GetScope(name);
            if (scope == null)
                scope = new Scope(name);
            scope.Start();
            return scope;
        }
    }
}
