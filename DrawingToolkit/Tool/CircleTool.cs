using DrawingToolkit.Interface;
using DrawingToolkit.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit.Tool
{
    class CircleTool : ATool
    {
        public bool isActive { set; get; }
        private Circle circleObject;

        public override bool MouseClick(object sender, MouseEventArgs e, List<AObject> listObject)
        {
            System.Diagnostics.Debug.WriteLine("Click");
            return true;
        }

        public override void MouseDown(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            circleObject = new Circle(e.Location);
            circleObject.to = e.Location;
            circleObject.setGraphics(panel1.CreateGraphics());
            circleObject.Draw();
            panel1.Invalidate();
        }

        public override void MouseMove(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            circleObject.to = e.Location;
            circleObject.Width = Math.Abs(e.X - circleObject.from.X);
            circleObject.Height = Math.Abs(e.Y - circleObject.from.Y);
            circleObject.Draw();
        }

        public override AObject MouseUp(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            circleObject.to = e.Location;
            circleObject.Width = Math.Abs(e.X - circleObject.from.X);
            circleObject.Height = Math.Abs(e.Y - circleObject.from.Y);
            circleObject.Draw();
            return circleObject;
        }
    }
}
