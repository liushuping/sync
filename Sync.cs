using System;
using System.Collections.Generic;
using System.IO;

namespace sync
{
    class Sync : Handler
    {
        public Sync(Handler next)
            : base(next, false)
        {
        }

        protected override bool ExecuteCore(string input)
        {
            if (input.ToLower() == "ls")
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    Util.PrintLine(string.Format("#{0} - {1}", i, list[i]));
                }

                return true;
            }
            else
            {
                var paras = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (paras.Length < 3)
                {
                    return false;
                }

                var cmd = paras[0];
                var filter = paras[1];
                var srcDir = paras[2];
                var destDir = paras[3];

                if (cmd.ToLower() == "sync")
                {
                    if (Watch(filter, srcDir, destDir))
                    {
                        list.Add(input);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private List<string> list = new List<string>();
        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();

        private bool Watch(string filter, string srcDir, string destDir)
        {
            const string errFormatDirMissing = "Could not find directory {0}";
            if (!Directory.Exists(srcDir))
            {
                Util.PrintLine(string.Format(errFormatDirMissing, srcDir));
                return false;
            }

            if (!Directory.Exists(destDir))
            {
                Util.PrintLine(string.Format(errFormatDirMissing, destDir));
                return false;
            }

            var watcher = new FileSystemWatcher(srcDir, filter);
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.Size |
                NotifyFilters.LastWrite | 
                NotifyFilters.LastAccess | 
                NotifyFilters.FileName | 
                NotifyFilters.CreationTime | 
                NotifyFilters.Attributes;

            watcher.Changed += (o, e) =>
            {
                var filename = Path.Combine(destDir, e.Name);
                var srcFilename = Path.Combine(srcDir, e.Name);
                var count = 5;
                if ((File.GetAttributes(srcFilename) & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    while (count > 0)
                    {
                        try
                        {
                            File.Copy(e.FullPath, filename, true);
                            count = -1;
                        }
                        catch (Exception)
                        {
                            --count;
                        }
                    }
                    Util.PrintLine(string.Format("{0} updated.", filename));    
                }
                
            };

            watcher.EnableRaisingEvents = true;

            watchers.Add(watcher);

            return true;
        }

    }
}
