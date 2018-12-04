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
    class GroupObject : AObject
    {
        //public Point from { get; set; }
        //public Point to { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private LinkedList<AObject> childs = new LinkedList<AObject>();
        private Pen p;

        public GroupObject()
        {
            this.p = new Pen(Color.Black);
            p.Width = 2;
            this.from = new Point(int.MaxValue, int.MaxValue);
            this.to = new Point(int.MinValue, int.MinValue);
            this.Width = 0;
            this.Height = 0;
        }

        public override void AddChild(LinkedList<AObject> listChild)
        {
            foreach(AObject aObject in listChild)
            {
                if(this.from.X>aObject.from.X)
                {
                    this.from = new Point(aObject.from.X,this.from.Y);
                }
                if (this.from.Y > aObject.from.Y)
                {
                    this.from = new Point(this.from.X, aObject.from.Y);
                }
                if (this.to.X < aObject.to.X)
                {
                    this.to = new Point(aObject.to.X, this.to.Y);
                }
                if (this.to.Y < aObject.to.Y)
                {
                    this.to = new Point(this.to.X, aObject.to.Y);
                }
                childs.AddLast(aObject);
            }
            this.Width = Math.Abs(from.X - to.X);
            this.Height = Math.Abs(from.Y - to.Y);
            //System.Diagnostics.Debug.WriteLine("Baru ini Dari" + this.from + "\nSampai:" + this.to);
        }

        public override LinkedList<AObject> RemoveChild()
        {
            return childs;
        }

        public override void DrawObject()
        {
            foreach (AObject aObject in childs)
            {
                aObject.Draw();
            }
            //System.Diagnostics.Debug.WriteLine("Width" + width + "Height" + height);
            //System.Diagnostics.Debug.WriteLine("Gambar ini Dari" + this.from + "\nSampai:" + this.to);
        }

        public override void DrawEdit()
        {
            DrawObject();
            this.p.Color = Color.Blue;
            DrawHandle();
            //DrawObject();
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
            //throw new NotImplementedException();
            return -1;
        }

        public override Point GetHandlePoint(int value)
        {
            throw new NotImplementedException();
        }

        public override void DrawHandle()
        {
            this.p.Color = Color.Blue;
            int width = to.X - from.X;
            int height = to.Y - from.Y;
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(Math.Min(to.X, from.X),
                                                Math.Min(to.Y, from.Y),
                                                Math.Abs(to.X - from.X),
                                                Math.Abs(to.Y - from.Y));
            this.getGraphics().SmoothingMode = SmoothingMode.AntiAlias;
            this.getGraphics().DrawRectangle(p, rect);
        }

        public override void Resize(int posisiClick, Point posisi)
        {
            throw new NotImplementedException();
        }

        public override bool Select(Point posisi)
        {
            /*if ((posisi.X >= from.X && posisi.X <= from.X + Width) && (posisi.Y >= from.Y && posisi.Y <= from.Y + Height))
            {
                //System.Diagnostics.Debug.WriteLine("Kotak Terpilih");
                return true;
            }*/
            foreach (AObject aObject in childs)
            {
                if(aObject.Select(posisi))
                {
                    //System.Diagnostics.Debug.WriteLine("Kotak Terpilih");
                    return true;
                }
            }
            return false;
            /*if ((posisi.X >= from.X && posisi.X <= from.X + Width) && (posisi.Y >= from.Y && posisi.Y <= from.Y + Height))
            {
                System.Diagnostics.Debug.WriteLine("Kotak Terpilih");
                return true;
            }*/
            System.Diagnostics.Debug.WriteLine("Kotak Tidak Terpilih");
            return false;
        }

        public override void Translate(int difX, int difY)
        {
            //throw new NotImplementedException();
            foreach(AObject aObject in childs)
            {
                aObject.Translate(difX,difY);
            }
            this.from = new Point(this.from.X + difX, this.from.Y + difY);
            this.to = new Point(this.to.X + difX, this.to.Y + difY);
        }
    }
}
