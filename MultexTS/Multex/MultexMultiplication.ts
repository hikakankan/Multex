// 計算の段階：1個めの数の入力の段階、2個めの数の入力の段階、掛け算の段階、足し算の段階
enum Stage
{
	Input1 = 0,
	Input2 = 1,
	Mult = 2,
	Add = 3
}

class MultexMultiplication
{
    private panelCalc: CSPanel;
    private labelAnswer: AnswerText;
    private key_settings: KeyboardSettings;
    private view_settings: ViewSettings;
    private single_mode: boolean;

    private max_size: number;
    private cur_rows: number;
    private cur_cols: number;
    private cur_size_1: number;
    private cur_size_2: number;
    private calc_pos_1: number;
    private calc_pos_2: number;
    private add_calc_pos: number;

    private at_high_digit: boolean;
    private at_highest: boolean;
    private auto_answer_mode: boolean;
    private created: boolean;
    private created_time: Date;

    private number1: LongInteger;
    private number2: LongInteger;
    private ran: CSRandom;

    private digit: CSRandom;
    private cur_stage: Stage;

    private do_key_press_event: boolean;
    //private eKeyboardSettings: KeyboardSettings;

    public constructor(panelCalc: CSPanel, labelAnswer: AnswerText, key_settings: KeyboardSettings, view_settings: ViewSettings) {
        this.panelCalc = panelCalc;			// 計算領域のパネル
        this.labelAnswer = labelAnswer;			// 答えを表示するラベル
        this.key_settings = key_settings;		// キーボードの設定
        this.view_settings = view_settings;		// 表示の設定
        this.single_mode = false;			// 上位の桁は計算しないモード(1桁ずつ計算するモード)

        this.max_size = 10;				// 最大の桁数
        this.cur_rows = 0;				// 現在の行数
        this.cur_cols = 0;				// 現在の桁数
        this.cur_size_1 = 0;				// 1個めの数の桁数
        this.cur_size_2 = 0;				// 2個めの数の桁数
        this.calc_pos_1 = 0;				// 1個めの数の、現在計算中の桁
        this.calc_pos_2 = 0;				// 2個めの数の、現在計算中の桁
        this.add_calc_pos = 0;				// 足し算をやっているとき(後半の計算の段階)の、現在計算中の桁
        this.at_high_digit = false;			// 1桁の掛け算の上位の桁を計算中(上位の桁を計算するモードのとき)
        this.at_highest = false;			// 1桁の掛け算の最上位の桁を計算中(上位の桁は計算しないモードのとき)
        this.auto_answer_mode = false;			// 答えの自動表示モード
        this.created = false;				// 問題作成ボタンで作成されたときtrue
        this.created_time = null;			// 問題を作成した時刻

        this.number1 = new LongInteger(this.max_size);	// 1個めのかける数
        this.number2 = new LongInteger(this.max_size);	// 2個めのかける数
        this.ran = new CSRandom();			// 乱数発生用

        this.digit = null;				// 数字を表示するボタンの配列

        //this.Stage = new StageEnum();			// 計算の段階
        this.cur_stage = Stage.Input1;		// 現在どの段階にいるか

        this.do_key_press_event = false;

        this.change_stage(Stage.Input1, true);

        //this.eKeyboardSettings = new KeyboardSettings();
    }

    public getContext = function (): CanvasRenderingContext2D
	{
		return this.panelCalc.getContext();
	}

    public repaint = function (): void
	{
		this.panelCalc.repaint();
	}

    public dprint = function(text: string): void
	{
		this.labelAnswer.addText(text);
	}

	/// <summary>
	/// 1桁ずつ計算するモードを設定する
	/// </summary>
	/// <param name="s_mode">1桁ずつ計算する</param>
    public set_single_mode = function (s_mode: boolean): void
	{
		this.single_mode = s_mode;
	}

	/// <summary>
	/// 答えの自動表示モードを設定する
	/// </summary>
	/// <param name="autoans">答えの自動表示のときtrue</param>
    public set_auto_answer_mode = function (autoans: boolean): void
	{
		this.auto_answer_mode = autoans;
	}

	/// <summary>
	/// 掛け算の問題を作成する
	/// </summary>
	/// <param name="size1">1個めの数の桁数</param>
	/// <param name="size2">2個めの数の桁数</param>
    public create_mult = function (size1: number, size2: number): void
	{
		this.number1.create_new_number(size1, this.ran);
		this.number2.create_new_number(size2, this.ran);
		this.set_long_numbers(this.number1, this.number2);
		this.created = true;
		this.created_time = new Date();
		this.change_stage(Stage.Mult, true);
		this.show_selection();
	}

	/// <summary>
	/// 答えをチェックする
	/// </summary>
	/// <returns>答えが正しいときtrue</returns>
    public check_answer = function(): boolean
	{
		var ans = this.number1.multiply(this.number2);
		for ( var i = 0; i < this.cur_cols; i++ )
		{
			if ( this.digit[this.cur_rows - 1][i].getNumber() != ans[i] )
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
    public get_answer = function(): string
	{
		var ans = this.number1.multiply(this.number2);
		return ans.get_string();
	}

	/// <summary>
	/// 答えを表示する
	/// </summary>
    public show_answer = function (): void
	{
		if ( this.created )
		{
			this.created = false;
            var time = new Date(new Date().getTime() - this.created_time.getTime());
			if ( this.check_answer() )
			{
				this.labelAnswer.setCorrectAndTime(this.get_answer(), time);
			}
			else
			{
				this.labelAnswer.setIncorrectAndTime(this.get_answer(), time);
			}
		}
		else
		{
			if ( this.check_answer() )
			{
				this.labelAnswer.setCorrect(this.get_answer());
			}
			else
			{
				this.labelAnswer.setIncorrect(this.get_answer());
			}
		}
	}

	/// <summary>
	/// ステージを変更して、計算位置を設定する
	/// </summary>
	/// <param name="stage">ステージ</param>
	/// <param name="next">次の方向に変更したかどうか</param>
    public change_stage = function (stage: Stage, next): void
	{
		this.cur_stage = stage;
		switch ( this.cur_stage )
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
					this.calc_pos_1 = 0;
					this.calc_pos_2 = 0;
					this.at_high_digit = true;
					this.at_highest = false;
				}
				else
				{
					this.calc_pos_1 = this.cur_size_1 - 1;
					this.calc_pos_2 = this.cur_size_2 - 1;
					this.at_high_digit = false;
					this.at_highest = true;
				}
				break;
			case Stage.Add:
				// 足し算をやっている段階のとき(後半)
				this.add_calc_pos = 0;
				this.at_high_digit = true;
				break;
		}
	}

	/// <summary>
	/// 計算領域を作成
	/// </summary>
	/// <param name="size1">1個めの数の桁数</param>
	/// <param name="size2">2個めの数の桁数</param>
    public create_panels = function (size1: number, size2: number): void
	{
		if ( size1 == 0 )
		{
			// 0のときは変更しない
			size1 = this.cur_size_1;
		}
		if ( size2 == 0 )
		{
			// 0のときは変更しない
			size2 = this.cur_size_2;
		}

		this.panelCalc.SuspendLayout();
		// 以前の領域を削除
		for ( var i = 0; i < this.cur_rows; i++ )
		{
			for ( var j = 0; j < this.cur_cols; j++ )
			{
				this.panelCalc.Controls.Remove(this.digit[i][j]);
			}
		}

		this.cur_size_1 = size1;
		this.cur_size_2 = size2;
		this.cur_rows = 2 + size2 + 1;
		this.cur_cols = size1 + size2;
		this.digit = new Array(this.cur_rows);
		for ( var i = 0; i < this.cur_rows; i++ )
		{
			this.digit[i] = new Array(this.cur_cols);
			for ( var j = 0; j < this.cur_cols; j++ )
			{
				if ( i == 0 )
				{
					this.digit[i][j] = new DigitButton(this, Stage.Input1, 0, j, this.view_settings);
				}
				else if ( i == 1 )
				{
					this.digit[i][j] = new DigitButton(this, Stage.Input2, 0, j, this.view_settings);
				}
				else if ( i < this.cur_rows - 1 )
				{
					this.digit[i][j] = new DigitButton(this, Stage.Mult, i - 2, j, this.view_settings);
				}
				else
				{
					this.digit[i][j] = new DigitButton(this, Stage.Add, 0, j, this.view_settings);
				}
				// 1個だけタブストップを設定する
				if ( i == 0 && j == 0 )
				{
					this.digit[i][j].TabStop = true;
				}
				else
				{
					this.digit[i][j].TabStop = false;
				}
			}
		}

		// 最初の行は1個めの数
		for ( var j = 0; j < size1; j++ )
		{
			this.digit[0][j].setFrame(true);
		}
		for ( var j = size1; j < this.cur_cols; j++ )
		{
			this.digit[0][j].setLock(true);
		}
		// 2行めは2個めの数
		for ( var j = 0; j < size2; j++ )
		{
			this.digit[1][j].setFrame(true);
		}
		for ( var j = size1; j < this.cur_cols; j++ )
		{
			this.digit[1][j].setLock(true);
		}
		// 2行めにはアンダーライン
		for ( var j = 0; j < this.cur_cols; j++ )
		{
			this.digit[1][j].setUnderLine(true);
		}
		// 3行めからの2個めの数の桁数分の行は、計算の途中の領域
		for ( var i = 0; i < size2; i++ )
		{
			for ( var j = 0; j < this.cur_cols; j++ )
			{
				if ( j >= i && j < i + size1 + 1 )
				{
					this.digit[i + 2][j].setFrame(true);
				}
				else
				{
					this.digit[i + 2][j].setLock(true);
				}
			}
		}
		// 1+2個めの数の桁数行めにはアンダーライン
		for ( var j = 0; j < this.cur_cols; j++ )
		{
			this.digit[1 + size2][j].setUnderLine(true);
		}
		// 最後の行は計算結果
		for ( var j = 0; j < this.cur_cols; j++ )
		{
			this.digit[this.cur_rows - 1][j].setFrame(true);
		}
		// 掛け算の記号を書く
		this.digit[1][this.cur_cols - 1].setSymbol("mult");

		for ( var i = 0; i < this.cur_rows; i++ )
		{
			for ( var j = 0; j < this.cur_cols; j++ )
			{
				this.panelCalc.Controls.Add(this.digit[i][j]);
			}
		}
		this.panelCalc.ResumeLayout(true);
		if ( size1 > 0 )
		{
			this.digit[0][0].Focus();
		}
		this.show_calc(); // JavaScriptで追加
	}

	/// <summary>
	/// 数をセットする
	/// </summary>
	/// <param name="n1">1個めの数</param>
	/// <param name="n2">2個めの数</param>
    public set_long_numbers = function (n1: LongInteger, n2: LongInteger): void
	{
		this.create_panels(n1.get_length(), n2.get_length());
		this.replace_one_number(0, this.cur_size_1, n1);
		this.replace_one_number(1, this.cur_size_2, n2);
	}

	/// <summary>
	/// 選択状態を表示する
	/// </summary>
    public show_selection = function (): void
	{
		switch ( this.cur_stage )
		{
			case Stage.Input1:
				// 1個めの数を入力する段階
				for ( var i = 0; i < this.cur_rows; i++ )
				{
					for ( var j = 0; j < this.cur_cols; j++ )
					{
						if ( i == 0 && j == 0 )
						{
							this.digit[i][j].setDestinationMark(true);
						}
						else
						{
							this.digit[i][j].clearMark();
						}
					}
				}
				break;
			case Stage.Input2:
				// 2個めの数を入力する段階
				for ( var i = 0; i < this.cur_rows; i++ )
				{
					for ( var j = 0; j < this.cur_cols; j++ )
					{
						if ( i == 1 && j == 0 )
						{
							this.digit[i][j].setDestinationMark(true);
						}
						else
						{
							this.digit[i][j].clearMark();
						}
					}
				}
				break;
			case Stage.Mult:
				// 掛け算をやっている段階のとき(前半)
				// 1個めの数の現在計算中の桁を選択状態にする
				for ( var j = 0; j < this.cur_size_1; j++ )
				{
					if ( j == this.calc_pos_1 )
					{
						this.digit[0][j].setSourceMark();
					}
					else
					{
						this.digit[0][j].clearMark();
					}
				}
				// 2個めの数の現在計算中の桁を選択状態にする
				for ( var j = 0; j < this.cur_size_2; j++ )
				{
					if ( j == this.calc_pos_2 )
					{
						this.digit[1][j].setSourceMark();
					}
					else
					{
						this.digit[1][j].clearMark();
					}
				}
				// 中間領域を選択状態にする
				for ( var i = 0; i < this.cur_size_2; i++ )
				{
					for ( var j = 0; j < this.cur_size_1 + 1; j++ )
					{
						if ( this.single_mode && i == this.calc_pos_2 && j == this.calc_pos_1 && !this.at_highest)
						{
							this.digit[2 + i][i + j].setDestinationMark(true);
						}
						else if ( this.single_mode && i == this.calc_pos_2 && j == this.calc_pos_1 + 1 && this.at_highest)
						{
							this.digit[2 + i][i + j].setDestinationMark(true);
						}
						else if ( !this.single_mode && i == this.calc_pos_2 && j == this.calc_pos_1 )
						{
							this.digit[2 + i][i + j].setDestinationMark(!this.at_high_digit);
						}
						else if ( !this.single_mode && i == this.calc_pos_2 && j == this.calc_pos_1 + 1 )
						{
							this.digit[2 + i][i + j].setDestinationMark(this.at_high_digit);
						}
						else
						{
							this.digit[2 + i][i + j].clearMark();
						}
					}
				}
				// 結果の領域の選択状態をクリア
				for ( var j = 0; j < this.cur_cols; j++ )
				{
					this.digit[this.cur_rows - 1][j].clearMark();
				}
				break;
			case Stage.Add:
				// 足し算をやっている段階のとき(後半)
				// 1個めの数の選択状態をクリア
				for ( var j = 0; j < this.cur_size_1; j++ )
				{
					this.digit[0][j].clearMark();
				}
				// 2個めの数の選択状態をクリア
				for ( var j = 0; j < this.cur_size_2; j++ )
				{
					this.digit[1][j].clearMark();
				}
				// 中間領域を選択状態にする
				for ( var i = 0; i < this.cur_size_2; i++ )
				{
					for ( var j = 0; j < this.cur_size_1 + 1; j++ )
					{
						if ( i + j == this.add_calc_pos )
						{
							this.digit[2 + i][i + j].setSourceMark();
						}
						else
						{
							this.digit[2 + i][i + j].clearMark();
						}
					}
				}
				// 結果の領域を選択状態にする
				for ( var j = 0; j < this.cur_cols; j++ )
				{
					if ( this.single_mode && j == this.add_calc_pos )
					{
						this.digit[this.cur_rows - 1][j].setDestinationMark(true);
					}
					else if ( !this.single_mode && j == this.add_calc_pos )
					{
						this.digit[this.cur_rows - 1][j].setDestinationMark(!this.at_high_digit);
					}
					else if ( !this.single_mode && j == this.add_calc_pos + 1 )
					{
						this.digit[this.cur_rows - 1][j].setDestinationMark(this.at_high_digit);
					}
					else
					{
						this.digit[this.cur_rows - 1][j].clearMark();
					}
				}
				break;
		}
		this.repaint(); // JavaScriptで追加
	}

	/// <summary>
	/// 計算領域のサイズを変更する
	/// </summary>
    public show_calc = function (): void
	{
		for ( var i = 0; i < this.cur_rows; i++ )
		{
			for ( var j = 0; j < this.cur_cols; j++ )
			{
				var Width = this.panelCalc.getWidth() / this.cur_cols;
				var Height = this.panelCalc.getHeight() / this.cur_rows;
				var Left = this.panelCalc.getWidth() * (this.cur_cols - 1 - j) / this.cur_cols;
				var Top = this.panelCalc.getHeight() * i / this.cur_rows;
				this.digit[i][j].setRect(Left, Top, Width, Height);
			}
		}
		this.show_selection();
	}

	/// <summary>
	/// 実際に1行の数を書き換える
	/// </summary>
	/// <param name="row">書き換える行</param>
	/// <param name="size">書き換える行の桁数</param>
	/// <param name="number">書き込む数</param>
    public replace_one_number = function (row: number, size: number, number: LongInteger): void
	{
		for ( var i = 0; i < number.get_length() && i < size; i++ )
		{
			this.digit[row][i].setNumber(number.get(i));
		}
		for ( var i: number = number.get_length(); i < size; i++ )
		{
			this.digit[row][i].setNumber(-1);
		}
	}

	/// <summary>
	/// 数を書き込む
	/// </summary>
	/// <param name="n">0～9の数</param>
    public set_number = function (n: number): void
	{
		switch ( this.cur_stage )
		{
			case Stage.Input1:
				// 1個めの数を入力する段階
				this.number1.shift_left_and_add(n);
				if ( this.number1.get_length() > this.cur_size_1 && this.number1.get_length() < this.max_size )
				{
					// 入力領域のサイズを変更して、数をセットする
					this.set_long_numbers(this.number1, this.number2);
				}
				else
				{
					this.replace_one_number(0, this.cur_size_1, this.number1);
				}
				this.show_selection();
				break;
			case Stage.Input2:
				// 2個めの数を入力する段階
				this.number2.shift_left_and_add(n);
				if ( this.number2.get_length() > this.cur_size_2 && this.number2.get_length() < this.max_size )
				{
					// 入力領域のサイズを変更して、数をセットする
					this.set_long_numbers(this.number1, this.number2);
				}
				else
				{
					this.replace_one_number(1, this.cur_size_2, this.number2);
				}
				this.show_selection();
				break;
			case Stage.Mult:
				// 掛け算をやっている段階のとき(前半)
				// 中間領域に書き込む
				if ( this.single_mode )
				{
					if ( this.at_highest )
					{
						this.digit[2 + this.calc_pos_2][this.calc_pos_1 + this.calc_pos_2 + 1].setNumber(n);
					}
					else
					{
						this.digit[2 + this.calc_pos_2][this.calc_pos_1 + this.calc_pos_2].setNumber(n);
					}
				}
				else
				{
					if ( this.at_high_digit )
					{
						this.digit[2 + this.calc_pos_2][this.calc_pos_1 + this.calc_pos_2 + 1].setNumber(n);
					}
					else
					{
						this.digit[2 + this.calc_pos_2][this.calc_pos_1 + this.calc_pos_2].setNumber(n);
					}
				}
				this.move_higher();
				break;
			case Stage.Add:
				// 足し算をやっている段階のとき(後半)
				// 結果の領域に書き込む
				if ( this.single_mode )
				{
					this.digit[this.cur_rows - 1][this.add_calc_pos].setNumber(n);
				}
				else
				{
					if ( this.at_high_digit )
					{
						this.digit[this.cur_rows - 1][this.add_calc_pos + 1].setNumber(n);
					}
					else
					{
						this.digit[this.cur_rows - 1][this.add_calc_pos].setNumber(n);
					}
				}
				this.move_higher();
				break;
		}
	}

	/// <summary>
	/// 数を書き込む(繰り上がりがあるとき)繰り上がりについては何もしないことにする
	/// </summary>
	/// <param name="n">0～9の数</param>
    public set_number_with_carry = function (n): void
	{
		// 繰り上がりについては何もしない
		this.set_number(n);
	}

	/// <summary>
	/// 上の桁へ移動
	/// </summary>
    public move_higher = function (): void
	{
		switch ( this.cur_stage )
		{
			case Stage.Input1:
				// 1個めの数を入力する段階
				this.change_stage(Stage.Input2, true);
				break;
			case Stage.Input2:
				// 2個めの数を入力する段階
				this.set_long_numbers(this.number1, this.number2);
				this.change_stage(Stage.Mult, true);
				break;
			case Stage.Mult:
				// 掛け算をやっている段階のとき(前半)
				if ( !this.single_mode && this.at_high_digit )
				{
					// 掛け算の上位の桁を計算中のときは、下位の桁へ
					this.at_high_digit = false;
				}
				else if ( this.calc_pos_1 < this.cur_size_1 - 1 )
				{
					// 1個めの数が上位の桁へ進めるときは、上位の桁へ
					this.calc_pos_1++;
					this.at_high_digit = true;
					this.at_highest = false;
				}
				else if ( this.single_mode && !this.at_highest )
				{
					// 1桁ずつ計算するモードのときは、最上位の桁の1桁上の桁を計算する
					this.at_highest = true;
				}
				else if ( this.calc_pos_2 < this.cur_size_2 - 1 )
				{
					// 2個めの数が上位の桁へ進めるときは、上位の桁へ
					this.calc_pos_2++;
					this.calc_pos_1 = 0;
					this.at_high_digit = !this.single_mode;
					this.at_highest = false;
				}
				else
				{
					// 足し算へ
					this.change_stage(Stage.Add, true);
				}
				break;
			case Stage.Add:
				// 足し算をやっている段階のとき(後半)
				if ( !this.single_mode && this.at_high_digit )
				{
					// 上位の桁を計算中のときは、下位の桁へ
					this.at_high_digit = false;
				}
				else if ( !this.single_mode && this.add_calc_pos < this.cur_cols - 2 )
				{
					// 上位の桁へ進めるときは、上位の桁へ
					// 2桁ずつ計算するモードのときは、いちばん上の桁へは行かない
					this.add_calc_pos++;
					this.at_high_digit = !this.single_mode;
				}
				else if ( this.single_mode && this.add_calc_pos < this.cur_cols - 1 )
				{
					// 上位の桁へ進めるときは、上位の桁へ
					// 1桁ずつ計算するモードのときは、いちばん上の桁へは行く
					this.add_calc_pos++;
					this.at_high_digit = !this.single_mode;
				}
				else
				{
					// 終了：自動表示モードのときは答えと時間を表示する
					if ( this.auto_answer_mode )
					{
						this.show_answer();
					}
				}
				break;
		}
		this.show_selection();
	}

	/// <summary>
	/// 下の桁へ移動
	/// </summary>
    public move_lower = function (): void
	{
		switch ( this.cur_stage )
		{
			case Stage.Input1:
				// 1個めの数を入力する段階
				this.number1.shift_right();
				this.replace_one_number(0, this.cur_size_1, this.number1);
				break;
			case Stage.Input2:
				// 2個めの数を入力する段階
				this.number2.shift_right();
				this.replace_one_number(1, this.cur_size_2, this.number2);
				if ( this.number2.is_zero() )
				{
					this.change_stage(Stage.Input1, false);
				}
				break;
			case Stage.Mult:
				// 掛け算をやっている段階のとき(前半)
				if ( !this.single_mode && !this.at_high_digit )
				{
					// 掛け算の下位の桁を計算中のときは、上位の桁へ
					this.at_high_digit = !this.single_mode;
				}
				else if ( this.calc_pos_1 > 0 )
				{
					// 1個めの数が下位の桁へ進めるときは、下位の桁へ
					if ( this.single_mode )
					{
						// 1桁ずつ計算するモードのときは、いちばん上の桁がある
						if ( !this.at_highest )
						{
							this.calc_pos_1--;
						}
						this.at_highest = false;
					}
					else
					{
						this.calc_pos_1--;
						this.at_high_digit = false;
					}
				}
				else if ( this.calc_pos_2 > 0 )
				{
					// 2個めの数が下位の桁へ進めるときは、下位の桁へ
					this.calc_pos_2--;
					this.calc_pos_1 = this.cur_size_1 - 1;
					this.at_high_digit = false;
					this.at_highest = true;
				}
				else
				{
					this.change_stage(Stage.Input2, false);
				}
				break;
			case Stage.Add:
				// 足し算をやっている段階のとき(後半)
				if ( !this.single_mode && !this.at_high_digit )
				{
					// 下位の桁を計算中のときは、上位の桁へ
					this.at_high_digit = !this.single_mode;
				}
				else if ( this.add_calc_pos > 0 )
				{
					// 下位の桁へ進めるときは、下位の桁へ
					this.add_calc_pos--;
					this.at_high_digit = false;
				}
				else
				{
					// 掛け算へ
					this.change_stage(Stage.Mult, false);
				}
				break;
		}
		this.show_selection();
	}

	/// <summary>
	/// キーボードの処理
	/// </summary>
	/// <param name="key">キーのコード</param>
	/// <param name="shift">シフトキーが押されているかどうか</param>
    public DoKeyDown = function (key: string, shift: boolean): void
	{
		this.do_key_press_event = true;
		switch ( this.key_settings.get_action(key) )
		{
			case KeyAction.None:
				break;
			case KeyAction.PutNumber:
				this.set_number(this.key_settings.get_number(key));
				this.do_key_press_event = false;
				break;
			case KeyAction.MoveHigher:
				this.move_higher();
				this.do_key_press_event = false;
				break;
			case KeyAction.MoveLower:
				this.move_lower();
				this.do_key_press_event = false;
				break;
		}
	}

	/// <summary>
	/// キーボードの処理
	/// </summary>
	/// <param name="key_char">キーの文字コード</param>
    public DoKeyPress = function (key_char: string): void
	{
		if ( !this.do_key_press_event )
		{
			return;
		}
		this.do_key_press_event = false;
		switch ( this.key_settings.get_action(key_char) )
		{
			case KeyAction.None:
				break;
			case KeyAction.PutNumber:
				this.set_number(this.key_settings.get_number(key_char));
				break;
			case KeyAction.MoveHigher:
				this.move_higher();
				break;
			case KeyAction.MoveLower:
				this.move_lower();
				break;
		}
	}

	/// <summary>
	/// 外部のボタンなどでキーボードが押された時の処理
	/// </summary>
	/// <param name="key">キーのコード</param>
	/// <param name="shift">シフトキーが押されているかどうか</param>
    public ExtKeyDown = function (key: string, shift: boolean): void
	{
		if ( this.key_settings.is_calc_key(key) )
		{
			this.DoKeyDown(key, shift);
			this.digit[0][0].Focus();
		}
	}

	/// <summary>
	/// 外部のボタンなどでキーボードが押された時の処理
	/// </summary>
	/// <param name="key_char">キーの文字コード</param>
    public ExtKeyPress = function (key_char: string): void
	{
		if (this. key_settings.is_calc_key(key_char) )
		{
			this.DoKeyPress(key_char);
			this.digit[0][0].Focus();
		}
	}

	/// <summary>
	/// 数字に変更されたときは、途中の数字がないところも、数字にして、数を更新する
	/// </summary>
	/// <param name="row">掛ける数のインデックス</param>
	/// <param name="col">桁</param>
	/// <param name="number">掛ける数</param>
    public input_number = function (row: number, col: number, number: LongInteger): void
	{
		// 数字に変更されたときは、途中の数字がないところも、数字にする
		if ( this.digit[row, col].getNumber() != -1 )
		{
			for ( var i = 0; i < col; i++ )
			{
				if ( this.digit[row][i].getNumber() == -1 )
				{
					this.digit[row][i].setNumber(0);
				}
			}
		}
		// 数を更新する
		number.clear();
		for ( var i = this.cur_size_1 - 1; i >= 0; i-- )
		{
			if ( this.digit[row][i].getNumber() == -1 )
			{
				number.shift_left_and_add(0);
			}
			else
			{
				number.shift_left_and_add(this.digit[row][i].getNumber());
			}
		}
	}

	/// <summary>
	/// 入力の段階からこの段階に来たときは、計算領域を作成し直す
	/// </summary>
    public start_calc = function (): void
	{
		if ( this.cur_stage == Stage.Input1 || this.cur_stage == Stage.Input2 )
		{
			this.set_long_numbers(this.number1, this.number2);
		}
	}

	/// <summary>
	/// マウスボタンが押されたときの処理
	/// </summary>
	/// <param name="stage">ステージ</param>
	/// <param name="row">行</param>
	/// <param name="col">桁</param>
    public DoMouseDown = function (stage: Stage, row: number, col: number): void
	{
		switch ( stage )
		{
			case Stage.Input1:
				// 1個めの数を入力する段階
				this.change_stage(stage, true);
				// 数字に変更されたときは、途中の数字がないところも、数字にして、数を更新する
				this.input_number(0, col, this.number1);
				break;
			case Stage.Input2:
				// 2個めの数を入力する段階
				this.change_stage(stage, true);
				// 数字に変更されたときは、途中の数字がないところも、数字にして、数を更新する
				this.input_number(1, col, this.number2);
				break;
			case Stage.Mult:
				// 掛け算をやっている段階のとき(前半)
				// 入力の段階からこの段階に来たときは、計算領域を作成し直す
				this.start_calc();
				this.change_stage(stage, true);
				// クリックされた位置を入力位置に変更する
				this.calc_pos_1 = col;
				this.calc_pos_2 = row;
				this.at_high_digit = false;
				break;
			case Stage.Add:
				// 足し算をやっている段階のとき(後半)
				// 入力の段階からこの段階に来たときは、計算領域を作成し直す
				this.start_calc();
				this.change_stage(stage, true);
				// クリックされた位置を入力位置に変更する
				this.add_calc_pos = col;
				this.at_high_digit = false;
				break;
		}
		this.show_selection();
	}

	/// <summary>
	/// フォーカスが来たときの処理
	/// </summary>
    public DoEnter = function (): void
	{
		this.labelAnswer.clear();
	}
}
