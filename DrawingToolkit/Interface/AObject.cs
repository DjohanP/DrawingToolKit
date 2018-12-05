using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using DrawingToolkit.State;

namespace DrawingToolkit.Interface
{
    public abstract class AObject: Observerable
    {
        public Point from { get; set; }
        public Point to { get; set; }
        public Point centerPoint { get; set; }
        public ADrawingState State
        {
            get
            {
                return this.state;
            }
        }

        private ADrawingState state;

        private Graphics graphics;

        public AObject()
        {
            this.ChangeState(PreviewState.GetInstance());
        }

        public abstract void DrawObject();
        public abstract void DrawPreview();//preview
        public abstract void DrawEdit();//edit
        public abstract void DrawStatic();//static
        public abstract void DrawHandle();
        public abstract Point GetHandlePoint(int value);
        public abstract int GetClickHandle(Point posisi);//mendapat titik yang diklik
        public abstract Boolean Select(Point posisi);
        public abstract void Translate(int difX,int difY);
        public abstract void Resize(int posisiClick, Point posisi);

        public abstract void FlipVertical(int midY);
        public abstract void FlipHorizontal(int midX);

        public abstract void RotateRight();
        public abstract void RotateLeft();

        public virtual void AddChild(LinkedList<AObject> listChild)
        {

        }

        public virtual LinkedList<AObject> RemoveChild()
        {
            return null;
        }

        /*public abstract void RenderOnPreview();
        public abstract void RenderOnEditingView();
        public abstract void RenderOnStaticView();*/

        public void ChangeState(ADrawingState state)
        {
            this.state = state;
        }

        public virtual void Draw()
        {
            this.state.Draw(this);
        }

        public void Select()
        {
            this.state.Select(this);
        }

        public void Deselect()
        {
            this.state.Deselect(this);
        }

        public virtual void setGraphics(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public Graphics getGraphics()
        {
            return this.graphics;
        }
    }
}
