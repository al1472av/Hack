using System.Threading;

namespace Hack
{
    public class Aim : ComponentBase
    {
        protected override int _sleepTime => 3;
        
        public Aim(ProcessWrapper processWrapper, string name)
        {
            
        }

        protected override void Action()
        {
            throw new System.NotImplementedException();
        }

        
    }
}