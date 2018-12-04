using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interface
{
    public interface IObserver//yang mengamati
    {
        void Update(Observerable observable,int difX,int difY);
    }
}
