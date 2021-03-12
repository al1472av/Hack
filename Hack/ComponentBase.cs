using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Hack
{
    public abstract class ComponentBase : IDisposable
    {
        protected virtual int _sleepTime => 1;
        private Thread _thread;

        protected ComponentBase()
        {
            _thread = new Thread(DoAction);
        }
        
        public void Run() => _thread.Start();

        public void Dispose()
        {
            _thread.Interrupt();
        }

        private void DoAction()
        {
            while (true)
            {
                Action();
                Thread.Sleep(_sleepTime);
            }
        }

        protected abstract void Action();
    }
}