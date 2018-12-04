using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interface
{
    public class Observerable//yang diamati
    {
        private LinkedList<IObserver> listObserver = new LinkedList<IObserver>();

        public void attach(IObserver observer)
        {
            listObserver.AddLast(observer);
        }

        public void detach(IObserver observer)
        {
            listObserver.Remove(observer);
        }

        public void notify()
        {
            foreach(IObserver observer in this.listObserver)
            {
                observer.Update(this);
            }
        }
    }
}
