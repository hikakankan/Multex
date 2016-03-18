using System;

namespace Multex
{
	/// <summary>
	/// MVGraphics ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class MVGraphics
	{
		private System.Drawing.Graphics graph;
		private System.Drawing.Font font;
		private System.Drawing.Color col;

		public MVGraphics(System.Drawing.Graphics g, System.Drawing.Font f)
		{
			graph = g;
			font = f;
		}

		public void setColor(MVColor c)
		{
			col = c.get_color();
		}

		public void setColor(System.Drawing.Color c)
		{
			col = c;
		}

		public void drawRect(int x, int y, int width, int height)
		{
			System.Drawing.Pen pen = new System.Drawing.Pen(col);
			graph.DrawRectangle(pen, x, y, width, height);
		}

		public void fillRect(int x, int y, int width, int height)
		{
			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(col);
			graph.FillRectangle(brush, x, y, width, height);
		}

		public void drawRect(System.Drawing.Rectangle rect)
		{
			System.Drawing.Pen pen = new System.Drawing.Pen(col);
			graph.DrawRectangle(pen, rect);
		}

		public void fillRect(System.Drawing.Rectangle rect)
		{
			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(col);
			graph.FillRectangle(brush, rect);
		}

		public void drawRoundRect(int x, int y, int width, int height, int d1, int d2)
		{
			int r1 = d1 / 2;
			int r2 = d2 / 2;
			System.Drawing.Pen pen = new System.Drawing.Pen(col);
			graph.DrawLine(pen, x + r1, y, x + width - r1, y);
			graph.DrawLine(pen, x + r1, y + height, x + width - r1, y + height);
			graph.DrawLine(pen, x, y + r2, x, y + height - r2);
			graph.DrawLine(pen, x + width, y + r2, x + width, y + height - r2);
			graph.DrawArc(pen, x, y, d1, d2, 180, 90);
			graph.DrawArc(pen, x + width - d1, y, d1, d2, 270, 90);
			graph.DrawArc(pen, x, y + height - d2, d1, d2, 90, 90);
			graph.DrawArc(pen, x + width - d1, y + height - d2, d1, d2, 0, 90);
		}

		public void fillRoundRect(int x, int y, int width, int height, int d1, int d2)
		{
			int r1 = d1 / 2;
			int r2 = d2 / 2;
			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(col);
			graph.FillRectangle(brush, x + r1, y, width - d1, r2);
			graph.FillRectangle(brush, x + r1, y + height - r2, width - d1, r2);
			graph.FillRectangle(brush, x, y + r2, r1, height - d2);
			graph.FillRectangle(brush, x + width - r1, y + r2, r1, height - d2);
			graph.FillRectangle(brush, x + r1, y + r2, width - d1, height - d2);
			graph.FillPie(brush, x, y, d1, d2, 180, 90);
			graph.FillPie(brush, x + width - d1, y, d1, d2, 270, 90);
			graph.FillPie(brush, x, y + height - d2, d1, d2, 90, 90);
			graph.FillPie(brush, x + width - d1, y + height - d2, d1, d2, 0, 90);
		}

		public void drawString(String str, int x, int y)
		{
			System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(col);
			graph.DrawString(str, font, brush, x, y);
		}

		public void drawLine(int x1, int y1, int x2, int y2)
		{
			System.Drawing.Pen pen = new System.Drawing.Pen(col);
			graph.DrawLine(pen, x1, y1, x2, y2);
		}

		public void drawOval(int x, int y, int width, int height)
		{
			System.Drawing.Pen pen = new System.Drawing.Pen(col);
			graph.DrawEllipse(pen, x, y, width, height);
		}

//		public MVFont getFontMetrics()
//		{
//			return new MVFont(font, graph);
//		}
	}
}
