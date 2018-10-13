using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonColor();

        }
        Pen p;
        private Graphics objGraphic;
        private bool shouldPaint = false;
        double px, py, vector, angle;
        Boolean line, rectangle, circle, triangle;
        private Point preCoor, newCoor;
        private List<Point> points = new List<Point>();
        Point a, b, c;
        Point[] list = new Point[100];
        int tebal = 1, initialX, initialY;
        int cirW, cirL;


        class Garis
        {
            public Point from, to;
        }

        class Lingkaran
        {
            public int xAwal, yAwal;
            public Point to;
        }

        class Persegi
        {
            public int xAwal, yAwal;
            public Point to;
        }

        class Stack
        {
            public String bentuk;
        }

        List<Garis> listGaris=new List<Garis>();
        List<Lingkaran> listLingkaran = new List<Lingkaran>();
        List<Persegi> listPersegi = new List<Persegi>();
        List<Stack> listUndo = new List<Stack>();


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            objGraphic = panel1.CreateGraphics();
            foreach(Garis garis in listGaris)
            {
                objGraphic.DrawLine(p, garis.from, garis.to);
            }

            foreach(Lingkaran lingkaran in listLingkaran)
            {
                p.Width = 2;
                cirW = Math.Abs(lingkaran.to.X - lingkaran.xAwal);
                cirL = Math.Abs(lingkaran.to.Y - lingkaran.yAwal);
                Rectangle rec = new Rectangle(Math.Min(lingkaran.to.X, lingkaran.xAwal),
                Math.Min(lingkaran.to.Y, lingkaran.yAwal),
                Math.Abs(lingkaran.to.X - lingkaran.xAwal),
                Math.Abs(lingkaran.to.Y - lingkaran.yAwal));
                objGraphic.DrawEllipse(p, rec);
            }

            foreach (Persegi persegi in listPersegi)
            {
                p.Width = 2;
                int width = persegi.to.X - persegi.xAwal;
                int height = persegi.to.Y - persegi.yAwal;
                Rectangle rect = new Rectangle(Math.Min(persegi.to.X, persegi.xAwal),
                Math.Min(persegi.to.Y, persegi.yAwal),
                Math.Abs(persegi.to.X - persegi.xAwal),
                Math.Abs(persegi.to.Y - persegi.yAwal));
                newCoor = new Point(persegi.to.X, persegi.to.Y);
                objGraphic.DrawRectangle(p, rect);
            }
        }

        void rumusline()
        {
            px = newCoor.X; py = newCoor.Y;
            vector = Math.Sqrt((Math.Pow(px, 2)) + (Math.Pow(py, 2)));
            angle = Math.Atan(py / px) * 180 / Math.PI;
            //display();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (line == false && rectangle == false && circle == false && triangle == false)
            {
                DialogResult box2;
                box2 = MessageBox.Show("Please, Select Shape", "Error", MessageBoxButtons.RetryCancel);
                if (box2 == DialogResult.Cancel)
                {
                    this.Dispose();
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Cursor = Cursors.Cross;
            if (e.Button == MouseButtons.Left)
            {
                if (line == true)
                {
                    shouldPaint = true;
                    preCoor = e.Location;
                    newCoor = preCoor;
                    panel1.Invalidate();
                }
                else if(circle==true||rectangle==true)
                {
                    shouldPaint = true;
                    initialX = e.X;
                    initialY = e.Y;
                    panel1.Invalidate();
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            panel1.Cursor = Cursors.Default;
            if (shouldPaint == true)
            {
                if (line == true)
                {
                    p.Width = 2;
                    ControlPaint.DrawReversibleLine(panel1.PointToScreen(preCoor), panel1.PointToScreen(newCoor), Color.Black);
                    Garis garis = new Garis();
                    garis.from = preCoor;
                    garis.to = newCoor;
                    listGaris.Add(garis);

                    Stack stack = new Stack();
                    stack.bentuk = "line";
                    listUndo.Add(stack);

                    objGraphic.DrawLine(p, preCoor, newCoor);
                    //rumusline();
                    /*DialogResult box2;
                    box2 = MessageBox.Show("Gambar", "Error", MessageBoxButtons.RetryCancel);
                    if (box2 == DialogResult.Cancel)
                    {
                        this.Dispose();
                    }*/
                    shouldPaint = false;
                }
                else if(circle==true)
                {
                    Lingkaran lingkaran = new Lingkaran();
                    lingkaran.xAwal = initialX;
                    lingkaran.yAwal = initialY;
                    lingkaran.to = newCoor;
                    listLingkaran.Add(lingkaran);

                    Stack stack = new Stack();
                    stack.bentuk = "circle";
                    listUndo.Add(stack);

                    shouldPaint = false;
                }
                else if(rectangle==true)
                {
                    Persegi persegi = new Persegi();
                    persegi.xAwal = initialX;
                    persegi.yAwal = initialY;
                    persegi.to = newCoor;
                    listPersegi.Add(persegi);

                    Stack stack = new Stack();
                    stack.bentuk = "rectangle";
                    listUndo.Add(stack);

                    shouldPaint = false;
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (shouldPaint == true)
            {
                if (line == true)
                {
                    this.Refresh();
                    //ControlPaint.DrawReversibleLine(panel1.PointToScreen(preCoor), panel1.PointToScreen(newCoor), Color.Black);
                    newCoor = new Point(e.X, e.Y);
                    ControlPaint.DrawReversibleLine(panel1.PointToScreen(preCoor), panel1.PointToScreen(newCoor), Color.Black);
                }
                else if(circle==true)
                {
                    this.Refresh();
                    p.Width = 2;
                    cirW = Math.Abs(e.X - initialX);
                    cirL = Math.Abs(e.Y - initialY);
                    Rectangle rec = new Rectangle(Math.Min(e.X, initialX),
                    Math.Min(e.Y, initialY),
                    Math.Abs(e.X - initialX),
                    Math.Abs(e.Y - initialY));
                    newCoor = new Point(e.X, e.Y);
                    objGraphic.DrawEllipse(p, rec);
                }
                else if(rectangle==true)
                {
                    this.Refresh();
                    p.Width = 2;
                    int width = e.X - initialX;
                    int height = e.Y - initialY;
                    Rectangle rect = new Rectangle(Math.Min(e.X, initialX),
                    Math.Min(e.Y, initialY),
                    Math.Abs(e.X - initialX),
                    Math.Abs(e.Y - initialY));
                    newCoor = new Point(e.X, e.Y);
                    objGraphic.DrawRectangle(p, rect);
                }
            }
        }


        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(line==true)
            {
                reset();
                buttonColor();
            }
            else
            {
                reset();
                buttonColor();
                line = true;
                lineToolStripMenuItem.BackColor = Color.Blue;
                p = new Pen(Color.Black);
            }
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (circle == true)
            {
                reset();
                buttonColor();
            }
            else
            {
                reset();
                buttonColor();
                circle = true;
                circleToolStripMenuItem.BackColor = Color.Blue;
                p = new Pen(Color.Black);
            }
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rectangle == true)
            {
                reset();
                buttonColor();
            }
            else
            {
                reset();
                buttonColor();
                rectangle = true;
                rectangleToolStripMenuItem.BackColor = Color.Blue;
                p = new Pen(Color.Black);
            }
        }

        private void undoToolStripMenuItem_Click(object sender,EventArgs e)
        {
            if(!listUndo.Any())
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
                String bentukTerakhir = listUndo[listUndo.Count-1].bentuk;
                //DialogResult box2;
                if (bentukTerakhir=="line")
                {
                    listGaris.RemoveAt(listGaris.Count-1);
                    listUndo.RemoveAt(listUndo.Count-1);
                    /*box2 = MessageBox.Show("Line", "Error", MessageBoxButtons.RetryCancel);
                    if (box2 == DialogResult.Cancel)
                    {
                        this.Dispose();
                    }*/
                }
                else if(bentukTerakhir=="circle")
                {
                    listLingkaran.RemoveAt(listLingkaran.Count - 1);
                    listUndo.RemoveAt(listUndo.Count - 1);
                    /*box2 = MessageBox.Show("Circle", "Error", MessageBoxButtons.RetryCancel);
                    if (box2 == DialogResult.Cancel)
                    {
                        this.Dispose();
                    }*/
                }
                else if(bentukTerakhir== "rectangle")
                {
                    listPersegi.RemoveAt(listPersegi.Count - 1);
                    listUndo.RemoveAt(listUndo.Count - 1);
                    /*box2 = MessageBox.Show("Recangle", "Error", MessageBoxButtons.RetryCancel);
                    if (box2 == DialogResult.Cancel)
                    {
                        this.Dispose();
                    }*/
                }
                this.Refresh();
            }
        }

        void buttonColor()
        {
            lineToolStripMenuItem.BackColor = Color.Snow;
            circleToolStripMenuItem.BackColor = Color.Snow;
            rectangleToolStripMenuItem.BackColor = Color.Snow;
            undoToolStripMenuItem.BackColor = Color.Snow;
        }

        void reset()
        {
            line = false;
            rectangle = false;
            circle = false;
            triangle = false;
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
    }
}
