using DrawingToolkit.Interface;
using DrawingToolkit.Object;
using DrawingToolkit.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rectangle = DrawingToolkit.Object.Rectangle;

namespace DrawingToolkit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonColor();
            this.DoubleBuffered = true;
        }
        private bool shouldPaint = false;
        Boolean select;
        Point initial;
        private AObject objectSelected;
        private int posisiClick=-1;
        private ATool toolSelected;
        private LineTool lineTool = new LineTool();
        private CircleTool circleTool = new CircleTool();
        private RectangleTool rectangleTool = new RectangleTool();
        private SelectTool selectTool = new SelectTool();
        private ConnectorTool connectorTool = new ConnectorTool();
        List<AObject> listObject = new List<AObject>();
        private LinkedList<AObject> drawables = new LinkedList<AObject>();

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //deselectObject();
            /*foreach(AObject Object in listObject)
            {
                Object.Deselect();
                Object.Draw();
            }*/
            //base.OnPaint(e);
            foreach (AObject Object in drawables)
            {
                //Object.Deselect();
                Object.Draw();
            }
        }
        
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if(toolSelected==null&&select==false)
            {
                DialogResult box2;
                box2 = MessageBox.Show("Please, Select Shape", "Error", MessageBoxButtons.RetryCancel);
                if (box2 == DialogResult.Cancel)
                {
                    this.Dispose();
                }
            }
            else if (selectTool.isActive == true)
            {
                /*deselectObject();
                initial = e.Location;
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
                }*/
                //if (toolSelected.MouseClick(sender,e,listObject)==true)
                if (toolSelected.MouseClick(sender, e, drawables) == true)
                {
                    //System.Diagnostics.Debug.WriteLine("Bisa");
                    shouldPaint = true;
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Cursor = Cursors.Cross;
            if (e.Button == MouseButtons.Left&&toolSelected!=null)
            {
                shouldPaint = true;
                //toolSelected.MouseDown(sender, e, panel1, listObject);
                toolSelected.MouseDown(sender, e, panel1, drawables);
            }
            /*else if (selectTool.isActive == true && e.Button == MouseButtons.Left)
            {
                toolSelected.MouseDown(sender, e, panel1, listObject);
                //shouldPaint = true;
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
            }*/
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(toolSelected!=null&& shouldPaint==true)
            {
                this.Refresh();
                //toolSelected.MouseMove(sender, e, panel1, listObject);
                toolSelected.MouseMove(sender, e, panel1, drawables);
                this.Invalidate();
            }
            /*else if (select == true&&shouldPaint==true)
            {
                this.Refresh();
                toolSelected.MouseMove(sender, e, panel1, listObject);
                if (posisiClick != -1)
                {
                    System.Diagnostics.Debug.WriteLine("Masukoii");
                    this.Refresh();
                    objectSelected.Resize(posisiClick, e.Location);
                    objectSelected.Draw();
                    objectSelected.DrawEdit();
                    objectSelected.DrawHandle();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Masuk"+posisiClick);
                    this.Refresh();
                    objectSelected.Translate(e.X - initial.X, e.Y - initial.Y);
                    //System.Diagnostics.Debug.WriteLine("objectSelected.from");
                    initial = e.Location;
                    objectSelected.DrawObject();//masalah
                    objectSelected.DrawHandle();
                }
            }*/
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            panel1.Cursor = Cursors.Default;
            if (shouldPaint == true && selectTool.isActive == true)
            {
                shouldPaint = false;
            }
            else if (toolSelected!=null&& shouldPaint==true)
            {
                //listObject.Add(toolSelected.MouseUp(sender, e, panel1, listObject));
                AObject objectPaint= toolSelected.MouseUp(sender, e, panel1, drawables);
                if(objectPaint!=null)
                {
                    drawables.AddLast(toolSelected.MouseUp(sender, e, panel1, drawables));
                }
                shouldPaint = false;
            }
            this.Invalidate();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.KeyCode.ToString() + " pressed.");
            if (toolSelected != null && selectTool.isActive)
            {
                toolSelected.KeyDown(sender, e,panel1);
            }
            //System.Diagnostics.Debug.WriteLine(e.KeyCode.ToString() + " pressed.");
            this.Refresh();
            /*this.Invalidate();*/
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(e.KeyCode.ToString() + " pressed2.");
        }


        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Refresh();
            if (lineTool.isActive==false)
            {
                reset();
                lineTool.isActive = true;
                toolSelected = lineTool;
                buttonColor();
                lineToolStripMenuItem.BackColor = Color.Blue;
            }
            else
            {
                reset();
                toolSelected = null;
                lineTool.isActive = false;
                buttonColor();
            }
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Refresh();
            if (circleTool.isActive == false)
            {
                reset();
                circleTool.isActive = true;
                toolSelected = circleTool;
                buttonColor();
                circleToolStripMenuItem.BackColor = Color.Blue;
            }
            else
            {
                reset();
                toolSelected = null;
                circleTool.isActive = false;
                buttonColor();
            }
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Refresh();
            if (rectangleTool.isActive == false)
            {
                reset();
                rectangleTool.isActive = true;
                toolSelected = rectangleTool;
                buttonColor();
                rectangleToolStripMenuItem.BackColor = Color.Blue;
            }
            else
            {
                reset();
                toolSelected = null;
                rectangleTool.isActive = false;
                buttonColor();
            }
        }

        private void cursorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Refresh();
            if (select == false)
            {
                /*reset();
                toolSelected = null;
                buttonColor();*/
                reset();
                selectTool.isActive = true;
                selectTool.ParentForm = this;
                toolSelected = selectTool;
                buttonColor();
                cursorToolStripMenuItem.BackColor = Color.Blue;
            }
            else
            {
                /*reset();
                toolSelected = null;
                buttonColor();
                select = true;
                cursorToolStripMenuItem.BackColor = Color.Blue;*/
                reset();
                toolSelected = null;
                selectTool.isActive = false;
                buttonColor();
            }
        }

        private void connectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Refresh();
            if (connectorTool.isActive == false)
            {
                reset();
                connectorTool.isActive = true;
                toolSelected = connectorTool;
                buttonColor();
                connectorToolStripMenuItem.BackColor = Color.Blue;
            }
            else
            {
                reset();
                toolSelected = null;
                connectorTool.isActive = false;
                buttonColor();
            }
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)//flip
        {
            if(toolSelected==selectTool&&selectTool.isActive==true)
            {
                AObject selectedObject = selectTool.GetObjectSelected();
                if(selectedObject!=null)
                {
                    selectedObject.FlipVertical((selectedObject.from.X+selectedObject.to.X)/2);
                    Invalidate();
                    Refresh();
                    return;
                }
            }
            //erorr
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)//flip
        {
            if (toolSelected == selectTool && selectTool.isActive == true)
            {
                AObject selectedObject = selectTool.GetObjectSelected();
                if (selectedObject != null)
                {
                    //System.Diagnostics.Debug.WriteLine("Ada Object");

                    selectedObject.FlipHorizontal((selectedObject.from.Y + selectedObject.to.Y) / 2);
                    Invalidate();
                    Refresh();
                    return;
                }
            }
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)//rotate
        {
            if (toolSelected == selectTool && selectTool.isActive == true)
            {
                AObject selectedObject = selectTool.GetObjectSelected();
                if (selectedObject != null)
                {
                    selectedObject.RotateRight((selectedObject.from.X + selectedObject.to.X) / 2, (selectedObject.from.Y + selectedObject.to.Y) / 2);
                    Invalidate();
                    Refresh();
                    return;
                }
            }
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)//rotate
        {
            if (toolSelected == selectTool && selectTool.isActive == true)
            {
                AObject selectedObject = selectTool.GetObjectSelected();
                if (selectedObject != null)
                {
                    selectedObject.RotateLeft((selectedObject.from.X + selectedObject.to.X) / 2, (selectedObject.from.Y + selectedObject.to.Y) / 2);
                    Invalidate();
                    Refresh();
                    return;
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender,EventArgs e)
        {
            reset();
            this.Refresh();
            if (!drawables.Any())
            {
                DialogResult box2;
                box2 = MessageBox.Show("Belum Ada Tindakan", "Error", MessageBoxButtons.RetryCancel);
                if (box2 == DialogResult.Cancel)
                {
                    this.Dispose();
                }
            }
            else
            {
                //listObject.RemoveAt(listObject.Count - 1);
                drawables.RemoveLast();
                this.Refresh();
            }
        }

        private void deselectObject()
        {
            posisiClick = -1;
            /*foreach (AObject Object in listObject)
            {
                Object.Deselect();
                //Object.DrawStatic();
            }*/
            foreach(AObject Object in drawables)
            {
                Object.Deselect();
                Object.Draw();
            }
            this.Refresh();
        }

        void buttonColor()
        {
            lineToolStripMenuItem.BackColor = Color.Snow;
            circleToolStripMenuItem.BackColor = Color.Snow;
            connectorToolStripMenuItem.BackColor = Color.Snow;
            rectangleToolStripMenuItem.BackColor = Color.Snow;
            undoToolStripMenuItem.BackColor = Color.Snow;
            cursorToolStripMenuItem.BackColor = Color.Snow;
        }

        void reset()
        {
            deselectObject();
            circleTool.isActive = false;
            lineTool.isActive = false;
            rectangleTool.isActive = false;
            selectTool.isActive = false;
            toolSelected = null;
            objectSelected = null;
            posisiClick = -1;
            select = false;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to exit ?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            { e.Cancel = true; }
        }

        public void Remove_Object(AObject Object)
        {
            drawables.Remove(Object);
        }

        public void Add_Object(AObject Object)
        {
            drawables.AddLast(Object);
        }
    }
}
