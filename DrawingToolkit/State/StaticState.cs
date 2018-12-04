using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.State
{
    public class StaticState : ADrawingState
    {
        private static ADrawingState instance;

        public static ADrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new StaticState();
            }
            return instance;
        }

        public override void Draw(AObject obj)
        {
            obj.DrawStatic();
        }

        public override void Select(AObject obj)
        {
            obj.ChangeState(EditState.GetInstance());
        }
    }
}
