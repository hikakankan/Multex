using System;
using System.Windows.Forms;

namespace Multex
{
	/// <summary>
	/// MXTextBox の概要の説明です。
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
