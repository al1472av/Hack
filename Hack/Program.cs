using System;

namespace Hack


{
    internal class Program : System.Windows.Application
    {
        [STAThread]
        public static void Main(string[] args) =>
            new Program().Run();

        public Program() =>
            Startup += (sender, args) => Initialize();
        

        public void Initialize()
        {
            GameHacker.Initialize();
            new Overlay().Run();
        }
    }
}