using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Command
{
    public class CreateCommand : ICommand
    {
        AObject obj;
        public Form1 ParentForm { get; set; }

        public CreateCommand(AObject aObject)
        {
            this.obj = aObject;
        }

        public void Execute()
        {
            ParentForm.Add_Object(obj);
        }

        public void Undo()
        {
            ParentForm.Remove_Object(obj);
        }
    }
}
