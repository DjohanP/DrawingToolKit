using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Command
{
    public class CommandManager
    {
        Stack<ICommand> commandStacks = new Stack<ICommand>();
        Stack<ICommand> historyStacks = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            //command.Execute();
            commandStacks.Push(command);
            System.Diagnostics.Debug.WriteLine("Stack saat ini init" + commandStacks.Count);
        }

        public void Undo()
        {

            if (commandStacks.Count > 0)
            {
                ICommand command = commandStacks.Pop();
                System.Diagnostics.Debug.WriteLine("Stack saat ini" + commandStacks.Count);
                historyStacks.Push(command);
                command.Undo();
            }
        }

        public void Redo()
        {
            if (historyStacks.Count > 0)
            {
                ICommand command = historyStacks.Pop();
                commandStacks.Push(command);
                command.Execute();
            }
        }
    }
}
