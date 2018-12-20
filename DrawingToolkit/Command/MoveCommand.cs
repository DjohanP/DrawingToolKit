using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Command
{
    public class MoveCommand : ICommand
    {
        AObject obj;
        int difX, difY;
        public Form1 ParentForm { get; set; }

        public MoveCommand(AObject aObject,int xAwal,int yAwal)
        {
            this.obj = aObject;
            this.difX = xAwal;
            this.difY = yAwal;
        }

        public void Execute()
        {
            obj.Translate(difX, difY);
        }

        public void Undo()
        {
            obj.Translate(-difX, -difY);
        }
    }
}
