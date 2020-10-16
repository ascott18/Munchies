using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Munchies
{
	/// <summary>
	///     Serves as a common base for all Content Containers.
	/// </summary>
	public abstract partial class ContentContainer : UserControl
	{
		public ContentContainer()
		{
			InitializeComponent();

			SetStyle(ControlStyles.Selectable, false);
		}
    }
}
