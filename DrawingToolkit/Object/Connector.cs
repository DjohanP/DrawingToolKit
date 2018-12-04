using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Object
{
    class Connector : AObject, IObserver
    {
        private const double EPSILON = 3.0;
        public AObject first;
        public AObject last;
        private Pen p;

        public Connector()
        {
            this.p = new Pen(Color.Black);
            p.Width = 2;
        }

        public Connector(Point awal):this()
        {
            this.from = awal;
        }

        public override void DrawObject()
        {
            this.getGraphics().SmoothingMode = SmoothingMode.AntiAlias;
            this.getGraphics().DrawLine(p, from, to);
        }

        public override void DrawEdit()
        {
            this.p.Color = Color.Blue;
            DrawObject();
        }

        public override void DrawHandle()
        {
            DrawObject();
        }

        public override void DrawPreview()
        {
            this.p.Color = Color.Red;
            DrawObject();
        }

        public override void DrawStatic()
        {
            this.p.Color = Color.Black;
            DrawObject();
        }

        public override int GetClickHandle(Point posisi)
        {
            return -1;
        }

        public override Point GetHandlePoint(int value)
        {
            throw new NotImplementedException();
        }

        public override void Resize(int posisiClick, Point posisi)
        {
            
        }

        public double GetSlope()
        {
            double m = (double)(to.Y - from.Y) / (double)(to.X - from.X);
            return m;
        }

        public override bool Select(Point posisi)
        {
            double m = GetSlope();
            double b = to.Y - m * to.X;
            double y_point = m * posisi.X + b;

            if (Math.Abs(posisi.Y - y_point) < EPSILON)
            {
                return true;
            }
            return false;
        }

        public override void Translate(int difX, int difY)
        {
            
        }

        public void Update(Observerable observable,int difX,int difY)
        {
            if (observable == this.first)
            {
                this.from = new Point(this.from.X + difX, this.from.Y + difY);
            }
            else if (observable == this.last)
            {
                this.to = new Point(this.to.X + difX, this.to.Y + difY);
            }
        }
    }
}
