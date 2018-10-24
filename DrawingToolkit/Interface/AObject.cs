using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;

namespace DrawingToolkit.Interface
{
    public abstract class AObject
    {
        private Graphics graphics;
        public abstract void Draw();
        public abstract void DrawEdit();
        public abstract void DrawStatic();
        public abstract void DrawHandle();
        public abstract Point GetHandlePoint(int value);
        public abstract int GetClickHandle(Point posisi);//mendapat titik yang diklik
        public abstract Boolean Select(Point posisi);
        public abstract void Translate(int difX,int difY);
        public abstract void Resize(int posisiClick, Point posisi);


        public virtual void setGraphics(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public Graphics getGraphics()
        {
            return this.graphics;
        }
    }
}
