using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Interface
{
    public abstract class ADrawingState
    {
        public ADrawingState State
        {
            get {
                return this.state;
            }
        }

        private ADrawingState state;
        public abstract void Draw(AObject obj);

        public virtual void Deselect(AObject obj)
        {

        }

        public virtual void Select(AObject obj)
        {

        }
    }
}