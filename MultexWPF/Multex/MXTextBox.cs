using System;
using System.Windows.Forms;

namespace Multex
{
	/// <summary>
	/// MXTextBox ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class MXTextBox : System.Windows.Forms.TextBox
	{
		public MXTextBox()
		{
			TabStop = false;
			ReadOnly = true;
		}
	}
}
