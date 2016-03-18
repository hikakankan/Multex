using System;
using System.Windows.Forms;

namespace Multex
{
	/// <summary>
	/// KeyboardSettings の概要の説明です。
	/// </summary>
	public class KeyboardSettings
	{
		private System.Collections.ArrayList key_setting_list;
		private System.Collections.ArrayList char_setting_list;
		public enum KeyAction { None, PutNumber, MoveHigher, MoveLower }

		private Keys[] key_list = new Keys[] {
												 Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4,
												 Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9,
												 Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
												 Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
												 Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
												 Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
												 Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z
											 };

		private char[] char_list = new char[] {
												  '1',  '2',  '3',  '4',  '5',  '6',  '7',  '8', '9', '0',
												  '!',  '\"', '#',  '$',  '%',  '&',  '\'', '(', ')',
												  '*',  '+',  ',',  '-',  '.',  '/',
												  ':',  ';',  '<',  '=',  '>',  '?',  '@',
												  '[',  '\\', ']',  '^',  '_',  '`',
												  '{',  '|',  '}',  '~'
											  };

		/// <summary>
		/// キーの個数を取得する
		/// </summary>
		/// <returns>キーの個数</returns>
		public int key_list_count()
		{
			return key_list.Length;
		}

		/// <summary>
		/// 指定したインデックスのキーを取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>キー</returns>
		public Keys key_item(int index)
		{
			if ( index < key_list.Length )
			{
				return key_list[index];
			}
			return 0;
		}

		/// <summary>
		/// 指定したインデックスのキーを表す文字列を取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>キーを表す文字列</returns>
		public string key_item_string(int index)
		{
			return "k" + Convert.ToString((int)key_item(index), 16);
		}

		/// <summary>
		/// キーの個数を取得する
		/// </summary>
		/// <returns>キーの個数</returns>
		public int char_list_count()
		{
			return char_list.Length;
		}

		/// <summary>
		/// 指定したインデックスのキーを取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>キー</returns>
		public char char_item(int index)
		{
			if ( index < char_list.Length )
			{
				return char_list[index];
			}
			return '\0';
		}

		/// <summary>
		/// 指定したインデックスのキーを表す文字列を取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>キーを表す文字列</returns>
		public string char_item_string(int index)
		{
			return "c" + Convert.ToString((int)char_item(index), 16);
		}

		/// <summary>
		/// 動作と数を動作を表す文字列に変換する
		/// </summary>
		/// <param name="action">動作</param>
		/// <param name="n">動作が数を入力するときの数</param>
		/// <returns>動作を表す文字列</returns>
		private string action_to_string(KeyAction action, int n)
		{
			switch ( action )
			{
				case KeyboardSettings.KeyAction.None:
					return "";
				case KeyboardSettings.KeyAction.PutNumber:
					return Convert.ToString(n);
				case KeyboardSettings.KeyAction.MoveHigher:
					return "L";
				case KeyboardSettings.KeyAction.MoveLower:
					return "R";
			}
			return "";
		}

		/// <summary>
		/// 動作を表す文字列を動作と数に変換する
		/// </summary>
		/// <param name="action">動作を表す文字列</param>
		/// <param name="n">動作が数を入力するときの数</param>
		/// <returns>動作</returns>
		private KeyAction string_to_action(string action, ref int n)
		{
			try
			{
				if ( action.Length == 1 )
				{
					KeyAction a;
					if ( action == "L" )
					{
						a = KeyAction.MoveHigher;
					}
					else if ( action == "R" )
					{
						a = KeyAction.MoveLower;
					}
					else
					{
						a = KeyAction.PutNumber;
						n = Convert.ToInt32(action);
					}
					return a;
				}
				return KeyAction.None;
			}
			catch ( Exception )
			{
				return KeyAction.None;
			}
		}

		public KeyboardSettings()
		{
			key_setting_list = new System.Collections.ArrayList();
			char_setting_list = new System.Collections.ArrayList();
			add_setting(Keys.D0, KeyAction.PutNumber, 0, false);
			add_setting(Keys.D1, KeyAction.PutNumber, 1, false);
			add_setting(Keys.D2, KeyAction.PutNumber, 2, false);
			add_setting(Keys.D3, KeyAction.PutNumber, 3, false);
			add_setting(Keys.D4, KeyAction.PutNumber, 4, false);
			add_setting(Keys.D5, KeyAction.PutNumber, 5, false);
			add_setting(Keys.D6, KeyAction.PutNumber, 6, false);
			add_setting(Keys.D7, KeyAction.PutNumber, 7, false);
			add_setting(Keys.D8, KeyAction.PutNumber, 8, false);
			add_setting(Keys.D9, KeyAction.PutNumber, 9, false);
			add_setting(Keys.A, KeyAction.PutNumber, 1, false);
			add_setting(Keys.S, KeyAction.PutNumber, 2, false);
			add_setting(Keys.D, KeyAction.PutNumber, 3, false);
			add_setting(Keys.F, KeyAction.PutNumber, 4, false);
			add_setting(Keys.G, KeyAction.PutNumber, 5, false);
			add_setting(Keys.H, KeyAction.PutNumber, 6, false);
			add_setting(Keys.J, KeyAction.PutNumber, 7, false);
			add_setting(Keys.K, KeyAction.PutNumber, 8, false);
			add_setting(Keys.L, KeyAction.PutNumber, 9, false);
			add_setting(Keys.Q, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.W, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.E, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.R, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.T, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.Y, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.U, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.I, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.O, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.P, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.Z, KeyAction.MoveLower, 0, false);
			add_setting(Keys.X, KeyAction.MoveLower, 0, false);
			add_setting(Keys.C, KeyAction.MoveLower, 0, false);
			add_setting(Keys.V, KeyAction.MoveLower, 0, false);
			add_setting(Keys.B, KeyAction.MoveLower, 0, false);
			add_setting(Keys.N, KeyAction.MoveLower, 0, false);
			add_setting(Keys.M, KeyAction.MoveLower, 0, false);
			add_setting(Keys.Space, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.Return, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.Enter, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.Back, KeyAction.MoveLower, 0, false);
			add_setting(Keys.Delete, KeyAction.MoveLower, 0, false);
			add_setting(Keys.Left, KeyAction.MoveHigher, 0, false);
			add_setting(Keys.Right, KeyAction.MoveLower, 0, false);
			add_setting(Keys.Up, KeyAction.MoveLower, 0, false);
			add_setting(Keys.Down, KeyAction.MoveHigher, 0, false);

			add_setting('0', KeyAction.PutNumber, 0, false);
			add_setting('1', KeyAction.PutNumber, 1, false);
			add_setting('2', KeyAction.PutNumber, 2, false);
			add_setting('3', KeyAction.PutNumber, 3, false);
			add_setting('4', KeyAction.PutNumber, 4, false);
			add_setting('5', KeyAction.PutNumber, 5, false);
			add_setting('6', KeyAction.PutNumber, 6, false);
			add_setting('7', KeyAction.PutNumber, 7, false);
			add_setting('8', KeyAction.PutNumber, 8, false);
			add_setting('9', KeyAction.PutNumber, 9, false);
			add_setting('!', KeyAction.PutNumber, 1, false);
			add_setting('\"', KeyAction.PutNumber, 2, false);
			add_setting('#', KeyAction.PutNumber, 3, false);
			add_setting('$', KeyAction.PutNumber, 4, false);
			add_setting('%', KeyAction.PutNumber, 5, false);
			add_setting('&', KeyAction.PutNumber, 6, false);
			add_setting('\'', KeyAction.PutNumber, 7, false);
			add_setting('(', KeyAction.PutNumber, 8, false);
			add_setting(')', KeyAction.PutNumber, 9, false);
			add_setting(';', KeyAction.PutNumber, 0, false);
			add_setting('+', KeyAction.PutNumber, 0, false);
			add_setting(',', KeyAction.MoveLower, 0, false);
			add_setting('.', KeyAction.MoveLower, 0, false);
			add_setting('/', KeyAction.MoveLower, 0, false);
			add_setting('<', KeyAction.MoveLower, 0, false);
			add_setting('>', KeyAction.MoveLower, 0, false);
			add_setting('?', KeyAction.MoveLower, 0, false);
			add_setting('*', KeyAction.None, 0, false);
			add_setting('-', KeyAction.None, 0, false);
			add_setting(':', KeyAction.None, 0, false);
			add_setting('=', KeyAction.None, 0, false);
			add_setting('@', KeyAction.None, 0, false);
			add_setting('[', KeyAction.None, 0, false);
			add_setting('\\', KeyAction.None, 0, false);
			add_setting(']', KeyAction.None, 0, false);
			add_setting('^', KeyAction.None, 0, false);
			add_setting('_', KeyAction.None, 0, false);
			add_setting('`', KeyAction.None, 0, false);
			add_setting('{', KeyAction.None, 0, false);
			add_setting('|', KeyAction.None, 0, false);
			add_setting('}', KeyAction.None, 0, false);
			add_setting('~', KeyAction.None, 0, false);
		}

		/// <summary>
		/// キーと動作を関連づける
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="action">動作</param>
		/// <param name="number">動作が数を入力する時の数</param>
		/// <param name="shift">シフトキーが押されたかどうか(使わない)</param>
		private void add_setting(Keys key, KeyAction action, int number, bool shift)
		{
			key_setting_list.Add(new KeyItem(key, action, number, shift));
		}

		/// <summary>
		/// 指定したキーの動作を取得する
		/// </summary>
		/// <param name="key">キー</param>
		/// <returns>動作</returns>
		public KeyAction get_action(Keys key)
		{
			System.Diagnostics.Debug.WriteLine("get_action " + Convert.ToString(key));
			for ( int i = 0; i < key_setting_list.Count; i++ )
			{
				KeyItem item = (KeyItem)key_setting_list[i];
				if ( item.Key == key )
				{
					return item.Action;
				}
			}
			return KeyAction.None;
		}

		/// <summary>
		/// 指定したキーの動作を表す文字列を取得する
		/// </summary>
		/// <param name="key">キー</param>
		/// <returns>動作を表す文字列</returns>
		public string get_action_string(Keys key)
		{
			return action_to_string(get_action(key), get_number(key));
		}

		/// <summary>
		/// 指定したキーの入力する数を取得する
		/// </summary>
		/// <param name="key">キー</param>
		/// <returns>入力する数</returns>
		public int get_number(Keys key)
		{
			for ( int i = 0; i < key_setting_list.Count; i++ )
			{
				KeyItem item = (KeyItem)key_setting_list[i];
				if ( item.Key == key )
				{
					return item.Number;
				}
			}
			return 0;
		}

		/// <summary>
		/// 指定したキーに動作と数を指定する
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="action">動作</param>
		/// <param name="number">数</param>
		private void set_action(Keys key, KeyAction action, int number)
		{
			for ( int i = 0; i < key_setting_list.Count; i++ )
			{
				KeyItem item = (KeyItem)key_setting_list[i];
				if ( item.Key == key )
				{
					item.Action = action;
					item.Number = number;
					return;
				}
			}
			key_setting_list.Add(new KeyItem(key, action, number, false));
		}

		/// <summary>
		/// 指定したキーに文字列から動作を指定する
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="action">動作を表す文字列</param>
		public void set_action(Keys key, string action)
		{
			int n = 0;
			KeyAction a = string_to_action(action, ref n);
			set_action(key, a, n);
		}

		/// <summary>
		/// 数を入力するキーかどうか
		/// </summary>
		/// <param name="key_char">キー</param>
		/// <returns>数を入力するキーのときtrue</returns>
		public bool is_calc_key(Keys key)
		{
			return get_action(key) == KeyAction.PutNumber;
		}

		/// <summary>
		/// キーの個数を取得する
		/// </summary>
		/// <returns>キーの個数</returns>
		public int get_key_settings_count()
		{
			return key_setting_list.Count;
		}

		/// <summary>
		/// 指定したインデックスのキーを表す文字列を取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>動作を表す文字列</returns>
		public string get_key_string(int index)
		{
			if ( index < key_setting_list.Count )
			{
				Keys k = ((KeyItem)key_setting_list[index]).Key;
				return "k" + Convert.ToString((int)k, 16);
			}
			return "k0";
		}

		/// <summary>
		/// 指定したインデックスの動作を表す文字列を取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>動作を表す文字列</returns>
		public string get_key_action_string(int index)
		{
			if ( index < key_setting_list.Count )
			{
				KeyAction action = ((KeyItem)key_setting_list[index]).Action;
				return action_to_string(action, ((KeyItem)key_setting_list[index]).Number);
			}
			return "";
		}

		/// <summary>
		/// キーと動作を関連づける
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="action">動作</param>
		/// <param name="number">動作が数を入力する時の数</param>
		/// <param name="shift">シフトキーが押されたかどうか(使わない)</param>
		private void add_setting(char key, KeyAction action, int number, bool shift)
		{
			char_setting_list.Add(new CharItem(key, action, number, shift));
		}

		/// <summary>
		/// 指定したキーの動作を取得する
		/// </summary>
		/// <param name="key">キー</param>
		/// <returns>動作</returns>
		public KeyAction get_action(char key)
		{
			System.Diagnostics.Debug.WriteLine("get_action " + Convert.ToString(key));
			for ( int i = 0; i < char_setting_list.Count; i++ )
			{
				CharItem item = (CharItem)char_setting_list[i];
				if ( item.Key == key )
				{
					return item.Action;
				}
			}
			return KeyAction.None;
		}

		/// <summary>
		/// 指定したキーの動作を表す文字列を取得する
		/// </summary>
		/// <param name="key">キー</param>
		/// <returns>動作を表す文字列</returns>
		public string get_action_string(char key)
		{
			return action_to_string(get_action(key), get_number(key));
		}

		/// <summary>
		/// 指定したキーの入力する数を取得する
		/// </summary>
		/// <param name="key">キー</param>
		/// <returns>入力する数</returns>
		public int get_number(char key)
		{
			for ( int i = 0; i < char_setting_list.Count; i++ )
			{
				CharItem item = (CharItem)char_setting_list[i];
				if ( item.Key == key )
				{
					return item.Number;
				}
			}
			return 0;
		}

		/// <summary>
		/// 指定したキーに動作と数を指定する
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="action">動作</param>
		/// <param name="number">数</param>
		private void set_action(char key, KeyAction action, int number)
		{
			for ( int i = 0; i < char_setting_list.Count; i++ )
			{
				CharItem item = (CharItem)char_setting_list[i];
				if ( item.Key == key )
				{
					item.Action = action;
					item.Number = number;
					return;
				}
			}
			char_setting_list.Add(new CharItem(key, action, number, false));
		}

		/// <summary>
		/// 指定したキーに文字列から動作を指定する
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="action">動作を表す文字列</param>
		public void set_action(char key, string action)
		{
			int n = 0;
			KeyAction a = string_to_action(action, ref n);
			set_action(key, a, n);
		}

		/// <summary>
		/// 数を入力するキーかどうか
		/// </summary>
		/// <param name="key_char">キー</param>
		/// <returns>数を入力するキーのときtrue</returns>
		public bool is_calc_key(char key_char)
		{
			return get_action(key_char) == KeyAction.PutNumber;
		}

		/// <summary>
		/// キーの個数を取得する
		/// </summary>
		/// <returns>キーの個数</returns>
		public int get_char_settings_count()
		{
			return char_setting_list.Count;
		}

		/// <summary>
		/// 指定したインデックスのキーを表す文字列を取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>動作を表す文字列</returns>
		public string get_char_string(int index)
		{
			if ( index < char_setting_list.Count )
			{
				char c = ((CharItem)char_setting_list[index]).Key;
				return "c" + Convert.ToString((int)c, 16);
			}
			return "c0";
		}

		/// <summary>
		/// 指定したインデックスの動作を表す文字列を取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>動作を表す文字列</returns>
		public string get_char_action_string(int index)
		{
			if ( index < char_setting_list.Count )
			{
				KeyAction action = ((CharItem)char_setting_list[index]).Action;
				return action_to_string(action, ((CharItem)char_setting_list[index]).Number);
			}
			return "";
		}
	}
}
