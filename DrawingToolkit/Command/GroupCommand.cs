using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Command
{
    class GroupCommand : ICommand
    {
        public AObject obj;
        public LinkedList<AObject> ObjectGroup;
        public Form1 ParentForm { get; set; }

        public GroupCommand(AObject aObject)
        {
            this.obj = aObject;
        }

        public void Execute()
        {
            foreach (AObject aObject in ObjectGroup)
            {
                aObject.Deselect();
                ParentForm.Remove_Object(aObject);
            }
            ParentForm.Add_Object(obj);
        }

        public void Undo()
        {
            ParentForm.Remove_Object(obj);
            foreach (AObject aObject in ObjectGroup)
            {
                aObject.Deselect();
                ParentForm.Add_Object(aObject);
            }
        }
    }
}
