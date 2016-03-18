using System;

namespace Multex
{
	/// <summary>
	/// MultexMultiplication の概要の説明です。
	/// </summary>
	public class MultexMultiplication
	{
		/// <summary>
		/// キーボードの設定
		/// </summary>
		private KeyboardSettings key_settings;
		/// <summary>
		/// 表示の設定
		/// </summary>
		private ViewSettings view_settings;

		/// <summary>
		/// タブインデックスの最初の値
		/// </summary>
		//private const int tabindex_start = 9;

		/// <summary>
		/// 最大の桁数
		/// </summary>
		private const int max_size = 10;
		/// <summary>
		/// 現在の行数
		/// </summary>
		private int cur_rows = 0;
		/// <summary>
		/// 現在の桁数
		/// </summary>
		private int cur_cols = 0;
		/// <summary>
		/// 1個めの数の桁数
		/// </summary>
		private int cur_size_1;
		/// <summary>
		/// 2個めの数の桁数
		/// </summary>
		private int cur_size_2;

		/// <summary>
		/// 数字を表示するボタンの配列
		/// </summary>
		private DigitButton [,] digit;

		/// <summary>
		/// 1個めの数の、現在計算中の桁
		/// </summary>
		private int calc_pos_1;
		/// <summary>
		/// 2個めの数の、現在計算中の桁
		/// </summary>
		private int calc_pos_2;
		/// <summary>
		/// 足し算をやっているとき(後半の計算の段階)の、現在計算中の桁
		/// </summary>
		private int add_calc_pos;
		/// <summary>
		/// 掛け算を計算している段階(前半の計算の段階)
		/// </summary>
		//private bool at_mult_stage;
		/// <summary>
		/// 計算の段階：1個めの数の入力の段階、2個めの数の入力の段階、掛け算の段階、足し算の段階
		/// </summary>
		public enum Stage { Input1, Input2, Mult, Add }
		/// <summary>
		/// 現在どの段階にいるか
		/// </summary>
		private Stage cur_stage;

		/// <summary>
		/// 1桁の掛け算の上位の桁を計算中(上位の桁を計算するモードのとき)
		/// </summary>
		private bool at_high_digit;
		/// <summary>
		/// 1桁の掛け算の最上位の桁を計算中(上位の桁は計算しないモードのとき)
		/// </summary>
		private bool at_highest;
		/// <summary>
		/// 上位の桁は計算しないモード
		/// </summary>
		private bool single_mode;

		/// <summary>
		/// 1個めのかける数
		/// </summary>
		private LongInteger number1;
		/// <summary>
		/// 2個めのかける数
		/// </summary>
		private LongInteger number2;

		/// <summary>
		/// 乱数発生用
		/// </summary>
		private Random ran;

		//private System.Drawing.Color back_color;

		/// <summary>
		/// 計算領域のパネル
		/// </summary>
		private System.Windows.Forms.Panel panelCalc;
		/// <summary>
		/// 答えを表示するラベル
		/// </summary>
		private System.Windows.Forms.Label labelAnswer;

		public MultexMultiplication(System.Windows.Forms.Panel panel, System.Windows.Forms.Label label, KeyboardSettings key, ViewSettings view)
		{
			panelCalc = panel;
			labelAnswer = label;
			//back_color = System.Drawing.Color.FromArgb(255, 245, 245); // パネルの背景の色
			ran = new Random();
			// キーボードの設定
			key_settings = key;
			// 表示の設定
			view_settings = view;
			number1 = new LongInteger(max_size);
			number2 = new LongInteger(max_size);
			single_mode = false;
			change_stage(Stage.Input1, true);
		}

		/// <summary>
		/// 1桁ずつ計算するモードを設定する
		/// </summary>
		/// <param name="s_mode">1桁ずつ計算する</param>
		public void set_single_mode(bool s_mode)
		{
			single_mode = s_mode;
		}

		/// <summary>
		/// 答えの自動表示モードを設定する
		/// </summary>
		/// <param name="autoans">答えの自動表示のときtrue</param>
		public void set_auto_answer_mode(bool autoans)
		{
			auto_answer_mode = autoans;
		}

		/// <summary>
		/// 問題作成ボタンで作成されたときtrue
		/// </summary>
		private bool created = false;

		/// <summary>
		/// 問題を作成した時刻
		/// </summary>
		private DateTime created_time;

		/// <summary>
		/// 答えの自動表示モード
		/// </summary>
		private bool auto_answer_mode = false;

		/// <summary>
		/// 掛け算の問題を作成する
		/// </summary>
		/// <param name="size1">1個めの数の桁数</param>
		/// <param name="size2">2個めの数の桁数</param>
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
		/// 答えをチェックする
		/// </summary>
		/// <returns>答えが正しいときtrue</returns>
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
		/// 答えを取得する
		/// </summary>
		/// <returns>答えの文字列</returns>
		private string get_answer()
		{
			LongInteger ans = number1.multiply(number2);
			return ans.get_string();
		}

		/// <summary>
		/// 答えを表示する
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
		/// ステージを変更して、計算位置を設定する
		/// </summary>
		/// <param name="stage">ステージ</param>
		/// <param name="next">次の方向に変更したかどうか</param>
		private void change_stage(Stage stage, bool next)
		{
			cur_stage = stage;
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1個めの数を入力する段階
					break;
				case Stage.Input2:
					// 2個めの数を入力する段階
					break;
				case Stage.Mult:
					// 掛け算をやっている段階のとき(前半)
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
					// 足し算をやっている段階のとき(後半)
					add_calc_pos = 0;
					at_high_digit = true;
					break;
			}
		}

		/// <summary>
		/// 計算領域を作成
		/// </summary>
		/// <param name="size1">1個めの数の桁数</param>
		/// <param name="size2">2個めの数の桁数</param>
		public void create_panels(int size1, int size2)
		{
			if ( size1 == 0 )
			{
				// 0のときは変更しない
				size1 = cur_size_1;
			}
			if ( size2 == 0 )
			{
				// 0のときは変更しない
				size2 = cur_size_2;
			}

			panelCalc.SuspendLayout();
			// 以前の領域を削除
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
					// 1個だけタブストップを設定する
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

			// 最初の行は1個めの数
			for ( int j = 0; j < size1; j++ )
			{
				digit[0, j].setFrame(true);
			}
			for ( int j = size1; j < cur_cols; j++ )
			{
				digit[0, j].setLock(true);
			}
			// 2行めは2個めの数
			for ( int j = 0; j < size2; j++ )
			{
				digit[1, j].setFrame(true);
			}
			for ( int j = size1; j < cur_cols; j++ )
			{
				digit[1, j].setLock(true);
			}
			// 2行めにはアンダーライン
			for ( int j = 0; j < cur_cols; j++ )
			{
				digit[1, j].setUnderLine(true);
			}
			// 3行めからの2個めの数の桁数分の行は、計算の途中の領域
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
			// 1+2個めの数の桁数行めにはアンダーライン
			for ( int j = 0; j < cur_cols; j++ )
			{
				digit[1 + size2, j].setUnderLine(true);
			}
			// 最後の行は計算結果
			for ( int j = 0; j < cur_cols; j++ )
			{
				digit[cur_rows - 1, j].setFrame(true);
			}
			// 掛け算の記号を書く
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
		/// 数をセットする
		/// </summary>
		/// <param name="n1">1個めの数</param>
		/// <param name="n2">2個めの数</param>
		private void set_long_numbers(LongInteger n1, LongInteger n2)
		{
			create_panels(n1.get_length(), n2.get_length());
			replace_one_number(0, cur_size_1, n1);
			replace_one_number(1, cur_size_2, n2);
		}

		/// <summary>
		/// 選択状態を表示する
		/// </summary>
		private void show_selection()
		{
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1個めの数を入力する段階
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
					// 2個めの数を入力する段階
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
					// 掛け算をやっている段階のとき(前半)
					// 1個めの数の現在計算中の桁を選択状態にする
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
					// 2個めの数の現在計算中の桁を選択状態にする
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
					// 中間領域を選択状態にする
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
					// 結果の領域の選択状態をクリア
					for ( int j = 0; j < cur_cols; j++ )
					{
						digit[cur_rows - 1, j].clearMark();
					}
					break;
				case Stage.Add:
					// 足し算をやっている段階のとき(後半)
					// 1個めの数の選択状態をクリア
					for ( int j = 0; j < cur_size_1; j++ )
					{
						digit[0, j].clearMark();
					}
					// 2個めの数の選択状態をクリア
					for ( int j = 0; j < cur_size_2; j++ )
					{
						digit[1, j].clearMark();
					}
					// 中間領域を選択状態にする
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
					// 結果の領域を選択状態にする
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
		/// 計算領域のサイズを変更する
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
		/// 実際に1行の数を書き換える
		/// </summary>
		/// <param name="row">書き換える行</param>
		/// <param name="size">書き換える行の桁数</param>
		/// <param name="number">書き込む数</param>
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
		/// 数を書き込む
		/// </summary>
		/// <param name="n">0～9の数</param>
		private void set_number(int n)
		{
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1個めの数を入力する段階
					number1.shift_left_and_add(n);
					if ( number1.get_length() > cur_size_1 && number1.get_length() < max_size )
					{
						// 入力領域のサイズを変更して、数をセットする
						set_long_numbers(number1, number2);
					}
					else
					{
						replace_one_number(0, cur_size_1, number1);
					}
					show_selection();
					break;
				case Stage.Input2:
					// 2個めの数を入力する段階
					number2.shift_left_and_add(n);
					if ( number2.get_length() > cur_size_2 && number2.get_length() < max_size )
					{
						// 入力領域のサイズを変更して、数をセットする
						set_long_numbers(number1, number2);
					}
					else
					{
						replace_one_number(1, cur_size_2, number2);
					}
					show_selection();
					break;
				case Stage.Mult:
					// 掛け算をやっている段階のとき(前半)
					// 中間領域に書き込む
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
					// 足し算をやっている段階のとき(後半)
					// 結果の領域に書き込む
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
		/// 数を書き込む(繰り上がりがあるとき)繰り上がりについては何もしないことにする
		/// </summary>
		/// <param name="n">0～9の数</param>
		private void set_number_with_carry(int n)
		{
			// 繰り上がりについては何もしない
			set_number(n);
		}

		/// <summary>
		/// 上の桁へ移動
		/// </summary>
		private void move_higher()
		{
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1個めの数を入力する段階
					change_stage(Stage.Input2, true);
					break;
				case Stage.Input2:
					// 2個めの数を入力する段階
					set_long_numbers(number1, number2);
					change_stage(Stage.Mult, true);
					break;
				case Stage.Mult:
					// 掛け算をやっている段階のとき(前半)
					if ( !single_mode && at_high_digit )
					{
						// 掛け算の上位の桁を計算中のときは、下位の桁へ
						at_high_digit = false;
					}
					else if ( calc_pos_1 < cur_size_1 - 1 )
					{
						// 1個めの数が上位の桁へ進めるときは、上位の桁へ
						calc_pos_1++;
						at_high_digit = true;
						at_highest = false;
					}
					else if ( single_mode && !at_highest )
					{
						// 1桁ずつ計算するモードのときは、最上位の桁の1桁上の桁を計算する
						at_highest = true;
					}
					else if ( calc_pos_2 < cur_size_2 - 1 )
					{
						// 2個めの数が上位の桁へ進めるときは、上位の桁へ
						calc_pos_2++;
						calc_pos_1 = 0;
						at_high_digit = !single_mode;
						at_highest = false;
					}
					else
					{
						// 足し算へ
						change_stage(Stage.Add, true);
					}
					break;
				case Stage.Add:
					// 足し算をやっている段階のとき(後半)
					if ( !single_mode && at_high_digit )
					{
						// 上位の桁を計算中のときは、下位の桁へ
						at_high_digit = false;
					}
					else if ( !single_mode && add_calc_pos < cur_cols - 2 )
					{
						// 上位の桁へ進めるときは、上位の桁へ
						// 2桁ずつ計算するモードのときは、いちばん上の桁へは行かない
						add_calc_pos++;
						at_high_digit = !single_mode;
					}
					else if ( single_mode && add_calc_pos < cur_cols - 1 )
					{
						// 上位の桁へ進めるときは、上位の桁へ
						// 1桁ずつ計算するモードのときは、いちばん上の桁へは行く
						add_calc_pos++;
						at_high_digit = !single_mode;
					}
					else
					{
						// 終了：自動表示モードのときは答えと時間を表示する
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
		/// 下の桁へ移動
		/// </summary>
		private void move_lower()
		{
			switch ( cur_stage )
			{
				case Stage.Input1:
					// 1個めの数を入力する段階
					number1.shift_right();
					replace_one_number(0, cur_size_1, number1);
					break;
				case Stage.Input2:
					// 2個めの数を入力する段階
					number2.shift_right();
					replace_one_number(1, cur_size_2, number2);
					if ( number2.is_zero() )
					{
						change_stage(Stage.Input1, false);
					}
					break;
				case Stage.Mult:
					// 掛け算をやっている段階のとき(前半)
					if ( !single_mode && !at_high_digit )
					{
						// 掛け算の下位の桁を計算中のときは、上位の桁へ
						at_high_digit = !single_mode;
					}
					else if ( calc_pos_1 > 0 )
					{
						// 1個めの数が下位の桁へ進めるときは、下位の桁へ
						if ( single_mode )
						{
							// 1桁ずつ計算するモードのときは、いちばん上の桁がある
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
						// 2個めの数が下位の桁へ進めるときは、下位の桁へ
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
					// 足し算をやっている段階のとき(後半)
					if ( !single_mode && !at_high_digit )
					{
						// 下位の桁を計算中のときは、上位の桁へ
						at_high_digit = !single_mode;
					}
					else if ( add_calc_pos > 0 )
					{
						// 下位の桁へ進めるときは、下位の桁へ
						add_calc_pos--;
						at_high_digit = false;
					}
					else
					{
						// 掛け算へ
						change_stage(Stage.Mult, false);
					}
					break;
			}
			show_selection();
		}

		private bool do_key_press_event = false;
		
		/// <summary>
		/// キーボードの処理
		/// </summary>
		/// <param name="key">キーのコード</param>
		/// <param name="shift">シフトキーが押されているかどうか</param>
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
		/// キーボードの処理
		/// </summary>
		/// <param name="key_char">キーの文字コード</param>
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
		/// 外部のボタンなどでキーボードが押された時の処理
		/// </summary>
		/// <param name="key">キーのコード</param>
		/// <param name="shift">シフトキーが押されているかどうか</param>
		public void ExtKeyDown(System.Windows.Forms.Keys key, bool shift)
		{
			if ( key_settings.is_calc_key(key) )
			{
				DoKeyDown(key, shift);
				digit[0, 0].Focus();
			}
		}

		/// <summary>
		/// 外部のボタンなどでキーボードが押された時の処理
		/// </summary>
		/// <param name="key_char">キーの文字コード</param>
		public void ExtKeyPress(char key_char)
		{
			if ( key_settings.is_calc_key(key_char) )
			{
				DoKeyPress(key_char);
				digit[0, 0].Focus();
			}
		}

		/// <summary>
		/// 数字に変更されたときは、途中の数字がないところも、数字にして、数を更新する
		/// </summary>
		/// <param name="row">掛ける数のインデックス</param>
		/// <param name="col">桁</param>
		/// <param name="number">掛ける数</param>
		private void input_number(int row, int col, ref LongInteger number)
		{
			// 数字に変更されたときは、途中の数字がないところも、数字にする
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
			// 数を更新する
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
		/// 入力の段階からこの段階に来たときは、計算領域を作成し直す
		/// </summary>
		private void start_calc()
		{
			if ( cur_stage == Stage.Input1 || cur_stage == Stage.Input1 )
			{
				set_long_numbers(number1, number2);
			}
		}

		/// <summary>
		/// マウスボタンが押されたときの処理
		/// </summary>
		/// <param name="stage">ステージ</param>
		/// <param name="row">行</param>
		/// <param name="col">桁</param>
		public void DoMouseDown(Stage stage, int row, int col)
		{
			switch ( stage )
			{
				case Stage.Input1:
					// 1個めの数を入力する段階
					change_stage(stage, true);
					// 数字に変更されたときは、途中の数字がないところも、数字にして、数を更新する
					input_number(0, col, ref number1);
					break;
				case Stage.Input2:
					// 2個めの数を入力する段階
					change_stage(stage, true);
					// 数字に変更されたときは、途中の数字がないところも、数字にして、数を更新する
					input_number(1, col, ref number2);
					break;
				case Stage.Mult:
					// 掛け算をやっている段階のとき(前半)
					// 入力の段階からこの段階に来たときは、計算領域を作成し直す
					start_calc();
					change_stage(stage, true);
					// クリックされた位置を入力位置に変更する
					calc_pos_1 = col;
					calc_pos_2 = row;
					at_high_digit = false;
					break;
				case Stage.Add:
					// 足し算をやっている段階のとき(後半)
					// 入力の段階からこの段階に来たときは、計算領域を作成し直す
					start_calc();
					change_stage(stage, true);
					// クリックされた位置を入力位置に変更する
					add_calc_pos = col;
					at_high_digit = false;
					break;
			}
			show_selection();
		}

		/// <summary>
		/// フォーカスが来たときの処理
		/// </summary>
		public void DoEnter()
		{
			labelAnswer.Text = "";
		}
	}
}
