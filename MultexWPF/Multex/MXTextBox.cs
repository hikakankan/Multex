using System;
using System.Windows.Forms;

namespace Multex
{
	/// <summary>
	/// MXTextBox �̊T�v�̐����ł��B
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
