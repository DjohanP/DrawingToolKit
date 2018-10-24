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
        public abstract bool MouseClick(object sender, MouseEventArgs e,List<AObject> listObject);
        public abstract void MouseDown(object sender, MouseEventArgs e,Panel panel1, List<AObject> listObject);
        public abstract void MouseMove(object sender, MouseEventArgs e,Panel panel1, List<AObject> listObject);
        public abstract AObject MouseUp(object sender, MouseEventArgs e,Panel panel1, List<AObject> listObject);
    }
}
