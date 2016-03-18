using System;

namespace Multex
{
	/// <summary>
	/// LongInteger の概要の説明です。
	/// </summary>
	public class LongInteger
	{
		private int[] num;

		public LongInteger(int size)
		{
			num = new int[size];
			clear();
		}

		/// <summary>
		/// 0にする
		/// </summary>
		public void clear()
		{
			for ( int i = 0; i < num.Length; i++ )
			{
				num[i] = 0;
			}
		}

		/// <summary>
		/// 左にシフトして、指定した数を加える
		/// </summary>
		/// <param name="n"></param>
		public void shift_left_and_add(int n)
		{
			if ( num[num.Length - 1] == 0 )
			{
				for ( int i = num.Length - 1; i >= 1; i-- )
				{
					num[i] = num[i - 1];
				}
				num[0] = n;
			}
		}

		/// <summary>
		/// 右にシフトする
		/// </summary>
		public void shift_right()
		{
			for ( int i = 0; i < num.Length - 1; i++ )
			{
				num[i] = num[i + 1];
			}
			num[num.Length - 1] = 0;
		}

		/// <summary>
		/// 0かどうかを調べる
		/// </summary>
		/// <returns>0のときtrue</returns>
		public bool is_zero()
		{
			for ( int i = 0; i < num.Length - 1; i++ )
			{
				if ( num[i] != 0 )
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
		public string get_string()
		{
			string s = "";
			for ( int i = 0; i < get_length(); i++ )
			{
				s = Convert.ToString(num[i]) + s;
			}
			return s;
		}

		/// <summary>
		/// 数の桁数を取得する
		/// </summary>
		/// <returns></returns>
		public int get_length()
		{
			int n = 0;
			for ( int i = 0; i < num.Length; i++ )
			{
				if ( num[i] != 0 )
				{
					n = i + 1;
				}
			}
			return n;
		}

		/// <summary>
		/// 数の各桁の数を取得・設定する
		/// </summary>
		public int this[int index]
		{
			get
			{
				if ( index < num.Length )
				{
					return num[index];
				}
				else
				{
					return 0;
				}
			}
			set
			{
				if ( index < num.Length )
				{
					num[index] = value;
				}
			}
		}

		/// <summary>
		/// 乱数で指定した桁数の数を生成する
		/// </summary>
		/// <param name="size">桁数</param>
		/// <param name="ran">乱数</param>
		public void create_new_number(int size, Random ran)
		{
			for ( int i = 0; i < num.Length; i++ )
			{
				num[i] = 0;
			}
			for ( int i = 0; i < size - 1; i++ )
			{
				num[i] = ran.Next(10);
			}
			num[size - 1] = ran.Next(9) + 1;
		}

		/// <summary>
		/// 足し算を行う
		/// </summary>
		/// <param name="n">足す数</param>
		/// <param name="index">足す先の桁</param>
		private void add(int n, int index)
		{
			for ( int i = index; i < num.Length; i++ )
			{
				int s = n + this[i];
				this[i] = s % 10;
				n = s / 10;
			}
		}

		/// <summary>
		/// 掛け算を行う
		/// </summary>
		/// <param name="n">掛ける数</param>
		/// <returns>積</returns>
		public LongInteger multiply(LongInteger n)
		{
			LongInteger res = new LongInteger(get_length() + n.get_length());
			for ( int i = 0; i < get_length(); i++ )
			{
				for ( int j = 0; j < n.get_length(); j++ )
				{
					int m = this[i] * n[j];
					res.add(m % 10, i + j);
					res.add(m / 10, i + j + 1);
				}
			}
			return res;
		}
	}
}
