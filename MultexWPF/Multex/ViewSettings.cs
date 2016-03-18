using System;
using System.Drawing;

namespace Multex
{
	/// <summary>
	/// ViewSettings �̊T�v�̐����ł��B
	/// </summary>
	public class ViewSettings
	{
		// ����̈�
		private System.Drawing.Color c_body_bg;      // �w�i�F
		private System.Drawing.Color c_body_text;    // �e�L�X�g�̐F
		private System.Drawing.Color c_button;       // �{�^���̐F
		private System.Drawing.Color c_button_text;  // �{�^���̃e�L�X�g�̐F
		private System.Drawing.Color c_textbox;      // �e�L�X�g�{�b�N�X�̐F
		private System.Drawing.Color c_text_text;    // �e�L�X�g�{�b�N�X�̃e�L�X�g�̐F

		// �v�Z�̈�
		private System.Drawing.Color c_pattern;      // �p�^�[���̐F
		private System.Drawing.Color c_back;         // �w�i�̐F
		private System.Drawing.Color c_frame;        // �g�̐F
		private System.Drawing.Color c_frame_back;   // �g�̔w�i�̐F
		private System.Drawing.Color c_underline;    // �A���_�[���C���̐F
		private System.Drawing.Color c_src_back;     // �v�Z���鐔���̔w�i�̐F
		private System.Drawing.Color c_dst_back;     // �v�Z���ʂ��������ޏꏊ�̔w�i�̐F(2��)
		private System.Drawing.Color c_upd_back;     // �v�Z���ʂ��������ޏꏊ�̔w�i�̐F

		// �t�H�[�}�b�g
		private string format_correct;               // �����̂Ƃ��̃t�H�[�}�b�g
		private string format_correct_time;          // �����̂Ƃ��̃t�H�[�}�b�g(���Ԃ���)
		private string format_incorrect;             // �s�����̂Ƃ��̃t�H�[�}�b�g
		private string format_incorrect_time;        // �s�����̂Ƃ��̃t�H�[�}�b�g(���Ԃ���)

		public ViewSettings()
		{
			c_body_bg = System.Drawing.Color.FromArgb(220, 240, 255);  // �w�i�F
			c_body_text = System.Drawing.Color.FromArgb(0, 0, 0);      // �e�L�X�g�̐F
			c_button = System.Drawing.Color.FromArgb(200, 220, 255);   // �{�^���̐F
			c_button_text = System.Drawing.Color.FromArgb(0, 0, 0);    // �{�^���̃e�L�X�g�̐F
			c_textbox = System.Drawing.Color.FromArgb(255, 245, 245);  // �e�L�X�g�{�b�N�X�̐F
			c_text_text = System.Drawing.Color.FromArgb(0, 0, 0);      // �e�L�X�g�{�b�N�X�̃e�L�X�g�̐F

			c_pattern = System.Drawing.Color.FromArgb(0, 200, 200);
			c_back = System.Drawing.Color.FromArgb(255, 245, 245);
			c_frame = System.Drawing.Color.FromArgb(0, 0, 0);
			c_frame_back = System.Drawing.Color.FromArgb(255, 240, 240);
			c_underline = System.Drawing.Color.FromArgb(0, 0, 0);
			c_src_back = System.Drawing.Color.FromArgb(255, 235, 235);
			c_dst_back = System.Drawing.Color.FromArgb(255, 235, 235);
			c_upd_back = System.Drawing.Color.FromArgb(255, 230, 230);

			format_correct = "�� {0}";
			format_correct_time = "�� {0} {1}";
			format_incorrect = "�~ {0}";
			format_incorrect_time = "�~ {0} {1}";
		}

		/// <summary>
		/// �w�i�F
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
		/// �e�L�X�g�̐F
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
		/// �{�^���̐F
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
		/// �{�^���̕����̐F
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
		/// �e�L�X�g�{�b�N�X�̐F
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
		/// �e�L�X�g�{�b�N�X�̃e�L�X�g�̐F
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
		/// �v�Z�̈�̕����̐F
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
		/// �v�Z�̈�̔w�i�F
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
		/// �v�Z�̈�̘g�̐F
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
		/// �v�Z�̈�̘g�̒��̐F
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
		/// �v�Z�̈�̉����̐F
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
		/// �v�Z�̈�̌v�Z���̌��̌��̐F
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
		/// �v�Z�̈�̌v�Z���̌��ʂ̌��̐F
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
		/// �v�Z�̈�̌v�Z���̌��ʂ̌��̒��ŏ������ތ��̐F
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
		/// �����̂Ƃ��̃t�H�[�}�b�g
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
		/// �����̂Ƃ��̃t�H�[�}�b�g(���Ԃ��\��)
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
		/// �s�����̂Ƃ��̃t�H�[�}�b�g
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
		/// �s�����̂Ƃ��̃t�H�[�}�b�g(���Ԃ��\��)
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
		/// �{�^���ɐF��ݒ肷��
		/// </summary>
		/// <param name="button">�{�^��</param>
		public void SetButtonColors(System.Windows.Forms.Button button)
		{
			button.BackColor = c_button;
			button.ForeColor = c_button_text;
		}

		/// <summary>
		/// �e�L�X�g�{�b�N�X�ɐF��ݒ肷��
		/// </summary>
		/// <param name="text">�e�L�X�g�{�b�N�X</param>
		public void SetTextBoxColors(System.Windows.Forms.TextBox text)
		{
			text.BackColor = c_textbox;
			text.ForeColor = c_text_text;
		}
	}
}
