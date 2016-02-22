using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private IList<Scope> Scopes { get; set; }

        private Context()
        {
            Scopes = new List<Scope>();
        }

        public static Context Instance
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

        public IScope GetScope(string name) => Scopes.Where(x => x.Name.Equals(name)).FirstOrDefault();

        public IScope GetLastScope() => Scopes.Last();

        public void CloseScope(string name) => Scopes.Where(x => x.Name.Equals(name)).FirstOrDefault()?.Close();

        public void CloseScope(IScope scope) => Scopes.Where(x => x.Equals(scope)).FirstOrDefault()?.Close();

        public void CloseScope() => Scopes.LastOrDefault()?.Close();

        public IScope AddNewScopeAndStartIt(string name)
        {
            Scope scope = (Scope)GetScope(name);
            if (scope == null)
                scope = new Scope(name);
            scope.Start();
            return scope;
        }
    }
}
