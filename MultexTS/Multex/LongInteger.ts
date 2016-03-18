class LongInteger
{
    private num: Array<number>;

    public constructor(size: number) {
        this.num = new Array<number>(size);
        this.clear();
    }

	/// <summary>
	/// 0にする
	/// </summary>
	public clear = function(): void
	{
		for ( var i = 0; i < this.num.length; i++ )
		{
			this.num[i] = 0;
		}
	}

	/// <summary>
	/// 左にシフトして、指定した数を加える
	/// </summary>
	/// <param name="n"></param>
    public shift_left_and_add = function (n: number): void
	{
		if ( this.num[this.num.length - 1] == 0 )
		{
			for ( var i = this.num.length - 1; i >= 1; i-- )
			{
				this.num[i] = this.num[i - 1];
			}
			this.num[0] = n;
		}
	}

	/// <summary>
	/// 右にシフトする
	/// </summary>
    public shift_right = function (): void
	{
		for ( var i = 0; i < this.num.length - 1; i++ )
		{
			this.num[i] = this.num[i + 1];
		}
		this.num[this.num.length - 1] = 0;
	}

	/// <summary>
	/// 0かどうかを調べる
	/// </summary>
	/// <returns>0のときtrue</returns>
    public is_zero = function(): boolean
	{
		for ( var i = 0; i < this.num.length - 1; i++ )
		{
			if ( this.num[i] != 0 )
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// 数を文字列に変換した物を取得する
	/// </summary>
	/// <returns></returns>
    public get_string = function(): string
	{
		var s = "";
		for ( var i = 0; i < this.get_length(); i++ )
		{
			s = String(this.num[i]) + s;
		}
		return s;
	}

	/// <summary>
	/// 数の桁数を取得する
	/// </summary>
	/// <returns></returns>
    public get_length = function(): number
	{
		var n = 0;
		for ( var i = 0; i < this.num.length; i++ )
		{
			if ( this.num[i] != 0 )
			{
				n = i + 1;
			}
		}
		return n;
	}

	/// <summary>
	/// 数の各桁の数を取得・設定する
	/// </summary>
    public get = function (index: number): number
	{
		if ( index < this.num.length )
		{
			return this.num[index];
		}
		else
		{
			return 0;
		}
	}
    public set = function (index: number, value: number): void
	{
		if ( index < this.num.length )
		{
			this.num[index] = value;
		}
	}

	/// <summary>
	/// 乱数で指定した桁数の数を生成する
	/// </summary>
	/// <param name="size">桁数</param>
	/// <param name="ran">乱数</param>
    public create_new_number = function (size: number, ran: CSRandom): void
	{
		for ( var i = 0; i < this.num.length; i++ )
		{
			this.num[i] = 0;
		}
		for ( var i = 0; i < size - 1; i++ )
		{
			this.num[i] = ran.Next(10);
		}
		this.num[size - 1] = ran.Next(9) + 1;
	}

	/// <summary>
	/// 足し算を行う
	/// </summary>
	/// <param name="n">足す数</param>
	/// <param name="index">足す先の桁</param>
    public add = function (n: number, index: number): void
	{
		for ( var i = index; i < this.num.length; i++ )
		{
			var s = n + this.num[i];
			this.num[i] = s % 10;
			n = Math.floor(s / 10);
		}
	}

	/// <summary>
	/// 掛け算を行う
	/// </summary>
	/// <param name="n">掛ける数</param>
	/// <returns>積</returns>
    public multiply = function (n: LongInteger): LongInteger
	{
		var res = new LongInteger(this.get_length() + n.get_length());
		for ( var i = 0; i < this.get_length(); i++ )
		{
			for ( var j = 0; j < n.get_length(); j++ )
			{
				var m = this[i] * n[j];
				res.add(m % 10, i + j);
				res.add(m / 10, i + j + 1);
			}
		}
		return res;
	}
}
