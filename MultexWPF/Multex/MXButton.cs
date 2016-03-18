using System;
using System.Windows.Forms;

namespace Multex
{
	/// <summary>
	/// MXButton ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class MXButton : System.Windows.Forms.Button
	{
		public MXButton()
		{
		}

		private MXTextBox text_box;
		private char key_type;
		private Keys key_code;
		private char key_char;

		public MXTextBox TextBox
		{
			get
			{
				return text_box;
			}
			set
			{
				text_box = value;
			}
		}

		public char KeyType
		{
			get
			{
				return key_type;
			}
			set
			{
				key_type = value;
			}
		}

		public Keys KeyCode
		{
			get
			{
				return key_code;
			}
			set
			{
				key_code = value;
			}
		}

		public char KeyChar
		{
			get
			{
				return key_char;
			}
			set
			{
				key_char = value;
			}
		}
	}
}
