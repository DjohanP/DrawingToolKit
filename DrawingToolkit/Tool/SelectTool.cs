using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit.Tool
{
    class SelectTool : ATool
    {
        public bool isActive { set; get; }
        public Point initial;
        public AObject objectSelected;
        private int posisiClick = -1;
        private bool shouldPaint = false;

        public override bool MouseClick(object sender, MouseEventArgs e, List<AObject> listObject)
        {
            initial = e.Location;
            foreach (AObject Object in listObject)
            {
                if (Object.Select(e.Location) == true)
                {
                    //shouldPaint = true;
                    objectSelected = Object;
                    Object.DrawEdit();
                    Object.DrawHandle();
                    //break;
                    return true;
                }
            }
            return false;
            //throw new NotImplementedException();
        }

        public override void MouseDown(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            initial = e.Location;
            if (objectSelected == null)
            {
                System.Diagnostics.Debug.WriteLine("Gak ada");
                foreach (AObject Object in listObject)
                {
                    if (Object.Select(e.Location) == true)
                    {
                        shouldPaint = true;
                        objectSelected = Object;
                        Object.DrawEdit();
                        Object.DrawHandle();
                        break;
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Ada");
                posisiClick = objectSelected.GetClickHandle(e.Location);
                System.Diagnostics.Debug.WriteLine(posisiClick);
            }
            panel1.Invalidate();
        }

        public override void MouseMove(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            throw new NotImplementedException();
        }

        public override AObject MouseUp(object sender, MouseEventArgs e, Panel panel1, List<AObject> listObject)
        {
            throw new NotImplementedException();
        }

        public AObject SelectObject(List<AObject> listObject, MouseEventArgs e)
        {
            foreach (AObject Object in listObject)
            {
                if (Object.Select(e.Location) == true)
                {
                    Object.DrawEdit();
                    Object.DrawHandle();
                    return Object;
                }
            }
            return null;
        }
    }
}
