using System;

namespace Multex
{
	/// <summary>
	/// MVColor ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class MVColor
	{
		private System.Drawing.Color col;

		public System.Drawing.Color get_color()
		{
			return col;
		}

		public MVColor(int r, int g, int b)
		{
			col = System.Drawing.Color.FromArgb(r, g, b);
		}

		public MVColor(System.Drawing.Color c)
		{
			col = c;
		}

		public int getRed()
		{
			return col.R;
		}

		public int getGreen()
		{
			return col.G;
		}

		public int getBlue()
		{
			return col.B;
		}

		public static MVColor black
		{
			get
			{
				return new MVColor(System.Drawing.Color.Black);
			}
		}

		public static MVColor white
		{
			get
			{
				return new MVColor(System.Drawing.Color.White);
			}
		}
	}
}
