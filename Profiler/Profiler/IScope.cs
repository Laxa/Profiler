using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiler
{
    interface IScope
    {
        /// <summary>
        /// Start the scope
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the scope
        /// </summary>
        void Stop();
    }
}
