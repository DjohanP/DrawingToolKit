using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.State
{
    public class EditState : ADrawingState
    {
        private static ADrawingState instance;

        public static ADrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new EditState();
            }
            return instance;
        }


        public override void Draw(AObject obj)
        {
            obj.DrawEdit();
        }

        public override void Deselect(AObject obj)
        {
            obj.ChangeState(StaticState.GetInstance());
        }
    }
}
