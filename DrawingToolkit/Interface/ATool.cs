using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit.Interface
{
    public abstract class ATool: ToolStripButton
    {
        public abstract bool MouseClick(object sender, MouseEventArgs e, LinkedList<AObject> listObject);
        public abstract void MouseDown(object sender, MouseEventArgs e,Panel panel1, LinkedList<AObject> listObject);
        public abstract void MouseMove(object sender, MouseEventArgs e,Panel panel1, LinkedList<AObject> listObject);
        public abstract AObject MouseUp(object sender, MouseEventArgs e,Panel panel1, LinkedList<AObject> listObject);

        public abstract void KeyUp(object sender, KeyEventArgs e);
        public abstract void KeyDown(object sender, KeyEventArgs e, Panel panel1);

        /*public virtual void KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.KeyChar.ToString() + " pressed.");
        }

        public virtual void KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.KeyCode.ToString() + " pressed2.");
        }*/
    }
}
