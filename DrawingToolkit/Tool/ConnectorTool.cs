using DrawingToolkit.Command;
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
    class ConnectorTool : ATool
    {
        public bool isActive { set; get; }
        private Connector connectorObject;
        public Form1 ParentForm { get; set; }
        public override void KeyDown(object sender, KeyEventArgs e, Panel panel1)
        {

        }

        public override void KeyUp(object sender, KeyEventArgs e)
        {

        }

        public override bool MouseClick(object sender, MouseEventArgs e, LinkedList<AObject> listObject)
        {
            return false;
        }

        public override void MouseDown(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            this.connectorObject = new Connector(e.Location);
            this.connectorObject.to = e.Location;
            this.connectorObject.setGraphics(panel1.CreateGraphics());
            this.connectorObject.Draw();
            panel1.Invalidate();
        }

        public override void MouseMove(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            this.connectorObject.to = e.Location;
            this.connectorObject.Draw();
        }

        public AObject checkObject(Point location, LinkedList<AObject> listObject)
        {
            foreach (AObject Object in listObject)
            {
                if (Object.Select(location) == true)
                {
                    return Object;
                }
            }
            return null;
        }

        public override AObject MouseUp(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            connectorObject.to = e.Location;
            connectorObject.Deselect();
            connectorObject.Draw();

            connectorObject.first=checkObject(connectorObject.from, listObject);
            connectorObject.last = checkObject(connectorObject.to, listObject);
            
            if (connectorObject.first==null|| connectorObject.last==null||connectorObject.to==connectorObject.from)
            {
                panel1.Invalidate();
                panel1.Refresh();
                return null;
            }
            connectorObject.first.attach(this.connectorObject);
            connectorObject.last.attach(this.connectorObject);

            connectorObject.from = new Point((connectorObject.first.from.X + connectorObject.first.to.X) / 2, (connectorObject.first.from.Y + connectorObject.first.to.Y) / 2);
            connectorObject.to = new Point((connectorObject.last.from.X + connectorObject.last.to.X) / 2, (connectorObject.last.from.Y + connectorObject.last.to.Y) / 2);

            CreateCommand createCommand = new CreateCommand(connectorObject);
            createCommand.ParentForm = ParentForm;
            ParentForm.Add_Command(createCommand);

            panel1.Invalidate();
            //panel1.Refresh();
            return connectorObject;
        }
    }
}
