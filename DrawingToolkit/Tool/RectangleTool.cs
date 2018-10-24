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
    class RectangleTool : ATool
    {
        public bool isActive { set; get; }
        private Rectangle rectangleObject;
        public override bool MouseClick(object sender, MouseEventArgs e, List<AObject> listObject)
        {
            System.Diagnostics.Debug.WriteLine("Click");
            return true;
        }

        public override void MouseDown(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            rectangleObject = new Rectangle(e.Location);
            rectangleObject.to = e.Location;
            rectangleObject.setGraphics(panel1.CreateGraphics());
            rectangleObject.Draw();
            panel1.Invalidate();
        }

        public override void MouseMove(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            rectangleObject.to = e.Location;
            rectangleObject.Width = Math.Abs(e.X - rectangleObject.from.X);
            rectangleObject.Height = Math.Abs(e.Y - rectangleObject.from.Y);
            rectangleObject.Draw();
        }

        public override AObject MouseUp(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            rectangleObject.to = e.Location;
            rectangleObject.Width = Math.Abs(e.X - rectangleObject.from.X);
            rectangleObject.Height = Math.Abs(e.Y - rectangleObject.from.Y);
            rectangleObject.Draw();
            return rectangleObject;
        }
    }
}
