using DrawingToolkit.Command;
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
        public Form1 ParentForm { get; set; }

        public override bool MouseClick(object sender, MouseEventArgs e, LinkedList<AObject> listObject)
        {
            System.Diagnostics.Debug.WriteLine("Click");
            return true;
        }

        public override void MouseDown(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            circleObject = new Circle(e.Location);
            circleObject.to = e.Location;
            circleObject.setGraphics(panel1.CreateGraphics());
            circleObject.Draw();
            panel1.Invalidate();
        }

        public override void MouseMove(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            circleObject.to = e.Location;
            circleObject.Width = Math.Abs(e.X - circleObject.from.X);
            circleObject.Height = Math.Abs(e.Y - circleObject.from.Y);
            circleObject.Draw();
        }

        public override AObject MouseUp(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            circleObject.to = e.Location;
            circleObject.Width = Math.Abs(e.X - circleObject.from.X);
            circleObject.Height = Math.Abs(e.Y - circleObject.from.Y);

            CreateCommand createCommand = new CreateCommand(circleObject);
            createCommand.ParentForm = ParentForm;
            ParentForm.Add_Command(createCommand);
            //ParentForm.Remove_Object(circleObject);
            
            //circleObject.Select();
            circleObject.Deselect();
            circleObject.Draw();
            return circleObject;
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
