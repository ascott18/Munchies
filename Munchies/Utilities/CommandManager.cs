using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Munchies
{
    /// <summary>
    /// A simple class that manages commands and menus for an application.
    /// To implement, override the <c>ProcessCmdKey</c> method on the <c>System.Windows.Forms.Control</c>
    /// that you wish to manage commands for using the proper return method specified by
    /// http://msdn.microsoft.com/en-us/library/system.windows.forms.control.processcmdkey.aspx
    /// </summary>
    public class CommandManager : List<Command>
    {
        public bool ProcessCmdKey(Keys keyData)
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
        /// <summary>
        /// The key combination that will trigger the action
        /// </summary>
        public readonly Keys Keys;

        /// <summary>
        /// The menu item that will trigger the command when clicked
        /// </summary>
        public readonly ToolStripMenuItem MenuItem;

        /// <summary>
        /// The action to be preformed when the command is triggered
        /// </summary>
        public readonly Action Action;


        private Func<bool> enabled;
        /// <summary>
        /// <para>Function that will be called to determine if the command should be triggered.</para>
        /// <para>This controls both key command behavior and menu item enabled state.</para>
        /// </summary>
        public Func<bool> Enabled
        {
            get { return enabled; }
            set
            {
                ToolStripMenuItem item = MenuItem;

                if (item != null)
                {
                    // If this item is a sub-menu,
                    // it should be checked for enabled state when the parent menu item is
                    // shown and when it is painted.
                    if (item.OwnerItem is ToolStripMenuItem)
                    {
                        var owner = (ToolStripMenuItem)item.OwnerItem;
                        owner.Paint += MenuItem_Paint_Enabled;
                        owner.VisibleChanged += MenuItem_Paint_Enabled;
                    }
                    else
					{
						// If this item is a top-level menu,
						// it should be checked for enabled state when the parent toolstrip is
						// shown and when it is painted.

                        var owner = item.Owner;
                        owner.Paint += MenuItem_Paint_Enabled;
                        owner.VisibleChanged += MenuItem_Paint_Enabled;
                    }
                }


                enabled = value;
            }
        }

        private void MenuItem_Paint_Enabled(object sender, EventArgs e)
        {
            MenuItem.Enabled = Enabled();
        }


        private Func<bool> _checked;
        /// <summary>
        /// Function that will be called to determine if the menu item should be checked or not.
        /// </summary>
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

        private void MenuItem_Paint_Checked(object sender, EventArgs e)
        {
            MenuItem.Checked = Checked();
        }

        /// <summary>
        /// Create a new command that is attached to both a menu item and key bindings.
        /// </summary>
        /// <param name="action">The action to be preformed when the command is triggered</param>
        /// <param name="keys">The key combination that will trigger the action</param>
        /// <param name="menuItem">The menu item that will trigger the command when clicked</param>
        public Command(Action action, Keys keys, ToolStripMenuItem menuItem)
        {
            Action = action;
            Keys = keys;
            MenuItem = menuItem;

            MenuItem.Click += MenuItem_Click;
        }

        /// <summary>
        /// Create a new command that is triggered only be key command.
        /// </summary>
        /// <param name="action">The action to be preformed when the command is triggered</param>
        /// <param name="keys">The key combination that will trigger the action</param>
        public Command(Action action, Keys keys)
        {
            Action = action;
            Keys = keys;
        }

        /// <summary>
        /// Create a new command that is triggered only by a menu item.
        /// </summary>
        /// <param name="action">The action to be preformed when the command is triggered</param>
        /// <param name="menuItem">The menu item that will trigger the command when clicked</param>
        public Command(Action action, ToolStripMenuItem menuItem)
        {
            Action = action;
            MenuItem = menuItem;

            MenuItem.Click += MenuItem_Click;
        }


        private void MenuItem_Click(object sender, EventArgs e)
        {
            Action();
        }
    }
}
