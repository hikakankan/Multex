class PatternSet
{
    private dhcount: number;
    private dvcount: number;
    private pattern: Array<Array<string>>;
    private match_ratio: Array<number>;

    public constructor() {
        this.dhcount = 6;
        this.dvcount = 8;
        this.pattern = new Array<Array<string>>(10);
        this.pattern[0] = new Array<string>(this.dvcount);
        this.pattern[1] = new Array<string>(this.dvcount);
        this.pattern[2] = new Array<string>(this.dvcount);
        this.pattern[3] = new Array<string>(this.dvcount);
        this.pattern[4] = new Array<string>(this.dvcount);
        this.pattern[5] = new Array<string>(this.dvcount);
        this.pattern[6] = new Array<string>(this.dvcount);
        this.pattern[7] = new Array<string>(this.dvcount);
        this.pattern[8] = new Array<string>(this.dvcount);
        this.pattern[9] = new Array<string>(this.dvcount);

        this.pattern[0][0] = "000000";
        this.pattern[0][1] = "001100";
        this.pattern[0][2] = "010010";
        this.pattern[0][3] = "010010";
        this.pattern[0][4] = "010010";
        this.pattern[0][5] = "010010";
        this.pattern[0][6] = "010010";
        this.pattern[0][7] = "001100";

        this.pattern[1][0] = "000000";
        this.pattern[1][1] = "000100";
        this.pattern[1][2] = "001100";
        this.pattern[1][3] = "000100";
        this.pattern[1][4] = "000100";
        this.pattern[1][5] = "000100";
        this.pattern[1][6] = "000100";
        this.pattern[1][7] = "001110";

        this.pattern[2][0] = "000000";
        this.pattern[2][1] = "001100";
        this.pattern[2][2] = "010010";
        this.pattern[2][3] = "010010";
        this.pattern[2][4] = "000100";
        this.pattern[2][5] = "001000";
        this.pattern[2][6] = "010000";
        this.pattern[2][7] = "011110";

        this.pattern[3][0] = "000000";
        this.pattern[3][1] = "001100";
        this.pattern[3][2] = "010010";
        this.pattern[3][3] = "000010";
        this.pattern[3][4] = "001100";
        this.pattern[3][5] = "000010";
        this.pattern[3][6] = "010010";
        this.pattern[3][7] = "001100";

        this.pattern[4][0] = "000000";
        this.pattern[4][1] = "000100";
        this.pattern[4][2] = "001100";
        this.pattern[4][3] = "010100";
        this.pattern[4][4] = "010100";
        this.pattern[4][5] = "011110";
        this.pattern[4][6] = "000100";
        this.pattern[4][7] = "000100";

        this.pattern[5][0] = "000000";
        this.pattern[5][1] = "011110";
        this.pattern[5][2] = "010000";
        this.pattern[5][3] = "011100";
        this.pattern[5][4] = "000010";
        this.pattern[5][5] = "000010";
        this.pattern[5][6] = "010010";
        this.pattern[5][7] = "001100";

        this.pattern[6][0] = "000000";
        this.pattern[6][1] = "001100";
        this.pattern[6][2] = "010010";
        this.pattern[6][3] = "010000";
        this.pattern[6][4] = "011100";
        this.pattern[6][5] = "010010";
        this.pattern[6][6] = "010010";
        this.pattern[6][7] = "001100";

        this.pattern[7][0] = "000000";
        this.pattern[7][1] = "011110";
        this.pattern[7][2] = "000010";
        this.pattern[7][3] = "000010";
        this.pattern[7][4] = "000100";
        this.pattern[7][5] = "000100";
        this.pattern[7][6] = "001000";
        this.pattern[7][7] = "001000";

        this.pattern[8][0] = "000000";
        this.pattern[8][1] = "001100";
        this.pattern[8][2] = "010010";
        this.pattern[8][3] = "010010";
        this.pattern[8][4] = "001100";
        this.pattern[8][5] = "010010";
        this.pattern[8][6] = "010010";
        this.pattern[8][7] = "001100";

        this.pattern[9][0] = "000000";
        this.pattern[9][1] = "001100";
        this.pattern[9][2] = "010010";
        this.pattern[9][3] = "010010";
        this.pattern[9][4] = "001110";
        this.pattern[9][5] = "000010";
        this.pattern[9][6] = "010010";
        this.pattern[9][7] = "001100";

        this.match_ratio = new Array<number>(10); // 一致するための個数(48個のうちの)
        for (var n = 0; n < 10; n++) {
            this.match_ratio[n] = 40;
        }
    }

    public get_match_number = function (match_number: number, pattern: Array<Array<string>>): number
	{
		var match_count_max = 0;
		for ( var n = 0; n < 10; n++ ) {
			var match_count = 0;
			for ( var i = 0; i < this.dvcount; i++ ) {
				for ( var j = 0; j < this.dhcount; j++ ) {
					if ( pattern[i][j] == this.pattern[n][i][j] ) {
						match_count++;
					}
				}
			}
			if ( match_count >= this.match_ratio[n] ) {
				if ( match_count > match_count_max ) {
					match_count_max = match_count;
					match_number = n;
				}
			}
		}
		return match_number;
	}
}

class ScriptPatternSet
{
    private dhcount: number;
    private dvcount: number;
    private pattern: Array<Array<string>>;
    private match_ratio: Array<number>;
    private out_pattern_ratio: number;
    private no_number: number;

    public constructor() {
        // 手書き用のパターン
        this.dhcount = 6;
        this.dvcount = 8;
        this.pattern = new Array<Array<string>>(11);
        this.pattern[0] = new Array<string>(this.dvcount);
        this.pattern[1] = new Array<string>(this.dvcount);
        this.pattern[2] = new Array<string>(this.dvcount);
        this.pattern[3] = new Array<string>(this.dvcount);
        this.pattern[4] = new Array<string>(this.dvcount);
        this.pattern[5] = new Array<string>(this.dvcount);
        this.pattern[6] = new Array<string>(this.dvcount);
        this.pattern[7] = new Array<string>(this.dvcount);
        this.pattern[8] = new Array<string>(this.dvcount);
        this.pattern[9] = new Array<string>(this.dvcount);
        this.pattern[10] = new Array<string>(this.dvcount);

        this.pattern[0][0] = "011110";
        this.pattern[0][1] = "111111";
        this.pattern[0][2] = "110011";
        this.pattern[0][3] = "110011";
        this.pattern[0][4] = "110011";
        this.pattern[0][5] = "110011";
        this.pattern[0][6] = "111111";
        this.pattern[0][7] = "011110";

        this.pattern[1][0] = "001100";
        this.pattern[1][1] = "001100";
        this.pattern[1][2] = "001100";
        this.pattern[1][3] = "001100";
        this.pattern[1][4] = "001100";
        this.pattern[1][5] = "001100";
        this.pattern[1][6] = "001100";
        this.pattern[1][7] = "001100";

        this.pattern[2][0] = "111111";
        this.pattern[2][1] = "111111";
        this.pattern[2][2] = "000110";
        this.pattern[2][3] = "001100";
        this.pattern[2][4] = "011000";
        this.pattern[2][5] = "110000";
        this.pattern[2][6] = "111111";
        this.pattern[2][7] = "111111";

        this.pattern[3][0] = "111111";
        this.pattern[3][1] = "111111";
        this.pattern[3][2] = "000110";
        this.pattern[3][3] = "001111";
        this.pattern[3][4] = "001111";
        this.pattern[3][5] = "000110";
        this.pattern[3][6] = "001100";
        this.pattern[3][7] = "011000";

        this.pattern[4][0] = "000011";
        this.pattern[4][1] = "000110";
        this.pattern[4][2] = "001110";
        this.pattern[4][3] = "011110";
        this.pattern[4][4] = "111111";
        this.pattern[4][5] = "111111";
        this.pattern[4][6] = "000110";
        this.pattern[4][7] = "000110";

        this.pattern[5][0] = "111111";
        this.pattern[5][1] = "111111";
        this.pattern[5][2] = "110000";
        this.pattern[5][3] = "111110";
        this.pattern[5][4] = "111111";
        this.pattern[5][5] = "000110";
        this.pattern[5][6] = "001100";
        this.pattern[5][7] = "011000";

        this.pattern[6][0] = "000011";
        this.pattern[6][1] = "000110";
        this.pattern[6][2] = "001100";
        this.pattern[6][3] = "011100";
        this.pattern[6][4] = "110110";
        this.pattern[6][5] = "110011";
        this.pattern[6][6] = "111111";
        this.pattern[6][7] = "011110";

        this.pattern[7][0] = "111111";
        this.pattern[7][1] = "111111";
        this.pattern[7][2] = "000011";
        this.pattern[7][3] = "000011";
        this.pattern[7][4] = "000110";
        this.pattern[7][5] = "001100";
        this.pattern[7][6] = "011000";
        this.pattern[7][7] = "110000";

        this.pattern[8][0] = "011110";
        this.pattern[8][1] = "111111";
        this.pattern[8][2] = "110011";
        this.pattern[8][3] = "011110";
        this.pattern[8][4] = "011110";
        this.pattern[8][5] = "110011";
        this.pattern[8][6] = "111111";
        this.pattern[8][7] = "011110";

        this.pattern[9][0] = "011110";
        this.pattern[9][1] = "111111";
        this.pattern[9][2] = "110011";
        this.pattern[9][3] = "011111";
        this.pattern[9][4] = "001110";
        this.pattern[9][5] = "001100";
        this.pattern[9][6] = "011000";
        this.pattern[9][7] = "110000";

        this.pattern[10][0] = "111000";
        this.pattern[10][1] = "111000";
        this.pattern[10][2] = "011100";
        this.pattern[10][3] = "011100";
        this.pattern[10][4] = "001110";
        this.pattern[10][5] = "001110";
        this.pattern[10][6] = "000111";
        this.pattern[10][7] = "000111";

        this.match_ratio = new Array(10); // 一致するための比率
        for (var n = 0; n < 11; n++) {
            this.match_ratio[n] = 0.4;
        }
        this.out_pattern_ratio = 0.1; // はみ出しても良い比率
        this.no_number = -1; // 数字なしの状態
    }

    public get_match_number = function (match_number: number, pattern: Array<Array<string>>): number
	{
		var match_ratio_max = 0;
		for ( var n = 0; n < 11; n++ ) {
			var cur_pattern_count = 0;
			var all_pattern_count = 0;
			var out_pattern_count = 0;
			for ( var i = 0; i < this.dvcount; i++ ) {
				for ( var j = 0; j < this.dhcount; j++ ) {
					if ( Number(pattern[i][j]) > Number(this.pattern[n][i][j]) ) {
						// パターンがはみ出している
						out_pattern_count++;
					} else {
						cur_pattern_count += Number(pattern[i][j]);
					}
					all_pattern_count += Number(this.pattern[n][i][j]);
				}
			}
			if ( out_pattern_count / all_pattern_count <= this.out_pattern_ratio ) {
				var cur_match_ratio =  cur_pattern_count / all_pattern_count;
				if ( cur_match_ratio >= this.match_ratio[n] ) {
					if ( cur_match_ratio > match_ratio_max ) {
						match_ratio_max = cur_match_ratio;
						match_number = n;
					}
				}
			}
		}
		if ( match_number >= 10 ) {
			match_number = this.no_number; // 数字なしの状態
		}
		return match_number;
	}
}

class Pattern
{
    private dhcount: number;
    private dvcount: number;
    private cancel_number: number;
    private number: number;
    private erase_ratio: number;
    private no_number: number;
    private pattern: Array<Array<string>>;

    public constructor(vcount: number, hcount: number) {
        this.dvcount = vcount;
        this.dhcount = hcount;
        this.cancel_number = -2;
        this.number = this.cancel_number;
        this.erase_ratio = 32; // 消去するための個数(48個のうちの)
        this.no_number = -1; // 数字なしの状態

        this.pattern = new Array<Array<string>>(vcount);
        for (var i = 0; i < vcount; i++) {
            this.pattern[i] = new Array<string>(hcount);
        }

        this.clear();
    }

    public clear = function(): void
	{
		for ( var i = 0; i < this.dvcount; i++ ) {
			for ( var j = 0; j < this.dhcount; j++ ) {
				this.pattern[i][j] = "0";
			}
		}
	}

    public set_pixel = function(i: number, j: number): void
	{
		this.pattern[i][j] = "1";
		var pixel_count = 0;
		for ( var i = 0; i < this.dvcount; i++ ) {
			for ( var j = 0; j < this.dhcount; j++ ) {
				if ( this.pattern[i][j] == "1" ) {
					pixel_count++;
				}
			}
		}
		if ( pixel_count >= this.erase_ratio ) {
			this.number = this.no_number; // 数字なしの状態
			this.clear();
			return;
		}
	}
}

class DigitButton
{
    private mult: number;
    private the_stage: number;
    private the_row: number;
    private the_col: number;
    private settings: ViewSettings;
    private manual_mode: boolean;
    private rect: CSRectangle;
    private no_number: number;
    private hcount: number;
    private vcount: number;
    private dhcount: number;
    private dvcount: number;
    private number: number;
    private lock: boolean;
    private frm: boolean;
    private sym: string;
    private ul: boolean;
    private source: boolean;
    private destination: boolean;
    private updating: boolean;
    private pattern_set: PatternSet;
    private script_pattern_set: ScriptPatternSet;
    private pattern_add: Array<string>;
    private pattern_sub: Array<string>;
    private pattern_mult: Array<string>;
    private pattern_div: Array<string>;
    private pattern_ast: Array<string>;
    private manual_moving: boolean;
    public TabStop: boolean;

    public constructor(mult, stage, row, col, view_settings) {
        this.mult = mult;	// 掛け算の問題
        this.the_stage = stage;	// ステージ(クリックされた位置の判定用)
        this.the_row = row;	// 行(クリックされた位置の判定用)
        this.the_col = col;	// 桁(クリックされた位置の判定用)
        this.settings = view_settings;

        this.manual_mode = true; // 手書きモード

        this.rect = new CSRectangle(20, 20, 80, 60);

        this.no_number = -1; // 数字なしの状態
        this.hcount = 3;
        this.vcount = 4;
        this.dhcount = 6;
        this.dvcount = 8;
        this.number = this.no_number; // 数字なしの状態
        this.lock = false;
        this.frm = false;
        this.sym = "";
        this.ul = false;
        this.source = false;
        this.destination = false;
        this.updating = false;

        this.pattern_set = new PatternSet();
        this.script_pattern_set = new ScriptPatternSet();

        this.pattern_add = new Array<string>(this.dvcount);
        this.pattern_add[0] = "000000";
        this.pattern_add[1] = "000000";
        this.pattern_add[2] = "000100";
        this.pattern_add[3] = "000100";
        this.pattern_add[4] = "011111";
        this.pattern_add[5] = "000100";
        this.pattern_add[6] = "000100";
        this.pattern_add[7] = "000000";

        this.pattern_sub = new Array<string>(this.dvcount);
        this.pattern_sub[0] = "000000";
        this.pattern_sub[1] = "000000";
        this.pattern_sub[2] = "000000";
        this.pattern_sub[3] = "000000";
        this.pattern_sub[4] = "011111";
        this.pattern_sub[5] = "000000";
        this.pattern_sub[6] = "000000";
        this.pattern_sub[7] = "000000";

        this.pattern_mult = new Array<string>(this.dvcount);
        this.pattern_mult[0] = "000000";
        this.pattern_mult[1] = "000000";
        this.pattern_mult[2] = "010001";
        this.pattern_mult[3] = "001010";
        this.pattern_mult[4] = "000100";
        this.pattern_mult[5] = "001010";
        this.pattern_mult[6] = "010001";
        this.pattern_mult[7] = "000000";

        this.pattern_div = new Array<string>(this.dvcount);
        this.pattern_div[0] = "000000";
        this.pattern_div[1] = "000000";
        this.pattern_div[2] = "000100";
        this.pattern_div[3] = "000000";
        this.pattern_div[4] = "011111";
        this.pattern_div[5] = "000000";
        this.pattern_div[6] = "000100";
        this.pattern_div[7] = "000000";

        this.pattern_ast = new Array<string>(this.dvcount);
        this.pattern_ast[0] = "000000";
        this.pattern_ast[1] = "000000";
        this.pattern_ast[2] = "010101";
        this.pattern_ast[3] = "001110";
        this.pattern_ast[4] = "000100";
        this.pattern_ast[5] = "001110";
        this.pattern_ast[6] = "010101";
        this.pattern_ast[7] = "000000";

        this.manual_moving = false;

        this.TabStop = false;
    }

    public setRect = function (left, top, width, height) {
        this.rect = new CSRectangle(left, top, width, height);
    }

    public Left = function()
	{
		return this.rect.x;
	}

    public Top = function()
	{
		return this.rect.y;
	}

    public Width = function()
	{
		return this.rect.width;
	}

    public Height = function()
	{
		return this.rect.height;
	}

    public XWidth = function()
	{
		return this.rect.width;
	}

    public XHeight = function()
	{
		return this.rect.height - 5;
	}

    public paint_pat = function(g, pat, rect)
	{
		for ( var i = 0; i < this.dvcount; i++ ) {
			for ( var j = 0; j < this.dhcount; j++ ) {
				if ( pat[i][j] == '1' ) {
					var bx = rect.x + rect.width * j / this.dhcount;
					var by = rect.y + rect.height * i / this.dvcount;
					var bw = rect.width / this.dhcount;
					var bh = rect.height / this.dvcount;
					g.setColor(this.settings.CalcAreaTextColor);
					g.fillRect(bx, by, bw + 1, bh + 1);
				}
			}
		}
	}

    public get_frame_rect = function()
	{
		// 上下左右の隙間を決める
		var hgap = Math.floor(Math.max(2.0, this.XHeight() * 0.04));
		var width = Math.floor(this.XWidth() * 0.98);
		var height = this.XHeight() - hgap;
		return new CSRectangle(this.Left(), this.Top() + hgap, width, height);
	}

    public get_inner_frame_rect = function()
	{
		// 内部のパターンを描く部分の矩形
		var frame_rect = this.get_frame_rect();
		return new CSRectangle(frame_rect.x + 1, frame_rect.y + 1, frame_rect.width - 4, frame_rect.height - 4);
	}

    public paint = function(g)
	{
		var frame_rect = this.get_frame_rect();
		var inner_frame_rect = this.get_inner_frame_rect();
		if ( this.frm ) {
			// 枠の内部を塗りつぶす
			if ( this.updating ) {
				g.setColor(this.settings.CalcAreaUpdatingBackColor);
			} else if ( this.destination ) {
				g.setColor(this.settings.CalcAreaDestinationBackColor);
			} else if ( this.source ) {
				g.setColor(this.settings.CalcAreaSourceBackColor);
			} else {
				g.setColor(this.settings.CalcAreaFrameBackColor);
			}
			g.fillRect(frame_rect.x, frame_rect.y, frame_rect.width, frame_rect.height);
			g.setColor(this.settings.CalcAreaFrameColor);
			g.drawRect(frame_rect.x, frame_rect.y, frame_rect.width - 1, frame_rect.height - 1);
		}
		if ( this.ul ) {
			// アンダーラインを描く
			g.setColor(this.settings.CalcAreaUnderlineColor);
			var my = frame_rect.y + (frame_rect.height + this.Height()) / 2 - 1; // フレームの中間
			g.drawLine(this.Left(), my, this.Left() + this.Width() + 1, my);
		}
		if ( this.manual_moving ) {
			// 手書き中
			this.paint_pat(g, this.current_pattern.pattern, inner_frame_rect);
		} else {
			if ( this.number != this.no_number ) {
				this.paint_pat(g, this.pattern_set.pattern[this.number], inner_frame_rect);
			} else if ( this.sym == "add" ) {
				this.paint_pat(g, this.pattern_add, inner_frame_rect);
			} else if ( this.sym == "sub" ) {
				this.paint_pat(g, this.pattern_sub, inner_frame_rect);
			} else if ( this.sym == "mult" ) {
				this.paint_pat(g, this.pattern_mult, inner_frame_rect);
			} else if ( this.sym == "div" ) {
				this.paint_pat(g, this.pattern_div, inner_frame_rect);
			} else if ( !(this.sym == "") ) {
				this.paint_pat(g, this.pattern_ast, inner_frame_rect);
			}
		}
	}

    public repaint = function()
	{
		var g = new CSGraphics(this.mult.getContext());
		this.paint(g);
	}

    public getNumber = function()
	{
		return this.number;
	}

    public setNumber = function(n)
	{
		if ( n == this.no_number || n >= 0 && n <= 9 ) {
			this.number = n;
			this.repaint();
		}
	}

    public getLock = function()
	{
		return this.lock;
	}

    public setLock = function(l)
	{
		this.lock = l;
	}

    public getFrame = function()
	{
		return this.frm;
	}

    public setFrame = function(f)
	{
		this.frm = f;
		this.repaint();
	}

    public getSymbol = function()
	{
		return this.sym;
	}

    public setSymbol = function(s)
	{
		this.sym = s;
		this.repaint();
	}

    public getUnderLine = function()
	{
		return this.ul;
	}

    public setUnderLine = function(u)
	{
		this.ul = u;
		this.repaint();
	}

	/// <summary>
	/// 計算の元の位置の表示状態を変更する
	/// </summary>
    public setSourceMark = function()
	{
		this.source = true;
		this.destination = false;
		this.updating = false;
		this.repaint();
	}

	/// <summary>
	/// 計算の結果を書き込む位置の表示状態を変更する
	/// </summary>
	/// <param name="updating">実際に書き込む位置のとき</param>
    public setDestinationMark = function(bupdating)
	{
		this.source = false;
		this.destination = true;
		this.updating = bupdating;
		this.repaint();
	}

	/// <summary>
	/// 表示を通常の状態にする
	/// </summary>
    public clearMark = function()
	{
		this.source = false;
		this.destination = false;
		this.updating = false;
		this.repaint();
	}

    public DigitButton_KeyPress = function(sender, e)
	{
		this.mult.DoKeyPress(e.KeyChar);
	}

    public DigitButton_Enter = function(sender, e)
	{
		this.mult.DoEnter();
	}

    public DigitButton_KeyDown = function(sender, e)
	{
		this.mult.DoKeyDown(e.KeyCode, e.Shift);
	}

    public mousePressed = function(x, y)
	{
		var frame_rect = this.get_inner_frame_rect();
		if ( !frame_rect.contains(x, y) ) {
			return;
		}
		x -= this.rect.x;
		y -= this.rect.y;
		if ( !this.lock ) {
			this.manual_moving = true;
			if ( this.manual_mode ) {
				// 手書きモード
				var posx = Math.floor(x * this.dhcount / this.Width());
				var posy = Math.floor(y * this.dvcount / this.Height());
				this.current_pattern = new Pattern(this.dvcount, this.dhcount);
				this.current_pattern.set_pixel(posy, posx);
				this.repaint();
			} else {
				var posx = Math.floor(x * this.hcount / this.Width());
				var posy = Math.floor(y * this.vcount / this.Height());
				var n = posy * this.hcount + posx;
				if ( n <= 2 ) {
					this.number = n + 7;
				} else if ( n <= 5 ) {
					this.number = n + 1;
				} else if ( n <= 8 ) {
					this.number = n - 5;
				} else if ( n <= 10 ) {
					this.number = 0;
				} else {
					this.number = this.no_number;
				}
				this.repaint();
			}
		}
	}

    public mouseReleased = function(x, y)
	{
		if ( !this.lock ) {
			if ( this.manual_mode ) {
				// 手書きモード
				if ( this.manual_moving ) {
					this.manual_moving = false;
					var match_number = this.pattern_set.get_match_number(this.current_pattern.cancel_number, this.current_pattern.pattern);
					match_number = this.script_pattern_set.get_match_number(match_number, this.current_pattern.pattern);
					if ( match_number != this.current_pattern.cancel_number ) {
						this.number = match_number;
					}
					this.repaint();
				}
			}
		}
	}

    public mouseMoved = function(x, y)
	{
		var frame_rect = this.get_inner_frame_rect();
		if ( !frame_rect.contains(x, y) ) {
			return;
		}
		x -= this.rect.x;
		y -= this.rect.y;
		if ( !this.lock ) {
			if ( this.manual_mode ) {
				// 手書きモード
				if ( this.manual_moving ) {
					var posx = Math.floor(x * this.dhcount / this.Width());
					var posy = Math.floor(y * this.dvcount / this.Height());
					this.current_pattern.set_pixel(posy, posx);
					this.repaint();
				}
			}
		}
	}

    public touchStart = function(x, y)
	{
		this.mousePressed(x, y);
	}

    public touchEnd = function(ids)
	{
		this.mouseReleased(0, 0);
	}

    public touchMove = function(x, y)
	{
		this.mouseMoved(x, y);
	}

    public Focus = function()
	{
	}
}
