using System;
using java.applet;
using java.awt;
using java.awt.event;

namespace FractalDiagram
{
	public class DigitButton : Panel implements MouseListener
	{
		private int hcount;
		private int vcount;
		private int dhcount;
		private int dvcount;
		private int number;
		private bool lock;
		private bool frm;
		private string sym;
		private bool ul;
		private string[][] pattern;
		private string[] pattern_add;
		private string[] pattern_sub;
		private string[] pattern_mult;
		private string[] pattern_div;
		private string[] pattern_ast;

		public DigitButton()
		{
			hcount = 3;
			vcount = 4;
			dhcount = 6;
			dvcount = 8;
			number = -1;
			lock = false;
			frm = false;
			sym = "";
			pattern = new string[10][];
			pattern[0] = new string[8];
			pattern[1] = new string[8];
			pattern[2] = new string[8];
			pattern[3] = new string[8];
			pattern[4] = new string[8];
			pattern[5] = new string[8];
			pattern[6] = new string[8];
			pattern[7] = new string[8];
			pattern[8] = new string[8];
			pattern[9] = new string[8];

			pattern[0][0] = "000000";
			pattern[0][1] = "001100";
			pattern[0][2] = "010010";
			pattern[0][3] = "010010";
			pattern[0][4] = "010010";
			pattern[0][5] = "010010";
			pattern[0][6] = "010010";
			pattern[0][7] = "001100";

			pattern[1][0] = "000000";
			pattern[1][1] = "000100";
			pattern[1][2] = "001100";
			pattern[1][3] = "000100";
			pattern[1][4] = "000100";
			pattern[1][5] = "000100";
			pattern[1][6] = "000100";
			pattern[1][7] = "001110";

			pattern[2][0] = "000000";
			pattern[2][1] = "001100";
			pattern[2][2] = "010010";
			pattern[2][3] = "010010";
			pattern[2][4] = "000100";
			pattern[2][5] = "001000";
			pattern[2][6] = "010000";
			pattern[2][7] = "011110";

			pattern[3][0] = "000000";
			pattern[3][1] = "001100";
			pattern[3][2] = "010010";
			pattern[3][3] = "000010";
			pattern[3][4] = "001100";
			pattern[3][5] = "000010";
			pattern[3][6] = "010010";
			pattern[3][7] = "001100";

			pattern[4][0] = "000000";
			pattern[4][1] = "000100";
			pattern[4][2] = "001100";
			pattern[4][3] = "010100";
			pattern[4][4] = "010100";
			pattern[4][5] = "011110";
			pattern[4][6] = "000100";
			pattern[4][7] = "000100";

			pattern[5][0] = "000000";
			pattern[5][1] = "011110";
			pattern[5][2] = "010000";
			pattern[5][3] = "011100";
			pattern[5][4] = "000010";
			pattern[5][5] = "000010";
			pattern[5][6] = "010010";
			pattern[5][7] = "001100";

			pattern[6][0] = "000000";
			pattern[6][1] = "001100";
			pattern[6][2] = "010010";
			pattern[6][3] = "010000";
			pattern[6][4] = "011100";
			pattern[6][5] = "010010";
			pattern[6][6] = "010010";
			pattern[6][7] = "001100";

			pattern[7][0] = "000000";
			pattern[7][1] = "011110";
			pattern[7][2] = "000010";
			pattern[7][3] = "000010";
			pattern[7][4] = "000100";
			pattern[7][5] = "000100";
			pattern[7][6] = "001000";
			pattern[7][7] = "001000";

			pattern[8][0] = "000000";
			pattern[8][1] = "001100";
			pattern[8][2] = "010010";
			pattern[8][3] = "010010";
			pattern[8][4] = "001100";
			pattern[8][5] = "010010";
			pattern[8][6] = "010010";
			pattern[8][7] = "001100";

			pattern[9][0] = "000000";
			pattern[9][1] = "001100";
			pattern[9][2] = "010010";
			pattern[9][3] = "010010";
			pattern[9][4] = "001110";
			pattern[9][5] = "000010";
			pattern[9][6] = "010010";
			pattern[9][7] = "001100";

			pattern_add = new string[8];
			pattern_add[0] = "000000";
			pattern_add[1] = "000000";
			pattern_add[2] = "000100";
			pattern_add[3] = "000100";
			pattern_add[4] = "011111";
			pattern_add[5] = "000100";
			pattern_add[6] = "000100";
			pattern_add[7] = "000000";

			pattern_sub = new string[8];
			pattern_sub[0] = "000000";
			pattern_sub[1] = "000000";
			pattern_sub[2] = "000000";
			pattern_sub[3] = "000000";
			pattern_sub[4] = "011111";
			pattern_sub[5] = "000000";
			pattern_sub[6] = "000000";
			pattern_sub[7] = "000000";

			pattern_mult = new string[8];
			pattern_mult[0] = "000000";
			pattern_mult[1] = "000000";
			pattern_mult[2] = "010001";
			pattern_mult[3] = "001010";
			pattern_mult[4] = "000100";
			pattern_mult[5] = "001010";
			pattern_mult[6] = "010001";
			pattern_mult[7] = "000000";

			pattern_div = new string[8];
			pattern_div[0] = "000000";
			pattern_div[1] = "000000";
			pattern_div[2] = "000100";
			pattern_div[3] = "000000";
			pattern_div[4] = "011111";
			pattern_div[5] = "000000";
			pattern_div[6] = "000100";
			pattern_div[7] = "000000";

			pattern_ast = new string[8];
			pattern_ast[0] = "000000";
			pattern_ast[1] = "000000";
			pattern_ast[2] = "010101";
			pattern_ast[3] = "001110";
			pattern_ast[4] = "000100";
			pattern_ast[5] = "001110";
			pattern_ast[6] = "010101";
			pattern_ast[7] = "000000";

			addMouseListener(this);
		}

		int Width()
		{
			return getSize().width;
		}

		int Height()
		{
			return getSize().height - 5;
		}

		int ULHeight()
		{
			return getSize().height - 1;
		}

		public string getAppletInfo()
		{
			return "–¼‘O: DigitButton\r\n" +
			       "’˜ìÒ: furuta\r\n" +
			       "Microsoft Visual J++ Version 1.1 ‚Åì¬‚³‚ê‚Ü‚µ‚½";
		}

		public void init()
		{
		}

		public void destroy()
		{
		}

		private void paint_pat(Graphics g, string[] pat, int width, int height, int hgap)
		{
			for ( int i = 0; i < dvcount; i++ ) {
				for ( int j = 0; j < dhcount; j++ ) {
					if ( pat[i].charAt(j) == '1' ) {
						int bx = width * j / dhcount + hgap;
						int by = (height - hgap) * i / dvcount;
						int bw = width / dhcount;
						int bh = (height - hgap) / dvcount;
						Rectangle rcBox = new Rectangle(bx, by - 1, bw + 1, bh + 1);
						g.setColor(new Color(0, 200, 200));
						g.fillRect(rcBox.x, rcBox.y, rcBox.width, rcBox.height);
					}
				}
			}
		}

		public void paint(Graphics g)
		{
			int width = Width();
			int height = Height();
			g.setColor(new Color(255, 255, 255));
			g.fillRect(0, 0, width, height);
			int hgap = 2;
			if ( frm ) {
				g.setColor(new Color(0, 0, 0));
				g.drawRect(0, hgap, width - 1, height - 1);
			}
			if ( ul ) {
				g.setColor(new Color(0, 0, 0));
				g.drawLine(0, ULHeight(), width - 1, ULHeight());
			}
			//System.out.println(number);
			if ( number != -1 ) {
				paint_pat(g, pattern[number], width, height, hgap);
			} else if ( sym.Equals("add") ) {
				paint_pat(g, pattern_add, width, height, hgap);
			} else if ( sym.Equals("sub") ) {
				paint_pat(g, pattern_sub, width, height, hgap);
			} else if ( sym.Equals("mult") ) {
				paint_pat(g, pattern_mult, width, height, hgap);
			} else if ( sym.Equals("div") ) {
				paint_pat(g, pattern_div, width, height, hgap);
			} else if ( !sym.Equals("") ) {
				paint_pat(g, pattern_ast, width, height, hgap);
			}
		}

		public void start()
		{
		}

		public void stop()
		{
		}

		public int getNumber()
		{
			return number;
		}

		public void setNumber(int n)
		{
			if ( n >= -1 && n <= 9 ) {
				number = n;
				repaint();
			}
		}

		public bool getLock()
		{
			return lock;
		}

		public void setLock(bool l)
		{
			lock = l;
		}

		public bool getFrame()
		{
			return frm;
		}

		public void setFrame(bool f)
		{
			frm = f;
			repaint();
		}

		public string getSymbol()
		{
			return sym;
		}

		public void setSymbol(string s)
		{
			sym = s;
			repaint();
		}

		public bool getUnderLine()
		{
			return ul;
		}

		public void setUnderLine(bool u)
		{
			ul = u;
			repaint();
		}

	    public void mouseClicked(MouseEvent e)
		{
	    }

	    public void mousePressed(MouseEvent e)
		{
			if ( !lock ) {
				int x = e.getX();
				int y = e.getY();
				int posx = x * hcount / Width();
				int posy = y * vcount / Height();
				int n = posy * hcount + posx;
				if ( n <= 2 ) {
					number = n + 7;
				} else if ( n <= 5 ) {
					number = n + 1;
				} else if ( n <= 8 ) {
					number = n - 5;
				} else if ( n <= 10 ) {
					number = 0;
				} else {
					number = -1;
				}
				repaint();
			}
	    }

	    public void mouseReleased(MouseEvent e)
		{
	    }

	    public void mouseEntered(MouseEvent e)
		{
	    }

		public void mouseExited(MouseEvent e)
		{
	    }
	}
}
