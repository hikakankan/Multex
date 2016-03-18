using System;
using System.Xml;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

namespace Multex
{
	/// <summary>
	/// Settings の概要の説明です。
	/// </summary>
	public class Settings
	{
		private const string settings_file_name = "Multex.mxi";

		private ViewSettings view_settings;
		private KeyboardSettings key_settings;

		public Settings()
		{
			view_settings = new ViewSettings();
			key_settings = new KeyboardSettings();
		}

		/// <summary>
		/// 表示の設定を取得する
		/// </summary>
		/// <returns>表示の設定</returns>
		public ViewSettings get_view_settings()
		{
			return view_settings;
		}

		/// <summary>
		/// キーボードの設定を取得する
		/// </summary>
		/// <returns>キーボードの設定</returns>
		public KeyboardSettings get_key_settings()
		{
			return key_settings;
		}

		/// <summary>
		/// XMLのセクションの対応するエレメントを取得する
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="parent">エレメントを探す親のノード</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <param name="create">取得できなかったときは作成する</param>
		/// <returns>セクションに対応するノード</returns>
		private XmlElement get_section(string section, XmlNode parent, XmlDocument doc, bool create)
		{
			if ( parent.HasChildNodes )
			{
				foreach ( XmlNode node in parent.ChildNodes )
				{
					if ( node.NodeType == XmlNodeType.Element && node.Name == section )
					{
						return (XmlElement)node;
					}
				}
			}
			if ( create )
			{
				XmlElement child = doc.CreateElement(section);
				parent.AppendChild(child);
				return child;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void get_setting_string(string section, string key, ref string val, XmlDocument doc)
		{
			try
			{
				XmlElement conf = doc.DocumentElement;
				if ( conf != null )
				{
					XmlElement sec = get_section(section, conf, doc, false);
					if ( sec != null )
					{
						val = sec.GetAttribute(key);
					}
				}
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private string get_setting_string(string section, string key, string val, XmlDocument doc)
		{
			try
			{
				string setting = val;
				get_setting_string(section, key, ref setting, doc);
				return setting;
			}
			catch ( Exception )
			{
				return val;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private int get_setting_integer(string section, string key, int val, XmlDocument doc)
		{
			try
			{
				string setting = "";
				get_setting_string(section, key, ref setting, doc);
				if ( setting != "" )
				{
					return Convert.ToInt32(setting);
				}
				return val;
			}
			catch ( Exception )
			{
				return val;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private bool get_setting_bool(string section, string key, bool val, XmlDocument doc)
		{
			try
			{
				string setting = "";
				get_setting_string(section, key, ref setting, doc);
				if ( setting != "" )
				{
					return setting != "f";
				}
				return val;
			}
			catch ( Exception )
			{
				return val;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private Color get_setting_color(string section, string key, Color val, XmlDocument doc)
		{
			try
			{
				string setting = "";
				get_setting_string(section, key, ref setting, doc);
				if ( setting != "" )
				{
					return Color.FromArgb(Convert.ToInt32(setting, 16));
				}
				return val;
			}
			catch ( Exception )
			{
				return val;
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_string(string section, string key, string val, XmlDocument doc)
		{
			try
			{
				XmlElement conf = doc.DocumentElement;
				if ( conf == null )
				{
					conf = doc.CreateElement("configuration");
					doc.AppendChild(conf);
				}
				get_section(section, conf, doc, true).SetAttribute(key, val);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_integer(string section, string key, int val, XmlDocument doc)
		{
			try
			{
				set_setting_string(section, key, Convert.ToString(val), doc);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_bool(string section, string key, bool val, XmlDocument doc)
		{
			try
			{
				if ( val )
				{
					set_setting_string(section, key, "t", doc);
				}
				else
				{
					set_setting_string(section, key, "f", doc);
				}
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_color(string section, string key, Color val, XmlDocument doc)
		{
			try
			{
				set_setting_string(section, key, Convert.ToString(val.ToArgb(), 16), doc);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="path">設定ファイルのパス</param>
		public void load_settings(string path)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				XmlTextReader reader = new XmlTextReader(path);
				doc.Load(reader);
				reader.Close();

				left = get_setting_integer("window", "left", left, doc);
				top = get_setting_integer("window", "top", top, doc);
				width = get_setting_integer("window", "width", width, doc);
				height = get_setting_integer("window", "height", height, doc);

				mag1 = get_setting_integer("oparation", "mag1", mag1, doc);
				mag2 = get_setting_integer("oparation", "mag2", mag2, doc);
				single_op_mode = get_setting_bool("oparation", "single", single_op_mode, doc);
				auto_ans_mode = get_setting_bool("oparation", "auto", auto_ans_mode, doc);

				view_settings.FormatCorrect = get_setting_string("format", "ok", view_settings.FormatCorrect, doc);
				view_settings.FormatCorrectAndTime = get_setting_string("format", "okwt", view_settings.FormatCorrectAndTime, doc);
				view_settings.FormatIncorrect = get_setting_string("format", "no", view_settings.FormatIncorrect, doc);
				view_settings.FormatIncorrectAndTime = get_setting_string("format", "nowt", view_settings.FormatIncorrectAndTime, doc);

				view_settings.BodyBackColor = get_setting_color("color", "body", view_settings.BodyBackColor, doc);
				view_settings.BodyTextColor = get_setting_color("color", "bodytext", view_settings.BodyTextColor, doc);
				view_settings.ButtonBackColor = get_setting_color("color", "button", view_settings.ButtonBackColor, doc);
				view_settings.ButtonTextColor = get_setting_color("color", "buttontext", view_settings.ButtonTextColor, doc);
				view_settings.TextBackColor = get_setting_color("color", "textback", view_settings.TextBackColor, doc);
				view_settings.TextTextColor = get_setting_color("color", "text", view_settings.TextTextColor, doc);
				view_settings.CalcAreaTextColor = get_setting_color("color", "calctext", view_settings.CalcAreaTextColor, doc);
				view_settings.CalcAreaBackColor = get_setting_color("color", "calcback", view_settings.CalcAreaBackColor, doc);
				view_settings.CalcAreaFrameColor = get_setting_color("color", "calcframe", view_settings.CalcAreaFrameColor, doc);
				view_settings.CalcAreaFrameBackColor = get_setting_color("color", "calcframeback", view_settings.CalcAreaFrameBackColor, doc);
				view_settings.CalcAreaUnderlineColor = get_setting_color("color", "calcunderline", view_settings.CalcAreaUnderlineColor, doc);
				view_settings.CalcAreaSourceBackColor = get_setting_color("color", "calcsrc", view_settings.CalcAreaSourceBackColor, doc);
				view_settings.CalcAreaDestinationBackColor = get_setting_color("color", "calcdst", view_settings.CalcAreaDestinationBackColor, doc);
				view_settings.CalcAreaUpdatingBackColor = get_setting_color("color", "calcupd", view_settings.CalcAreaUpdatingBackColor, doc);

				for ( int i = 0; i < key_settings.key_list_count(); i++ )
				{
					System.Windows.Forms.Keys key = key_settings.key_item(i);
					string key_str = key_settings.key_item_string(i);
					string action = "default";
					get_setting_string("key", key_str, ref action, doc);
					if ( action != "default" )
					{
						key_settings.set_action(key, action);
					}
				}
				for ( int i = 0; i < key_settings.char_list_count(); i++ )
				{
					char key = key_settings.char_item(i);
					string key_str = key_settings.char_item_string(i);
					string action = "default";
					get_setting_string("key", key_str, ref action, doc);
					if ( action != "default" )
					{
						key_settings.set_action(key, action);
					}
				}
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		public void load_settings()
		{
			load_settings(settings_file_name);
		}

		private string file_path;

		/// <summary>
		/// ファイルダイアログでファイル名を指定して設定を読み込む
		/// </summary>
		/// <returns>読み込んだときtrue、キャンセルのときfalse</returns>
		public bool load_settings_file()
		{
			try
			{
				if ( file_path == null )
				{
					file_path = settings_file_name;
				}
				System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
				openFileDialog1.Filter = "情報ファイル (*.mxi)|*.mxi|すべてのファイル (*.*)|*.*";
				openFileDialog1.FilterIndex = 1;
				openFileDialog1.FileName = file_path;
				openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
				openFileDialog1.RestoreDirectory = true;
				if ( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
				{
					file_path = openFileDialog1.FileName;
					load_settings(file_path);
					return true;
				}
			}
			catch ( Exception )
			{
			}
			return false;
		}

		/// <summary>
		/// 設定を保存する
		/// </summary>
		/// <param name="path">設定ファイルのパス</param>
		public void save_settings(string path)
		{
			try
			{
				XmlDocument doc = new XmlDocument();

				set_setting_integer("window", "left", left, doc);
				set_setting_integer("window", "top", top, doc);
				set_setting_integer("window", "width", width, doc);
				set_setting_integer("window", "height", height, doc);

				set_setting_integer("oparation", "mag1", mag1, doc);
				set_setting_integer("oparation", "mag2", mag2, doc);
				set_setting_bool("oparation", "single", single_op_mode, doc);
				set_setting_bool("oparation", "auto", auto_ans_mode, doc);

				set_setting_string("format", "ok", view_settings.FormatCorrect, doc);
				set_setting_string("format", "okwt", view_settings.FormatCorrectAndTime, doc);
				set_setting_string("format", "no", view_settings.FormatIncorrect, doc);
				set_setting_string("format", "nowt", view_settings.FormatIncorrectAndTime, doc);

				set_setting_color("color", "body", view_settings.BodyBackColor, doc);
				set_setting_color("color", "bodytext", view_settings.BodyTextColor, doc);
				set_setting_color("color", "button", view_settings.ButtonBackColor, doc);
				set_setting_color("color", "buttontext", view_settings.ButtonTextColor, doc);
				set_setting_color("color", "textback", view_settings.TextBackColor, doc);
				set_setting_color("color", "text", view_settings.TextTextColor, doc);
				set_setting_color("color", "calctext", view_settings.CalcAreaTextColor, doc);
				set_setting_color("color", "calcback", view_settings.CalcAreaBackColor, doc);
				set_setting_color("color", "calcframe", view_settings.CalcAreaFrameColor, doc);
				set_setting_color("color", "calcframeback", view_settings.CalcAreaFrameBackColor, doc);
				set_setting_color("color", "calcunderline", view_settings.CalcAreaUnderlineColor, doc);
				set_setting_color("color", "calcsrc", view_settings.CalcAreaSourceBackColor, doc);
				set_setting_color("color", "calcdst", view_settings.CalcAreaDestinationBackColor, doc);
				set_setting_color("color", "calcupd", view_settings.CalcAreaUpdatingBackColor, doc);

				int count = key_settings.get_key_settings_count();
				for ( int i = 0; i < count; i++ )
				{
					string key = key_settings.get_key_string(i);
					string action = key_settings.get_key_action_string(i);
					set_setting_string("key", key, action, doc);
				}
				count = key_settings.get_char_settings_count();
				for ( int i = 0; i < count; i++ )
				{
					string key = key_settings.get_char_string(i);
					string action = key_settings.get_char_action_string(i);
					set_setting_string("key", key, action, doc);
				}

				XmlTextWriter writer = new XmlTextWriter(path, null);
				writer.Formatting = Formatting.Indented;
				doc.Save(writer);
				writer.Close();
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を保存する
		/// </summary>
		public void save_settings()
		{
			save_settings(settings_file_name);
		}

		/// <summary>
		/// ファイルダイアログでファイル名を指定して設定を保存する
		/// </summary>
		public void save_settings_file()
		{
			try
			{
				System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
				saveFileDialog1.AddExtension = true;
				saveFileDialog1.DefaultExt = ".txt";
				saveFileDialog1.OverwritePrompt = true;
				saveFileDialog1.Filter = "情報ファイル (*.mxi)|*.mxi|すべてのファイル (*.*)|*.*";
				saveFileDialog1.FilterIndex = 1;
				saveFileDialog1.FileName = "";
				saveFileDialog1.CheckPathExists = true;
				saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
				saveFileDialog1.RestoreDirectory = true;
				if ( saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
				{
					file_path = saveFileDialog1.FileName;
					save_settings(file_path);
				}
			}
			catch ( Exception )
			{
			}
		}

		private int left = 80;
		private int top = 80;
		private int width = 292;
		private int height = 390;
		private int mag1 = 1;
		private int mag2 = 1;
		private bool single_op_mode = false;
		private bool auto_ans_mode = false;

		/// <summary>
		/// ウィンドウの位置(左)
		/// </summary>
		public int Left
		{
			get
			{
				return left;
			}
			set
			{
				left = value;
			}
		}

		/// <summary>
		/// ウィンドウの位置(上)
		/// </summary>
		public int Top
		{
			get
			{
				return top;
			}
			set
			{
				top = value;
			}
		}

		/// <summary>
		/// ウィンドウの幅
		/// </summary>
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
			}
		}

		/// <summary>
		/// ウィンドウの高さ
		/// </summary>
		public int Height
		{
			get
			{
				return height;
			}
			set
			{
				height = value;
			}
		}

		/// <summary>
		/// かける数１の桁数
		/// </summary>
		public int Magnitude1
		{
			get
			{
				return mag1;
			}
			set
			{
				mag1 = value;
			}
		}

		/// <summary>
		/// かける数２の桁数
		/// </summary>
		public int Magnitude2
		{
			get
			{
				return mag2;
			}
			set
			{
				mag2 = value;
			}
		}

		/// <summary>
		/// １桁ずつ計算するモード
		/// </summary>
		public bool SingleOperationMode
		{
			get
			{
				return single_op_mode;
			}
			set
			{
				single_op_mode = value;
			}
		}

		/// <summary>
		/// 答えを自動表示するモード
		/// </summary>
		public bool AutoAnswerMode
		{
			get
			{
				return auto_ans_mode;
			}
			set
			{
				auto_ans_mode = value;
			}
		}
	}
}
