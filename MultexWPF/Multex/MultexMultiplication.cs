using System;

namespace Multex
{
	/// <summary>
	/// MultexMultiplication �̊T�v�̐����ł��B
	/// </summary>
	public class MultexMultiplication
	{
		/// <summary>
		/// �L�[�{�[�h�̐ݒ�
		/// </summary>
		private KeyboardSettings key_settings;
		/// <summary>
		/// �\���̐ݒ�
		/// </summary>
		private ViewSettings view_settings;

		/// <summary>
		/// �^�u�C���f�b�N�X�̍ŏ��̒l
		/// </summary>
		//private const int tabindex_start = 9;

		/// <summary>
		/// �ő�̌���
		/// </summary>
		private const int max_size = 10;
		/// <summary>
		/// ���݂̍s��
		/// </summary>
		private int cur_rows = 0;
		/// <summary>
		/// ���݂̌���
		/// </summary>
		private int cur_cols = 0;
		/// <summary>
		/// 1�߂̐��̌���
		/// </summary>
		private int cur_size_1;
		/// <summary>
		/// 2�߂̐��̌���
		/// </summary>
		private int cur_size_2;

		/// <summary>
		/// ������\������{�^���̔z��
		/// </summary>
		private DigitButton [,] digit;

		/// <summary>
		/// 1�߂̐��́A���݌v�Z���̌�
		/// </summary>
		private int calc_pos_1;
		/// <summary>
		/// 2�߂̐��́A���݌v�Z���̌�
		/// </summary>
		private int calc_pos_2;
		/// <summary>
		/// �����Z������Ă���Ƃ�(�㔼�̌v�Z�̒i�K)�́A���݌v�Z���̌�
		/// </summary>
		private int add_calc_pos;
		/// <summary>
		/// �|���Z���v�Z���Ă���i�K(�O���̌v�Z�̒i�K)
		/// </summary>
		//private bool at_mult_stage;
		/// <summary>
		/// �v�Z�̒i�K�F1�߂̐��̓��͂̒i�K�A2�߂̐��̓��͂̒i�K�A�|���Z�̒i�K�A�����Z�̒i�K
		/// </summary>
		public enum Stage { Input1, Input2, Mult, Add }
		/// <summary>
		/// ���݂ǂ̒i�K�ɂ��邩
		/// </summary>
		private Stage cur_stage;

		/// <summary>
		/// 1���̊|���Z�̏�ʂ̌����v�Z��(��ʂ̌����v�Z���郂�[�h�̂Ƃ�)
		/// </summary>
		private bool at_high_digit;
		/// <summary>
		/// 1���̊|���Z�̍ŏ�ʂ̌����v�Z��(��ʂ̌��͌v�Z���Ȃ����[�h�̂Ƃ�)
		/// </summary>
		private bool at_highest;
		/// <summary>
		/// ��ʂ̌��͌v�Z���Ȃ����[�h
		/// </summary>
		private bool single_mode;

		/// <summary>
		/// 1�߂̂����鐔
		/// </summary>
		private LongInteger number1;
		/// <summary>
		/// 2�߂̂����鐔
		/// </summary>
		private LongInteger number2;

		/// <summary>
		/// ���������p
		/// </summary>
		private Random ran;

		//private System.Drawing.Color back_color;

		/// <summary>
		/// �v�Z�̈�̃p�l��
		/// </summary>
		private System.Windows.Forms.Panel panelCalc;
		/// <summary>
		/// ������\�����郉�x��
		/// </summary>
		private System.Windows.Forms.Label labelAnswer;

		public MultexMultiplication(System.Windows.Forms.Panel panel, System.Windows.Forms.Label label, KeyboardSettings key, ViewSettings view)
		{
			panelCalc = panel;
			labelAnswer = label;
			//back_color = System.Drawing.Color.FromArgb(255, 245, 245); // �p�l���̔w�i�̐F
			ran = new Random();
			// �L�[�{�[�h�̐ݒ�
			key_settings = key;
			// �\���̐ݒ�
			view_settings = view;
			number1 = new LongInteger(max_size);
			number2 = new LongInteger(max_size);
			single_mode = false;
			change_stage(Stage.Input1, true);
		}

		/// <summary>
		/// 1�����v�Z���郂�[�h��ݒ肷��
		/// </summary>
		/// <param name="s_mode">1�����v�Z����</param>
		public void set_single_mode(bool s_mode)
		{
			single_mode = s_mode;
		}

		/// <summary>
		/// �����̎����\�����[�h��ݒ肷��
		/// </summary>
		/// <param name="autoans">�����̎����\���̂Ƃ�true</param>
		public void set_auto_answer_mode(bool autoans)
		{
			auto_answer_mode = autoans;
		}

		/// <summary>
		/// ���쐬�{�^���ō쐬���ꂽ�Ƃ�true
		/// </summary>
		private bool created = false;

		/// <summary>
		/// �����쐬��������
		/// </summary>
		private DateTime created_time;

		/// <summary>
		/// �����̎����\�����[�h
		/// </summary>
		private bool auto_answer_mode = false;

		/// <summary>
		/// �|���Z�̖����쐬����
		/// </summary>
		/// <param name="size1">1�߂̐��̌���</param>
		/// <param name="size2">2�߂̐��̌���</param>
		public void create_mult(int size1, int size2)
		{
			number1.create_new_number(size1, ran);
			number2.create_new_number(size2, ran);
			set_long_numbers(number1, number2);
			created = true;
			created_time = DateTime.Now;
			change_stage(Stage.Mult, true);
			show_selection();
		}

		/// <summary>
		/// �������`�F�b�N����
		/// </summary>
		/// <returns>�������������Ƃ�true</returns>
		private bool check_answer()
		{
			LongInteger ans = number1.multiply(number2);
			for ( int i = 0; i < cur_cols; i++ )
			{
				if ( digit[cur_rows - 1, i].getNumber() != ans[i] )
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// �������擾����
		/// </summary>
		/// <returns>�����̕�����</returns>
		private string get_answer()
		{
			LongInteger ans = number1.multiply(number2);
			return ans.get_string();
		}

		/// <summary>
		/// ������\������
		/// </summary>
		public void show_answer()
		{
			if ( created )
			{
				created = false;
				TimeSpan time = DateTime.Now - created_time;
				if ( check_answer() )
				{
					labelAnswer.Text = String.Format(view_settings.FormatCorrectAndTime, get_answer(), time);
				}
				else
				{
					labelAnswer.Text = String.Format(view_settings.FormatIncorrectAndTime, get_answer(), time);
				}
			}
			else
			{
				if ( check_answer() )
				{
					labelAnswer.Text = String.Format(view_settings.FormatCorrect, get_answer());
				}
				else
				{
					labelAnswer.Text = String.Format(view_settings.FormatIncorrect, get_answer());
				}
			}
			//string namehead = String.Format("{0:yyyyMMddhhmmss}", DateTime.Now);
		}

		/// <summary>
		/// �X�e�[�W��ύX���āA�v�Z�ʒu��ݒ肷��
		/// </summary>
		/// <param name="stage">�X�e�[�W</param>
		/// <param name="next">���̕����ɕύX�������ǂ���</param>
		private void change_stage(Stage stage, bool next)
		{
			cur_stage = stage;
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1�߂̐�����͂���i�K
					break;
				case Stage.Input2:
					// 2�߂̐�����͂���i�K
					break;
				case Stage.Mult:
					// �|���Z������Ă���i�K�̂Ƃ�(�O��)
					if ( next )
					{
						calc_pos_1 = 0;
						calc_pos_2 = 0;
						at_high_digit = true;
						at_highest = false;
					}
					else
					{
						calc_pos_1 = cur_size_1 - 1;
						calc_pos_2 = cur_size_2 - 1;
						at_high_digit = false;
						at_highest = true;
					}
					break;
				case Stage.Add:
					// �����Z������Ă���i�K�̂Ƃ�(�㔼)
					add_calc_pos = 0;
					at_high_digit = true;
					break;
			}
		}

		/// <summary>
		/// �v�Z�̈���쐬
		/// </summary>
		/// <param name="size1">1�߂̐��̌���</param>
		/// <param name="size2">2�߂̐��̌���</param>
		public void create_panels(int size1, int size2)
		{
			if ( size1 == 0 )
			{
				// 0�̂Ƃ��͕ύX���Ȃ�
				size1 = cur_size_1;
			}
			if ( size2 == 0 )
			{
				// 0�̂Ƃ��͕ύX���Ȃ�
				size2 = cur_size_2;
			}

			panelCalc.SuspendLayout();
			// �ȑO�̗̈���폜
			for ( int i = 0; i < cur_rows; i++ )
			{
				for ( int j = 0; j < cur_cols; j++ )
				{
					panelCalc.Controls.Remove(digit[i, j]);
				}
			}

			cur_size_1 = size1;
			cur_size_2 = size2;
			cur_rows = 2 + size2 + 1;
			cur_cols = size1 + size2;
			digit = new DigitButton[cur_rows, cur_cols];
			//int t = tabindex_start;
			for ( int i = 0; i < cur_rows; i++ )
			{
				for ( int j = 0; j < cur_cols; j++ )
				{
					if ( i == 0 )
					{
						digit[i, j] = new DigitButton(this, Stage.Input1, 0, j, view_settings);
					}
					else if ( i == 1 )
					{
						digit[i, j] = new DigitButton(this, Stage.Input2, 0, j, view_settings);
					}
					else if ( i < cur_rows - 1 )
					{
						digit[i, j] = new DigitButton(this, Stage.Mult, i - 2, j, view_settings);
					}
					else
					{
						digit[i, j] = new DigitButton(this, Stage.Add, 0, j, view_settings);
					}
					// 1�����^�u�X�g�b�v��ݒ肷��
					if ( i == 0 && j == 0 )
					{
						digit[i, j].TabStop = true;
					}
					else
					{
						digit[i, j].TabStop = false;
					}
					//digit[i, j].TabIndex = t++;
				}
			}

			// �ŏ��̍s��1�߂̐�
			for ( int j = 0; j < size1; j++ )
			{
				digit[0, j].setFrame(true);
			}
			for ( int j = size1; j < cur_cols; j++ )
			{
				digit[0, j].setLock(true);
			}
			// 2�s�߂�2�߂̐�
			for ( int j = 0; j < size2; j++ )
			{
				digit[1, j].setFrame(true);
			}
			for ( int j = size1; j < cur_cols; j++ )
			{
				digit[1, j].setLock(true);
			}
			// 2�s�߂ɂ̓A���_�[���C��
			for ( int j = 0; j < cur_cols; j++ )
			{
				digit[1, j].setUnderLine(true);
			}
			// 3�s�߂����2�߂̐��̌������̍s�́A�v�Z�̓r���̗̈�
			for ( int i = 0; i < size2; i++ )
			{
				for ( int j = 0; j < cur_cols; j++ )
				{
					if ( j >= i && j < i + size1 + 1 )
					{
						digit[i + 2, j].setFrame(true);
					}
					else
					{
						digit[i + 2, j].setLock(true);
					}
				}
			}
			// 1+2�߂̐��̌����s�߂ɂ̓A���_�[���C��
			for ( int j = 0; j < cur_cols; j++ )
			{
				digit[1 + size2, j].setUnderLine(true);
			}
			// �Ō�̍s�͌v�Z����
			for ( int j = 0; j < cur_cols; j++ )
			{
				digit[cur_rows - 1, j].setFrame(true);
			}
			// �|���Z�̋L��������
			digit[1, cur_cols - 1].setSymbol("mult");
			//setnumbers("12", "34");

			for ( int i = 0; i < cur_rows; i++ )
			{
				for ( int j = 0; j < cur_cols; j++ )
				{
					panelCalc.Controls.Add(digit[i, j]);
				}
			}
			//panelCalc.BackColor = back_color;
			panelCalc.ResumeLayout(true);
			if ( size1 > 0 )
			{
				digit[0, 0].Focus();
			}
		}

		/// <summary>
		/// �����Z�b�g����
		/// </summary>
		/// <param name="n1">1�߂̐�</param>
		/// <param name="n2">2�߂̐�</param>
		private void set_long_numbers(LongInteger n1, LongInteger n2)
		{
			create_panels(n1.get_length(), n2.get_length());
			replace_one_number(0, cur_size_1, n1);
			replace_one_number(1, cur_size_2, n2);
		}

		/// <summary>
		/// �I����Ԃ�\������
		/// </summary>
		private void show_selection()
		{
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1�߂̐�����͂���i�K
					for ( int i = 0; i < cur_rows; i++ )
					{
						for ( int j = 0; j < cur_cols; j++ )
						{
							if ( i == 0 && j == 0 )
							{
								digit[i, j].setDestinationMark(true);
							}
							else
							{
								digit[i, j].clearMark();
							}
						}
					}
					break;
				case Stage.Input2:
					// 2�߂̐�����͂���i�K
					for ( int i = 0; i < cur_rows; i++ )
					{
						for ( int j = 0; j < cur_cols; j++ )
						{
							if ( i == 1 && j == 0 )
							{
								digit[i, j].setDestinationMark(true);
							}
							else
							{
								digit[i, j].clearMark();
							}
						}
					}
					break;
				case Stage.Mult:
					// �|���Z������Ă���i�K�̂Ƃ�(�O��)
					// 1�߂̐��̌��݌v�Z���̌���I����Ԃɂ���
					for ( int j = 0; j < cur_size_1; j++ )
					{
						if ( j == calc_pos_1 )
						{
							digit[0, j].setSourceMark();
						}
						else
						{
							digit[0, j].clearMark();
						}
					}
					// 2�߂̐��̌��݌v�Z���̌���I����Ԃɂ���
					for ( int j = 0; j < cur_size_2; j++ )
					{
						if ( j == calc_pos_2 )
						{
							digit[1, j].setSourceMark();
						}
						else
						{
							digit[1, j].clearMark();
						}
					}
					// ���ԗ̈��I����Ԃɂ���
					for ( int i = 0; i < cur_size_2; i++ )
					{
						for ( int j = 0; j < cur_size_1 + 1; j++ )
						{
							if ( single_mode && i == calc_pos_2 && j == calc_pos_1 && !at_highest)
							{
								digit[2 + i, i + j].setDestinationMark(true);
							}
							else if ( single_mode && i == calc_pos_2 && j == calc_pos_1 + 1 && at_highest)
							{
								digit[2 + i, i + j].setDestinationMark(true);
							}
							else if ( !single_mode && i == calc_pos_2 && j == calc_pos_1 )
							{
								digit[2 + i, i + j].setDestinationMark(!at_high_digit);
							}
							else if ( !single_mode && i == calc_pos_2 && j == calc_pos_1 + 1 )
							{
								digit[2 + i, i + j].setDestinationMark(at_high_digit);
							}
							else
							{
								digit[2 + i, i + j].clearMark();
							}
						}
					}
					// ���ʂ̗̈�̑I����Ԃ��N���A
					for ( int j = 0; j < cur_cols; j++ )
					{
						digit[cur_rows - 1, j].clearMark();
					}
					break;
				case Stage.Add:
					// �����Z������Ă���i�K�̂Ƃ�(�㔼)
					// 1�߂̐��̑I����Ԃ��N���A
					for ( int j = 0; j < cur_size_1; j++ )
					{
						digit[0, j].clearMark();
					}
					// 2�߂̐��̑I����Ԃ��N���A
					for ( int j = 0; j < cur_size_2; j++ )
					{
						digit[1, j].clearMark();
					}
					// ���ԗ̈��I����Ԃɂ���
					for ( int i = 0; i < cur_size_2; i++ )
					{
						for ( int j = 0; j < cur_size_1 + 1; j++ )
						{
							if ( i + j == add_calc_pos )
							{
								digit[2 + i, i + j].setSourceMark();
							}
							else
							{
								digit[2 + i, i + j].clearMark();
							}
						}
					}
					// ���ʂ̗̈��I����Ԃɂ���
					for ( int j = 0; j < cur_cols; j++ )
					{
						if ( single_mode && j == add_calc_pos )
						{
							digit[cur_rows - 1, j].setDestinationMark(true);
						}
						else if ( !single_mode && j == add_calc_pos )
						{
							digit[cur_rows - 1, j].setDestinationMark(!at_high_digit);
						}
						else if ( !single_mode && j == add_calc_pos + 1 )
						{
							digit[cur_rows - 1, j].setDestinationMark(at_high_digit);
						}
						else
						{
							digit[cur_rows - 1, j].clearMark();
						}
					}
					break;
			}
		}

		/// <summary>
		/// �v�Z�̈�̃T�C�Y��ύX����
		/// </summary>
		public void show_calc()
		{
			for ( int i = 0; i < cur_rows; i++ )
			{
				for ( int j = 0; j < cur_cols; j++ )
				{
					digit[i, j].Width = panelCalc.Width / cur_cols;
					digit[i, j].Height = panelCalc.Height / cur_rows;
					digit[i, j].Left = panelCalc.Width * (cur_cols - 1 - j) / cur_cols;
					digit[i, j].Top = panelCalc.Height * i / cur_rows;
				}
			}
			show_selection();
		}

		/// <summary>
		/// ���ۂ�1�s�̐�������������
		/// </summary>
		/// <param name="row">����������s</param>
		/// <param name="size">����������s�̌���</param>
		/// <param name="number">�������ސ�</param>
		private void replace_one_number(int row, int size, LongInteger number)
		{
			for ( int i = 0; i < number.get_length() && i < size; i++ )
			{
				digit[row, i].setNumber(number[i]);
			}
			for ( int i = number.get_length(); i < size; i++ )
			{
				digit[row, i].setNumber(-1);
			}
		}

		/// <summary>
		/// ������������
		/// </summary>
		/// <param name="n">0�`9�̐�</param>
		private void set_number(int n)
		{
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1�߂̐�����͂���i�K
					number1.shift_left_and_add(n);
					if ( number1.get_length() > cur_size_1 && number1.get_length() < max_size )
					{
						// ���͗̈�̃T�C�Y��ύX���āA�����Z�b�g����
						set_long_numbers(number1, number2);
					}
					else
					{
						replace_one_number(0, cur_size_1, number1);
					}
					show_selection();
					break;
				case Stage.Input2:
					// 2�߂̐�����͂���i�K
					number2.shift_left_and_add(n);
					if ( number2.get_length() > cur_size_2 && number2.get_length() < max_size )
					{
						// ���͗̈�̃T�C�Y��ύX���āA�����Z�b�g����
						set_long_numbers(number1, number2);
					}
					else
					{
						replace_one_number(1, cur_size_2, number2);
					}
					show_selection();
					break;
				case Stage.Mult:
					// �|���Z������Ă���i�K�̂Ƃ�(�O��)
					// ���ԗ̈�ɏ�������
					if ( single_mode )
					{
						if ( at_highest )
						{
							digit[2 + calc_pos_2, calc_pos_1 + calc_pos_2 + 1].setNumber(n);
						}
						else
						{
							digit[2 + calc_pos_2, calc_pos_1 + calc_pos_2].setNumber(n);
						}
					}
					else
					{
						if ( at_high_digit )
						{
							digit[2 + calc_pos_2, calc_pos_1 + calc_pos_2 + 1].setNumber(n);
						}
						else
						{
							digit[2 + calc_pos_2, calc_pos_1 + calc_pos_2].setNumber(n);
						}
					}
					move_higher();
					break;
				case Stage.Add:
					// �����Z������Ă���i�K�̂Ƃ�(�㔼)
					// ���ʂ̗̈�ɏ�������
					if ( single_mode )
					{
						digit[cur_rows - 1, add_calc_pos].setNumber(n);
					}
					else
					{
						if ( at_high_digit )
						{
							digit[cur_rows - 1, add_calc_pos + 1].setNumber(n);
						}
						else
						{
							digit[cur_rows - 1, add_calc_pos].setNumber(n);
						}
					}
					move_higher();
					break;
			}
		}

		/// <summary>
		/// ������������(�J��オ�肪����Ƃ�)�J��オ��ɂ��Ă͉������Ȃ����Ƃɂ���
		/// </summary>
		/// <param name="n">0�`9�̐�</param>
		private void set_number_with_carry(int n)
		{
			// �J��オ��ɂ��Ă͉������Ȃ�
			set_number(n);
		}

		/// <summary>
		/// ��̌��ֈړ�
		/// </summary>
		private void move_higher()
		{
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1�߂̐�����͂���i�K
					change_stage(Stage.Input2, true);
					break;
				case Stage.Input2:
					// 2�߂̐�����͂���i�K
					set_long_numbers(number1, number2);
					change_stage(Stage.Mult, true);
					break;
				case Stage.Mult:
					// �|���Z������Ă���i�K�̂Ƃ�(�O��)
					if ( !single_mode && at_high_digit )
					{
						// �|���Z�̏�ʂ̌����v�Z���̂Ƃ��́A���ʂ̌���
						at_high_digit = false;
					}
					else if ( calc_pos_1 < cur_size_1 - 1 )
					{
						// 1�߂̐�����ʂ̌��֐i�߂�Ƃ��́A��ʂ̌���
						calc_pos_1++;
						at_high_digit = true;
						at_highest = false;
					}
					else if ( single_mode && !at_highest )
					{
						// 1�����v�Z���郂�[�h�̂Ƃ��́A�ŏ�ʂ̌���1����̌����v�Z����
						at_highest = true;
					}
					else if ( calc_pos_2 < cur_size_2 - 1 )
					{
						// 2�߂̐�����ʂ̌��֐i�߂�Ƃ��́A��ʂ̌���
						calc_pos_2++;
						calc_pos_1 = 0;
						at_high_digit = !single_mode;
						at_highest = false;
					}
					else
					{
						// �����Z��
						change_stage(Stage.Add, true);
					}
					break;
				case Stage.Add:
					// �����Z������Ă���i�K�̂Ƃ�(�㔼)
					if ( !single_mode && at_high_digit )
					{
						// ��ʂ̌����v�Z���̂Ƃ��́A���ʂ̌���
						at_high_digit = false;
					}
					else if ( !single_mode && add_calc_pos < cur_cols - 2 )
					{
						// ��ʂ̌��֐i�߂�Ƃ��́A��ʂ̌���
						// 2�����v�Z���郂�[�h�̂Ƃ��́A�����΂��̌��ւ͍s���Ȃ�
						add_calc_pos++;
						at_high_digit = !single_mode;
					}
					else if ( single_mode && add_calc_pos < cur_cols - 1 )
					{
						// ��ʂ̌��֐i�߂�Ƃ��́A��ʂ̌���
						// 1�����v�Z���郂�[�h�̂Ƃ��́A�����΂��̌��ւ͍s��
						add_calc_pos++;
						at_high_digit = !single_mode;
					}
					else
					{
						// �I���F�����\�����[�h�̂Ƃ��͓����Ǝ��Ԃ�\������
						if ( auto_answer_mode )
						{
							show_answer();
						}
					}
					break;
			}
			show_selection();
		}

		/// <summary>
		/// ���̌��ֈړ�
		/// </summary>
		private void move_lower()
		{
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1�߂̐�����͂���i�K
					number1.shift_right();
					replace_one_number(0, cur_size_1, number1);
					break;
				case Stage.Input2:
					// 2�߂̐�����͂���i�K
					number2.shift_right();
					replace_one_number(1, cur_size_2, number2);
					if ( number2.is_zero() )
					{
						change_stage(Stage.Input1, false);
					}
					break;
				case Stage.Mult:
					// �|���Z������Ă���i�K�̂Ƃ�(�O��)
					if ( !single_mode && !at_high_digit )
					{
						// �|���Z�̉��ʂ̌����v�Z���̂Ƃ��́A��ʂ̌���
						at_high_digit = !single_mode;
					}
					else if ( calc_pos_1 > 0 )
					{
						// 1�߂̐������ʂ̌��֐i�߂�Ƃ��́A���ʂ̌���
						if ( single_mode )
						{
							// 1�����v�Z���郂�[�h�̂Ƃ��́A�����΂��̌�������
							if ( !at_highest )
							{
								calc_pos_1--;
							}
							at_highest = false;
						}
						else
						{
							calc_pos_1--;
							at_high_digit = false;
						}
					}
					else if ( calc_pos_2 > 0 )
					{
						// 2�߂̐������ʂ̌��֐i�߂�Ƃ��́A���ʂ̌���
						calc_pos_2--;
						calc_pos_1 = cur_size_1 - 1;
						at_high_digit = false;
						at_highest = true;
					}
					else
					{
						change_stage(Stage.Input2, false);
					}
					break;
				case Stage.Add:
					// �����Z������Ă���i�K�̂Ƃ�(�㔼)
					if ( !single_mode && !at_high_digit )
					{
						// ���ʂ̌����v�Z���̂Ƃ��́A��ʂ̌���
						at_high_digit = !single_mode;
					}
					else if ( add_calc_pos > 0 )
					{
						// ���ʂ̌��֐i�߂�Ƃ��́A���ʂ̌���
						add_calc_pos--;
						at_high_digit = false;
					}
					else
					{
						// �|���Z��
						change_stage(Stage.Mult, false);
					}
					break;
			}
			show_selection();
		}

		private bool do_key_press_event = false;
		
		/// <summary>
		/// �L�[�{�[�h�̏���
		/// </summary>
		/// <param name="key">�L�[�̃R�[�h</param>
		/// <param name="shift">�V�t�g�L�[��������Ă��邩�ǂ���</param>
		public void DoKeyDown(System.Windows.Forms.Keys key, bool shift)
		{
			System.Diagnostics.Debug.WriteLine("DoKeyDown: " + key.ToString());
			do_key_press_event = true;
			switch ( key_settings.get_action(key) )
			{
				case KeyboardSettings.KeyAction.None:
					break;
				case KeyboardSettings.KeyAction.PutNumber:
					set_number(key_settings.get_number(key));
					do_key_press_event = false;
					break;
					//case KeyboardSettings.KeyAction.PutNumberWithCarry:
					//	set_number_with_carry(key_settings.Number);
					//	break;
				case KeyboardSettings.KeyAction.MoveHigher:
					move_higher();
					do_key_press_event = false;
					break;
				case KeyboardSettings.KeyAction.MoveLower:
					move_lower();
					do_key_press_event = false;
					break;
			}
		}

		/// <summary>
		/// �L�[�{�[�h�̏���
		/// </summary>
		/// <param name="key_char">�L�[�̕����R�[�h</param>
		public void DoKeyPress(char key_char)
		{
			System.Diagnostics.Debug.WriteLine("DoKeyPress: " + key_char.ToString());
			if ( !do_key_press_event )
			{
				return;
			}
			do_key_press_event = false;
			switch ( key_settings.get_action(key_char) )
			{
				case KeyboardSettings.KeyAction.None:
					break;
				case KeyboardSettings.KeyAction.PutNumber:
					set_number(key_settings.get_number(key_char));
					break;
					//case KeyboardSettings.KeyAction.PutNumberWithCarry:
					//	set_number_with_carry(key_settings.Number);
					//	break;
				case KeyboardSettings.KeyAction.MoveHigher:
					move_higher();
					break;
				case KeyboardSettings.KeyAction.MoveLower:
					move_lower();
					break;
			}
		}

		/// <summary>
		/// �O���̃{�^���ȂǂŃL�[�{�[�h�������ꂽ���̏���
		/// </summary>
		/// <param name="key">�L�[�̃R�[�h</param>
		/// <param name="shift">�V�t�g�L�[��������Ă��邩�ǂ���</param>
		public void ExtKeyDown(System.Windows.Forms.Keys key, bool shift)
		{
			if ( key_settings.is_calc_key(key) )
			{
				DoKeyDown(key, shift);
				digit[0, 0].Focus();
			}
		}

		/// <summary>
		/// �O���̃{�^���ȂǂŃL�[�{�[�h�������ꂽ���̏���
		/// </summary>
		/// <param name="key_char">�L�[�̕����R�[�h</param>
		public void ExtKeyPress(char key_char)
		{
			if ( key_settings.is_calc_key(key_char) )
			{
				DoKeyPress(key_char);
				digit[0, 0].Focus();
			}
		}

		/// <summary>
		/// �����ɕύX���ꂽ�Ƃ��́A�r���̐������Ȃ��Ƃ�����A�����ɂ��āA�����X�V����
		/// </summary>
		/// <param name="row">�|���鐔�̃C���f�b�N�X</param>
		/// <param name="col">��</param>
		/// <param name="number">�|���鐔</param>
		private void input_number(int row, int col, ref LongInteger number)
		{
			// �����ɕύX���ꂽ�Ƃ��́A�r���̐������Ȃ��Ƃ�����A�����ɂ���
			if ( digit[row, col].getNumber() != -1 )
			{
				for ( int i = 0; i < col; i++ )
				{
					if ( digit[row, i].getNumber() == -1 )
					{
						digit[row, i].setNumber(0);
					}
				}
			}
			// �����X�V����
			number.clear();
			for ( int i = cur_size_1 - 1; i >= 0; i-- )
			{
				if ( digit[row, i].getNumber() == -1 )
				{
					number.shift_left_and_add(0);
				}
				else
				{
					number.shift_left_and_add(digit[row, i].getNumber());
				}
			}
		}

		/// <summary>
		/// ���͂̒i�K���炱�̒i�K�ɗ����Ƃ��́A�v�Z�̈���쐬������
		/// </summary>
		private void start_calc()
		{
			if ( cur_stage == Stage.Input1 || cur_stage == Stage.Input1 )
			{
				set_long_numbers(number1, number2);
			}
		}

		/// <summary>
		/// �}�E�X�{�^���������ꂽ�Ƃ��̏���
		/// </summary>
		/// <param name="stage">�X�e�[�W</param>
		/// <param name="row">�s</param>
		/// <param name="col">��</param>
		public void DoMouseDown(Stage stage, int row, int col)
		{
			switch ( stage )
			{
				case Stage.Input1:
					// 1�߂̐�����͂���i�K
					change_stage(stage, true);
					// �����ɕύX���ꂽ�Ƃ��́A�r���̐������Ȃ��Ƃ�����A�����ɂ��āA�����X�V����
					input_number(0, col, ref number1);
					break;
				case Stage.Input2:
					// 2�߂̐�����͂���i�K
					change_stage(stage, true);
					// �����ɕύX���ꂽ�Ƃ��́A�r���̐������Ȃ��Ƃ�����A�����ɂ��āA�����X�V����
					input_number(1, col, ref number2);
					break;
				case Stage.Mult:
					// �|���Z������Ă���i�K�̂Ƃ�(�O��)
					// ���͂̒i�K���炱�̒i�K�ɗ����Ƃ��́A�v�Z�̈���쐬������
					start_calc();
					change_stage(stage, true);
					// �N���b�N���ꂽ�ʒu����͈ʒu�ɕύX����
					calc_pos_1 = col;
					calc_pos_2 = row;
					at_high_digit = false;
					break;
				case Stage.Add:
					// �����Z������Ă���i�K�̂Ƃ�(�㔼)
					// ���͂̒i�K���炱�̒i�K�ɗ����Ƃ��́A�v�Z�̈���쐬������
					start_calc();
					change_stage(stage, true);
					// �N���b�N���ꂽ�ʒu����͈ʒu�ɕύX����
					add_calc_pos = col;
					at_high_digit = false;
					break;
			}
			show_selection();
		}

		/// <summary>
		/// �t�H�[�J�X�������Ƃ��̏���
		/// </summary>
		public void DoEnter()
		{
			labelAnswer.Text = "";
		}
	}
}
