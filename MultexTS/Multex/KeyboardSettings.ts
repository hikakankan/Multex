class CharItem
{
    public Key: string;
    public Action: KeyAction;
    public Number: number;
    public Shift: boolean;

    public constructor(key: string, action: KeyAction, number: number, shift: boolean) {
        this.Key = key;
        this.Action = action;
        this.Number = number;
        this.Shift = shift;
    }
}

enum KeyAction
{
	None = 0,
	PutNumber = 1,
	MoveHigher = 2,
	MoveLower = 3
}

class KeyboardSettings
{
    private char_setting_list: Array<CharItem>;

    public constructor() {
        this.char_setting_list = new Array<CharItem>();

        this.add_setting('0', KeyAction.PutNumber, 0, false);
        this.add_setting('1', KeyAction.PutNumber, 1, false);
        this.add_setting('2', KeyAction.PutNumber, 2, false);
        this.add_setting('3', KeyAction.PutNumber, 3, false);
        this.add_setting('4', KeyAction.PutNumber, 4, false);
        this.add_setting('5', KeyAction.PutNumber, 5, false);
        this.add_setting('6', KeyAction.PutNumber, 6, false);
        this.add_setting('7', KeyAction.PutNumber, 7, false);
        this.add_setting('8', KeyAction.PutNumber, 8, false);
        this.add_setting('9', KeyAction.PutNumber, 9, false);
        this.add_setting('!', KeyAction.PutNumber, 1, false);
        this.add_setting('\"', KeyAction.PutNumber, 2, false);
        this.add_setting('#', KeyAction.PutNumber, 3, false);
        this.add_setting('$', KeyAction.PutNumber, 4, false);
        this.add_setting('%', KeyAction.PutNumber, 5, false);
        this.add_setting('&', KeyAction.PutNumber, 6, false);
        this.add_setting('\'', KeyAction.PutNumber, 7, false);
        this.add_setting('(', KeyAction.PutNumber, 8, false);
        this.add_setting(')', KeyAction.PutNumber, 9, false);
        this.add_setting(';', KeyAction.PutNumber, 0, false);
        this.add_setting('+', KeyAction.PutNumber, 0, false);
        this.add_setting(',', KeyAction.MoveLower, 0, false);
        this.add_setting('.', KeyAction.MoveLower, 0, false);
        this.add_setting('/', KeyAction.MoveLower, 0, false);
        this.add_setting('<', KeyAction.MoveLower, 0, false);
        this.add_setting('>', KeyAction.MoveLower, 0, false);
        this.add_setting('?', KeyAction.MoveLower, 0, false);
        this.add_setting('*', KeyAction.None, 0, false);
        this.add_setting('-', KeyAction.None, 0, false);
        this.add_setting(':', KeyAction.None, 0, false);
        this.add_setting('=', KeyAction.None, 0, false);
        this.add_setting('@', KeyAction.None, 0, false);
        this.add_setting('[', KeyAction.None, 0, false);
        this.add_setting('\\', KeyAction.None, 0, false);
        this.add_setting(']', KeyAction.None, 0, false);
        this.add_setting('^', KeyAction.None, 0, false);
        this.add_setting('_', KeyAction.None, 0, false);
        this.add_setting('`', KeyAction.None, 0, false);
        this.add_setting('{', KeyAction.None, 0, false);
        this.add_setting('|', KeyAction.None, 0, false);
        this.add_setting('}', KeyAction.None, 0, false);
        this.add_setting('~', KeyAction.None, 0, false);
    }

	/// <summary>
	/// キーと動作を関連づける
	/// </summary>
	/// <param name="key">キー</param>
	/// <param name="action">動作</param>
	/// <param name="number">動作が数を入力する時の数</param>
	/// <param name="shift">シフトキーが押されたかどうか(使わない)</param>
    public add_setting = function (key: string, action: KeyAction, number: number, shift: boolean): void
	{
		this.char_setting_list.push(new CharItem(key, action, number, shift));
	}

	/// <summary>
	/// 指定したキーの動作を取得する
	/// </summary>
	/// <param name="key">キー</param>
	/// <returns>動作</returns>
    public get_action = function (key: string): KeyAction
	{
		for ( var i = 0; i < this.char_setting_list.length; i++ )
		{
			var item = this.char_setting_list[i];
			if ( item.Key == key )
			{
				return item.Action;
			}
		}
		return this.KeyAction.None;
	}

	/// <summary>
	/// 指定したキーの入力する数を取得する
	/// </summary>
	/// <param name="key">キー</param>
	/// <returns>入力する数</returns>
    public get_number = function (key: string): number
	{
		for ( var i = 0; i < this.char_setting_list.length; i++ )
		{
			var item = this.char_setting_list[i];
			if ( item.Key == key )
			{
				return item.Number;
			}
		}
		return 0;
	}

	/// <summary>
	/// 数を入力するキーかどうか
	/// </summary>
	/// <param name="key_char">キー</param>
	/// <returns>数を入力するキーのときtrue</returns>
    public is_calc_key = function (key_char: string): boolean
	{
		return this.get_action(key_char) == this.KeyAction.PutNumber;
	}
}
