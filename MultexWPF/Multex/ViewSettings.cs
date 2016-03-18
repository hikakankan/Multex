using System;
using System.Drawing;

namespace Multex
{
	/// <summary>
	/// ViewSettings の概要の説明です。
	/// </summary>
	public class ViewSettings
	{
		// 制御領域
		private System.Drawing.Color c_body_bg;      // 背景色
		private System.Drawing.Color c_body_text;    // テキストの色
		private System.Drawing.Color c_button;       // ボタンの色
		private System.Drawing.Color c_button_text;  // ボタンのテキストの色
		private System.Drawing.Color c_textbox;      // テキストボックスの色
		private System.Drawing.Color c_text_text;    // テキストボックスのテキストの色

		// 計算領域
		private System.Drawing.Color c_pattern;      // パターンの色
		private System.Drawing.Color c_back;         // 背景の色
		private System.Drawing.Color c_frame;        // 枠の色
		private System.Drawing.Color c_frame_back;   // 枠の背景の色
		private System.Drawing.Color c_underline;    // アンダーラインの色
		private System.Drawing.Color c_src_back;     // 計算する数字の背景の色
		private System.Drawing.Color c_dst_back;     // 計算結果を書き込む場所の背景の色(2桁)
		private System.Drawing.Color c_upd_back;     // 計算結果を書き込む場所の背景の色

		// フォーマット
		private string format_correct;               // 正解のときのフォーマット
		private string format_correct_time;          // 正解のときのフォーマット(時間あり)
		private string format_incorrect;             // 不正解のときのフォーマット
		private string format_incorrect_time;        // 不正解のときのフォーマット(時間あり)

		public ViewSettings()
		{
			c_body_bg = System.Drawing.Color.FromArgb(220, 240, 255);  // 背景色
			c_body_text = System.Drawing.Color.FromArgb(0, 0, 0);      // テキストの色
			c_button = System.Drawing.Color.FromArgb(200, 220, 255);   // ボタンの色
			c_button_text = System.Drawing.Color.FromArgb(0, 0, 0);    // ボタンのテキストの色
			c_textbox = System.Drawing.Color.FromArgb(255, 245, 245);  // テキストボックスの色
			c_text_text = System.Drawing.Color.FromArgb(0, 0, 0);      // テキストボックスのテキストの色

			c_pattern = System.Drawing.Color.FromArgb(0, 200, 200);
			c_back = System.Drawing.Color.FromArgb(255, 245, 245);
			c_frame = System.Drawing.Color.FromArgb(0, 0, 0);
			c_frame_back = System.Drawing.Color.FromArgb(255, 240, 240);
			c_underline = System.Drawing.Color.FromArgb(0, 0, 0);
			c_src_back = System.Drawing.Color.FromArgb(255, 235, 235);
			c_dst_back = System.Drawing.Color.FromArgb(255, 235, 235);
			c_upd_back = System.Drawing.Color.FromArgb(255, 230, 230);

			format_correct = "○ {0}";
			format_correct_time = "○ {0} {1}";
			format_incorrect = "× {0}";
			format_incorrect_time = "× {0} {1}";
		}

		/// <summary>
		/// 背景色
		/// </summary>
		public Color BodyBackColor
		{
			get
			{
				return c_body_bg;
			}
			set
			{
				c_body_bg = value;
			}
		}

		/// <summary>
		/// テキストの色
		/// </summary>
		public Color BodyTextColor
		{
			get
			{
				return c_body_text;
			}
			set
			{
				c_body_text = value;
			}
		}

		/// <summary>
		/// ボタンの色
		/// </summary>
		public Color ButtonBackColor
		{
			get
			{
				return c_button;
			}
			set
			{
				c_button = value;
			}
		}

		/// <summary>
		/// ボタンの文字の色
		/// </summary>
		public Color ButtonTextColor
		{
			get
			{
				return c_button_text;
			}
			set
			{
				c_button_text = value;
			}
		}

		/// <summary>
		/// テキストボックスの色
		/// </summary>
		public Color TextBackColor
		{
			get
			{
				return c_textbox;
			}
			set
			{
				c_textbox = value;
			}
		}

		/// <summary>
		/// テキストボックスのテキストの色
		/// </summary>
		public Color TextTextColor
		{
			get
			{
				return c_text_text;
			}
			set
			{
				c_text_text = value;
			}
		}

		/// <summary>
		/// 計算領域の文字の色
		/// </summary>
		public Color CalcAreaTextColor
		{
			get
			{
				return c_pattern;
			}
			set
			{
				c_pattern = value;
			}
		}

		/// <summary>
		/// 計算領域の背景色
		/// </summary>
		public Color CalcAreaBackColor
		{
			get
			{
				return c_back;
			}
			set
			{
				c_back = value;
			}
		}

		/// <summary>
		/// 計算領域の枠の色
		/// </summary>
		public Color CalcAreaFrameColor
		{
			get
			{
				return c_frame;
			}
			set
			{
				c_frame = value;
			}
		}

		/// <summary>
		/// 計算領域の枠の中の色
		/// </summary>
		public Color CalcAreaFrameBackColor
		{
			get
			{
				return c_frame_back;
			}
			set
			{
				c_frame_back = value;
			}
		}

		/// <summary>
		/// 計算領域の下線の色
		/// </summary>
		public Color CalcAreaUnderlineColor
		{
			get
			{
				return c_underline;
			}
			set
			{
				c_underline = value;
			}
		}

		/// <summary>
		/// 計算領域の計算中の元の桁の色
		/// </summary>
		public Color CalcAreaSourceBackColor
		{
			get
			{
				return c_src_back;
			}
			set
			{
				c_src_back = value;
			}
		}

		/// <summary>
		/// 計算領域の計算中の結果の桁の色
		/// </summary>
		public Color CalcAreaDestinationBackColor
		{
			get
			{
				return c_dst_back;
			}
			set
			{
				c_dst_back = value;
			}
		}

		/// <summary>
		/// 計算領域の計算中の結果の桁の中で書き込む桁の色
		/// </summary>
		public Color CalcAreaUpdatingBackColor
		{
			get
			{
				return c_upd_back;
			}
			set
			{
				c_upd_back = value;
			}
		}

		/// <summary>
		/// 正解のときのフォーマット
		/// </summary>
		public string FormatCorrect
		{
			get
			{
				return format_correct;
			}
			set
			{
				format_correct = value;
			}
		}

		/// <summary>
		/// 正解のときのフォーマット(時間も表示)
		/// </summary>
		public string FormatCorrectAndTime
		{
			get
			{
				return format_correct_time;
			}
			set
			{
				format_correct_time = value;
			}
		}

		/// <summary>
		/// 不正解のときのフォーマット
		/// </summary>
		public string FormatIncorrect
		{
			get
			{
				return format_incorrect;
			}
			set
			{
				format_incorrect = value;
			}
		}

		/// <summary>
		/// 不正解のときのフォーマット(時間も表示)
		/// </summary>
		public string FormatIncorrectAndTime
		{
			get
			{
				return format_incorrect_time;
			}
			set
			{
				format_incorrect_time = value;
			}
		}

		/// <summary>
		/// ボタンに色を設定する
		/// </summary>
		/// <param name="button">ボタン</param>
		public void SetButtonColors(System.Windows.Forms.Button button)
		{
			button.BackColor = c_button;
			button.ForeColor = c_button_text;
		}

		/// <summary>
		/// テキストボックスに色を設定する
		/// </summary>
		/// <param name="text">テキストボックス</param>
		public void SetTextBoxColors(System.Windows.Forms.TextBox text)
		{
			text.BackColor = c_textbox;
			text.ForeColor = c_text_text;
		}
	}
}
