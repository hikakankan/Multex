using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Multex
{
	/// <summary>
	/// DigitButton の概要の説明です。
	/// </summary>
	public class DigitButton : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DigitButton(MultexMultiplication m, MultexMultiplication.Stage stage, int row, int col, ViewSettings view)
		{
			// この呼び出しは、Windows.Forms フォーム デザイナで必要です。
			InitializeComponent();

			// TODO: InitializeComponent 呼び出しの後に初期化処理を追加します。
			init(m, stage, row, col, view);
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region コンポーネント デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// DigitButton
			// 
			this.Name = "DigitButton";
			this.Size = new System.Drawing.Size(160, 160);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitButton_KeyPress);
			this.Resize += new System.EventHandler(this.DigitButton_Resize);
			this.Enter += new System.EventHandler(this.DigitButton_Enter);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.DigitButton_Paint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DigitButton_KeyDown);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DigitButton_MouseDown);

		}
		#endregion

		private const int hcount = 3;
		private const int vcount = 4;
		private const int dhcount = 6;
		private const int dvcount = 8;
		private int number;
		private bool lock_f;
		private bool frm;
		private string sym;
		private bool ul;
		private bool source;
		private bool destination;
		private bool updating;
		private string[][] pattern;
		private string[] pattern_add;
		private string[] pattern_sub;
		private string[] pattern_mult;
		private string[] pattern_div;
		private string[] pattern_ast;
		/// <summary>
		/// 掛け算の問題
		/// </summary>
		private MultexMultiplication mult;
		/// <summary>
		/// ステージ(クリックされた位置の判定用)
		/// </summary>
		private MultexMultiplication.Stage the_stage;
		/// <summary>
		/// 行(クリックされた位置の判定用)
		/// </summary>
		private int the_row;
		/// <summary>
		/// 桁(クリックされた位置の判定用)
		/// </summary>
		private int the_col;

		private ViewSettings settings;

		public void init(MultexMultiplication m, MultexMultiplication.Stage stage, int row, int col, ViewSettings view)
		{
			mult = m;
			the_stage = stage;
			the_row = row;
			the_col = col;
			settings = view;

			number = -1;
			lock_f = false;
			frm = false;
			source = false;
			destination = false;
			updating = false;
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
		}

		int XWidth()
		{
			return Size.Width;
		}

		int XHeight()
		{
			return Size.Height - 5;
		}

		int ULHeight()
		{
			return Size.Height - 1;
		}

		private void paint_pat(MVGraphics g, string[] pat, Rectangle rect)
		{
			Color c_pattern = settings.CalcAreaTextColor;
			for ( int i = 0; i < dvcount; i++ ) 
			{
				for ( int j = 0; j < dhcount; j++ ) 
				{
					if ( pat[i][j] == '1' ) 
					{
						//int bx = (width - hgap) * j / dhcount + hgap / 2;
						//int by = (height - hgap * 2) * i / dvcount + hgap;
						//int bw = (width - hgap) / dhcount;
						//int bh = (height - hgap * 2) / dvcount;
						int bx = rect.Width * j / dhcount;
						int by = rect.Height * i / dvcount;
						int bw = rect.Width  / dhcount;
						int bh = rect.Height / dvcount;
						g.setColor(c_pattern);
						g.fillRect(bx, by + rect.Y, bw + 1, bh + 1);
					}
				}
			}
		}

		private Rectangle get_frame_rect()
		{
			// 上下左右の隙間を決める
			int hgap = 2;
			hgap = (int)Math.Max(2.0, (double)XHeight() * 0.04);
			int width = (int)(XWidth() * 0.98);
			int height = XHeight() - hgap;
			return new Rectangle(0, hgap, width, height);
		}

		public void paint(MVGraphics g)
		{
			Color c_upd_back = settings.CalcAreaUpdatingBackColor;
			Color c_dst_back = settings.CalcAreaDestinationBackColor;
			Color c_src_back = settings.CalcAreaSourceBackColor;
			Color c_frame_back = settings.CalcAreaFrameBackColor;
			Color c_frame = settings.CalcAreaFrameColor;
			Color c_underline = settings.CalcAreaUnderlineColor;
			Rectangle frame_rect = get_frame_rect();
			if ( frm ) 
			{
				// 枠の内部を塗りつぶす
				if ( updating )
				{
					g.setColor(c_upd_back);
				}
				else if ( destination )
				{
					g.setColor(c_dst_back);
				}
				else if ( source )
				{
					g.setColor(c_src_back);
				}
				else
				{
					g.setColor(c_frame_back);
				}
				g.fillRect(frame_rect);
				// 枠を描く
				g.setColor(c_frame);
				g.drawRect(frame_rect);
			}
			if ( ul ) 
			{
				// アンダーラインを描く
				g.setColor(c_underline);
				//g.drawLine(0, ULHeight(), width - 1, ULHeight());
				//g.drawLine(0, ULHeight(), XWidth() - 1, ULHeight());
				g.drawLine(0, ULHeight(), XWidth() + 1, ULHeight());
			}
			//System.out.println(number);
			if ( number != -1 ) 
			{
				paint_pat(g, pattern[number], frame_rect);
			} 
			else if ( sym.Equals("add") ) 
			{
				paint_pat(g, pattern_add, frame_rect);
			} 
			else if ( sym.Equals("sub") ) 
			{
				paint_pat(g, pattern_sub, frame_rect);
			} 
			else if ( sym.Equals("mult") ) 
			{
				paint_pat(g, pattern_mult, frame_rect);
			} 
			else if ( sym.Equals("div") ) 
			{
				paint_pat(g, pattern_div, frame_rect);
			} 
			else if ( !sym.Equals("") ) 
			{
				paint_pat(g, pattern_ast, frame_rect);
			}
		}

		public int getNumber()
		{
			return number;
		}

		public void setNumber(int n)
		{
			if ( n >= -1 && n <= 9 ) 
			{
				number = n;
				repaint();
			}
		}

		public bool getLock()
		{
			return lock_f;
		}

		public void setLock(bool l)
		{
			lock_f = l;
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

		private void repaint()
		{
			Refresh();
		}

		private void DigitButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ( !lock_f ) 
			{
				Rectangle frame_rect = get_frame_rect();
				int x = e.X - frame_rect.X;
				int y = e.Y - frame_rect.Y;
				int posx = x * hcount / frame_rect.Width;
				int posy = y * vcount / frame_rect.Height;
				int n = posy * hcount + posx;
				if ( n <= 2 ) 
				{
					number = n + 7;
				} 
				else if ( n <= 5 ) 
				{
					number = n + 1;
				} 
				else if ( n <= 8 ) 
				{
					number = n - 5;
				} 
				else if ( n <= 10 ) 
				{
					number = 0;
				} 
				else 
				{
					number = -1;
				}
				mult.DoMouseDown(the_stage, the_row, the_col);
				repaint();
			}
		}

		private void DigitButton_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			paint(new MVGraphics(e.Graphics, Font));
		}

		private void DigitButton_Resize(object sender, System.EventArgs e)
		{
			repaint();
		}

		/// <summary>
		/// 計算の元の位置の表示状態を変更する
		/// </summary>
		public void setSourceMark()
		{
			source = true;
			destination = false;
			updating = false;
			repaint();
		}

		/// <summary>
		/// 計算の結果を書き込む位置の表示状態を変更する
		/// </summary>
		/// <param name="updating">実際に書き込む位置のとき</param>
		public void setDestinationMark(bool bupdating)
		{
			source = false;
			destination = true;
			updating = bupdating;
			repaint();
		}

		/// <summary>
		/// 繰り上がり先の位置の表示状態を変更する
		/// </summary>
		public void setCarry()
		{
		}

		/// <summary>
		/// 表示を通常の状態にする
		/// </summary>
		public void clearMark()
		{
			source = false;
			destination = false;
			updating = false;
			repaint();
		}

		private void DigitButton_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			mult.DoKeyPress(e.KeyChar);
		}

		private void DigitButton_Enter(object sender, System.EventArgs e)
		{
			mult.DoEnter();
		}

		private void DigitButton_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			mult.DoKeyDown(e.KeyCode, e.Shift);
		}
	}
}
