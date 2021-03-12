using System.Diagnostics;

namespace Hack
{
    public class ModuleWrapper
    {
        public Process Process { get;}

        public ProcessModule ProcessModule { get;}

        public ModuleWrapper(Process process, ProcessModule processModule)
        {
            Process = process;
            ProcessModule = processModule;
        }

    }
}