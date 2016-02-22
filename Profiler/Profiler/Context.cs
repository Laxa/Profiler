using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        internal IScope GetScope(string name)
        {
            if (String.IsNullOrEmpty(name))
                return null;
            return Scopes.Where(x => x.Name.Equals(name)).FirstOrDefault();
        }

        internal IScope GetLastScope() => Scopes.Last();

        internal void CloseScope(string name) => Scopes.Where(x => x.Name.Equals(name)).FirstOrDefault()?.Close();

        internal void CloseScope(IScope scope) => Scopes.Where(x => x.Equals(scope)).FirstOrDefault()?.Close();

        internal void CloseScope() => Scopes.LastOrDefault()?.Close();

        internal IScope AddNewScopeAndStartIt(string name)
        {
            Scope scope = (Scope)GetScope(name);
            if (scope == null)
            {
                scope = new Scope(name);
                Scopes.Add(scope);
            }
            scope.Start();
            return scope;
        }

        internal void ExportCSV()
        {
            // we are going to save our CSV export to this path
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), nameof(Profiler));
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            int max = 0;
            foreach (Scope scope in Scopes)
            {
                if (scope.Mesures.Count > max)
                    max = scope.Mesures.Count;
            }
            StringBuilder sb = new StringBuilder();
            // header of our CSV
            sb.Append($"Scope name{CSVSeparator}");
            for (int i = 0; i < max; i++)
                sb.Append($"#{i}{CSVSeparator}");
            sb.AppendLine();
            // Now we write our mesure to the StringBuilder
            foreach (Scope scope in Scopes)
            {
                sb.Append($"{scope.Name}{CSVSeparator}");
                for (int i = 0; i < scope.Mesures.Count; i++)
                    sb.Append($"{scope.Mesures[i]}{CSVSeparator}");
                sb.AppendLine();
            }
            // And we save that to the csv file
            File.WriteAllText($"{folderPath}\\mesures.csv", sb.ToString());
        }
    }
}
