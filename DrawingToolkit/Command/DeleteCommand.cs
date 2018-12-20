using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Command
{
    class DeleteCommand : ICommand
    {
        public Form1 ParentForm { get; set; }
        AObject obj;

        public DeleteCommand(AObject aObject)
        {
            this.obj = aObject;
        }

        public void Execute()
        {
            ParentForm.Remove_Object(obj);
        }

        public void Undo()
        {
            ParentForm.Add_Object(obj);
        }
    }
}
