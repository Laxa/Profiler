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

        private Context() { }

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


    }
}
