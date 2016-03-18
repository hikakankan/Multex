using System;
using System.Windows.Forms;

namespace Multex
{
	/// <summary>
	/// KeyItem ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class KeyItem
	{
		private Keys xkey;
		private KeyboardSettings.KeyAction xaction;
		private int xnumber;
		private bool xshift;

		public KeyItem(Keys key, KeyboardSettings.KeyAction action, int number, bool shift)
		{
			xkey = key;
			xaction = action;
			xnumber = number;
			xshift = shift;
		}

		public Keys Key
		{
			get
			{
				return xkey;
			}
		}

		public KeyboardSettings.KeyAction Action
		{
			get
			{
				return xaction;
			}
			set
			{
				xaction = value;
			}
		}

		public int Number
		{
			get
			{
				return xnumber;
			}
			set
			{
				xnumber = value;
			}
		}

		public bool Shift
		{
			get
			{
				return xshift;
			}
		}
	}
}
