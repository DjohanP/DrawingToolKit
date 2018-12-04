using DrawingToolkit.Interface;
using DrawingToolkit.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit.Tool
{
    class LineTool : ATool
    {
        public bool isActive { set; get; }
        private Line lineObject;

        public LineTool()
        {
            this.isActive = false;
        }


        public override bool MouseClick(object sender, MouseEventArgs e, LinkedList<AObject> listObject)
        {
            System.Diagnostics.Debug.WriteLine("Click");
            return true;
        }

        public override void MouseDown(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            this.lineObject = new Line(e.Location);
            System.Diagnostics.Debug.WriteLine(lineObject.from);
            this.lineObject.to = (e.Location);
            this.lineObject.setGraphics(panel1.CreateGraphics());
            this.lineObject.Draw();
            panel1.Invalidate();
        }

        public override void MouseMove(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            this.lineObject.to = e.Location;
            this.lineObject.Draw();
        }

        public override AObject MouseUp(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            lineObject.to = e.Location;
            //lineObject.Draw();
            //listObject.Add(lineObject);
            //lineObject.Select();
            lineObject.Deselect();
            lineObject.Draw();
            return lineObject;
        }

        public override void KeyUp(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void KeyDown(object sender, KeyEventArgs e, Panel panel1)
        {
            throw new NotImplementedException();
        }
    }
}
