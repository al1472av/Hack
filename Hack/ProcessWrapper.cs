using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hack.Extensions;

namespace Hack
{
    public class ProcessWrapper
    {
        public Process GameProcess { get; }
        public Dictionary<string, ModuleWrapper> ProcessModules;

        public bool IsModulesValid
        {
            get
            {
                if (ProcessModules == null)
                    return false;

                foreach (var m in ProcessModules.Keys)
                {
                    if (m == null)
                        return false;
                }

                return true;
            }
        }

        public ProcessWrapper(string processName, params string[] modules)
        {
            ProcessModules = new Dictionary<string, ModuleWrapper>();
            GameProcess = Process.GetProcessesByName(processName).FirstOrDefault();

            foreach (var m in modules)
            {
                ProcessModules.Add(m, new ModuleWrapper(GameProcess,GameProcess.GetModule(m)));
                if(GameProcess.GetModule(m) == null)
                    Console.WriteLine("null");
            }
            
        }
    }
}