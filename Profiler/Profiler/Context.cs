using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Profiler
{
    /// <summary>
    /// This is our internal profiler class where we are going to handle all the informations that need to be kep
    /// This is a singleton
    /// </summary>
    internal sealed class Context
    {
        internal const char CSVSeparator = ';';

        private static volatile Context instance;
        // We are going to use this as a lock in case of multi-threading
        private static object syncRoot = new Object();

        internal Dictionary<int, Dictionary<string, IScope>> dic;

        private Context()
        {
            dic = new Dictionary<int, Dictionary<string, IScope>>();
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

        internal IScope GetScope(string name)
        {
            if (String.IsNullOrEmpty(name))
                return null;
            Dictionary<string, IScope> tmp = GetContextDic();
            IScope scope = null;
            if (tmp != null)
            {
                try
                {
                    scope = tmp[name];
                }
                catch { }
            }
            return scope;
        }

        internal IScope GetLastScope() => GetContextDic()?.Last().Value;

        internal void CloseScope(string name) => ((Scope)(GetScope(name)))?.Close();

        internal void CloseScope(IScope scope) => ((Scope)GetContextDic()?.Where(x => x.Value.Equals(scope)).FirstOrDefault().Value)?.Close();

        internal void CloseScope() => ((Scope)GetContextDic()?.Where(x => ((Scope)x.Value).Closed == false).LastOrDefault().Value)?.Close();

        internal IScope AddNewScopeAndStartIt(string name)
        {
            Dictionary<string, IScope> tmp = GetContextDic();
            // initializing our context in that case
            if (tmp == null)
            {
                dic.Add(Thread.CurrentThread.ManagedThreadId, new Dictionary<string, IScope>() { [name] = new Scope(name) });
            }
            IScope scope = GetScope(name);
            if (scope == null)
            {
                scope = new Scope(name);
                dic[Thread.CurrentThread.ManagedThreadId][name] = scope;
            }
            scope.Start();
            return scope;
        }

        internal Dictionary<string, IScope> GetContextDic()
        {
            Dictionary<string, IScope> tmp;
            dic.TryGetValue(Thread.CurrentThread.ManagedThreadId, out tmp);
            return tmp;
        }

        internal void ExportCSV()
        {
            // we are going to save our CSV export to this path
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), nameof(Profiler));
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            StringBuilder sb = new StringBuilder();
            // header of our CSV
            sb.AppendLine($"Context id{CSVSeparator}Scope name{CSVSeparator}Total elapsed time in ms{CSVSeparator}");
            // Now we write our mesure to the StringBuilder
            List<int> keys = dic.Keys.ToList();
            foreach (int key in keys)
            {
                foreach (KeyValuePair<string, IScope> tmp in dic[key])
                {
                    if (((Scope)tmp.Value).Closed == true)
                    {
                        sb.AppendLine($"{key}{CSVSeparator}{tmp.Key}{CSVSeparator}{((Scope)tmp.Value).Timer.ElapsedMilliseconds.ToString()}{CSVSeparator}");
                    }
                }
            }
            // And we save that to the csv file
            File.WriteAllText($"{folderPath}\\mesures.csv", sb.ToString());
        }
    }
}
