using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndrewScott.SimpleCommandManager
{
    /// <summary>
    /// A simple class that manages commands and menus for an application.
    /// To implement, override the <c>ProcessCmdKey</c> method on the <c>System.Windows.Forms.Control</c>
    /// that you wish to manage commands for using the proper return method specified by
    /// http://msdn.microsoft.com/en-us/library/system.windows.forms.control.processcmdkey.aspx
    /// </summary>
    public class CommandManager : List<Command>
    {
        public bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool processed = false;

            foreach (Command command in this.Where(c => 
                (keyData == c.Keys)
                && (c.Enabled == null || c.Enabled())
            ))
            {
                command.Action();

                processed = true;
            }

            return processed;
        }
    }

    public class Command
    {
        public readonly Keys Keys;
        public readonly ToolStripMenuItem MenuItem;
        public readonly Action Action;

        private Func<bool> enabled;
        public Func<bool> Enabled
        {
            get { return enabled; }
            set
            {
                if (MenuItem != null)
                    MenuItem.Paint += MenuItem_Paint_Enabled;

                enabled = value;
            }
        }

        void MenuItem_Paint_Enabled(object sender, EventArgs e)
        {
            MenuItem.Enabled = Enabled();
        }


        private Func<bool> _checked;
        public Func<bool> Checked
        {
            get { return _checked; }
            set
            {
                if (MenuItem != null)
                    MenuItem.Paint += MenuItem_Paint_Checked;

                _checked = value;
            }
        }

        void MenuItem_Paint_Checked(object sender, EventArgs e)
        {
            MenuItem.Checked = Checked();
        }


        public Command(Action action, Keys keys, ToolStripMenuItem menuItem)
        {
            Action = action;
            Keys = keys;
            MenuItem = menuItem;

            MenuItem.Click += MenuItem_Click;
        }

        public Command(Action action, Keys keys)
        {
            Action = action;
            Keys = keys;
        }

        public Command(Action action, ToolStripMenuItem menuItem)
        {
            Action = action;
            MenuItem = menuItem;

            MenuItem.Click += MenuItem_Click;
        }


        void MenuItem_Click(object sender, EventArgs e)
        {
            Action();
        }
    }
}
