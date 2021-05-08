using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Stuffie
{
	public class HexConvert
	{
		public static int conversionmode = 1;

		public static bool togglemode = false;

		public static bool modechange = false;

		public static void Start()
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"Welcome to domedavs hexconverter");
			Console.ResetColor();
			Help();
			linetypedesign = true;
		}

		private static bool linetypedesign = false;
		public static void Update()
		{
			if (linetypedesign)
			{
				Console.Write("|");
			}
			try
			{
				ReadConsole(Console.ReadLine().ToString());
			}
			catch
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Something Went Wrong While Trying To Read Input");
				Console.ResetColor();
			}
		}

		public static void ReadConsole(string consolestring)
		{
			if (consolestring.Equals(string.Empty))
			{
				Console.SetCursorPosition(0, Console.CursorTop - 1);
				return;
			}
			if (consolestring.Equals("/help"))
			{
				Help();
				return;
			}
			if (consolestring.Equals("/int") && modechange)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				conversionmode = 1;
				modechange = false;
				Console.WriteLine($"Current conversion mode: {Getmodetype()}");
				Console.ResetColor();
				return;
			}
			if (consolestring.Equals("/float") && modechange)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				conversionmode = 2;
				modechange = false;
				Console.WriteLine($"Current conversion mode: {Getmodetype()}");
				Console.ResetColor();
				return;
			}
			if (consolestring.Equals("/string") && modechange)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				conversionmode = 3;
				modechange = false;
				Console.WriteLine($"Current conversion mode: {Getmodetype()}");
				Console.ResetColor();
				return;
			}
			if (consolestring.Equals("/toggle") && modechange)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				togglemode = !togglemode;
				modechange = false;
				Console.WriteLine($"Current conversion mode: {Getmodetype()}");
				Console.ResetColor();
				return;
			}
			if (consolestring.Equals("/change"))
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"Now you can change up stuff by using /int, /float, etc...");
				Console.ResetColor();
				modechange = true;
				return;
			}
			Hexify(consolestring);
		}

		public static string Getmodetype()
		{
			string modetype = "Error While Obtaining Mode";
			if (conversionmode == 1 && !togglemode)
			{
				modetype = "INT to HEX";
			}
			if (conversionmode == 2 && !togglemode)
			{
				modetype = "FLOAT to HEX";
			}
			if (conversionmode == 3 && !togglemode)
			{
				modetype = "STRING to HEX";
			}

			if (conversionmode == 1 && togglemode)
			{
				modetype = "HEX to INT";
			}
			if (conversionmode == 2 && togglemode)
			{
				modetype = "HEX to FLOAT";
			}
			if (conversionmode == 3 && togglemode)
			{
				modetype = "HEX to STRING";
			}
			return modetype;
		}

		public static void Help()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"Type '/change' to change modes,\nYou have to type it before using any of the commands bellow except /help\nCurrent mode is: {Getmodetype()}\nType '/int' to set numbers to int\nType '/float' to set numbers to float\nType '/string' to set text to string\nType '/toggle' to toggle between modes (something to hex, hex to something)\nType '/help' to see this message again\n");
			Console.ResetColor();
		}

		public static string CombineTextAsHex(string str)
		{
			int repeate = 0;
			char[] chars;
			if (conversionmode == 1)
			{
				repeate = 8;
			}
			if (conversionmode == 2)
			{
				repeate = 16;
			}
			if (conversionmode == 3)
			{
				repeate = str.Count();
			}

			chars = new char[repeate];
			char[] givenchars = str.ToCharArray();
			for (int i = 0; i < repeate; i++)
			{

				if (i < givenchars.Length)
				{
					chars[repeate - i - 1] = givenchars[givenchars.Length - i - 1];
				}
				else
				{
					chars[repeate - i - 1] = '0';
				}
			}
			int seperatetwo = 0;
			string combinedstr = string.Empty;
			for (int a = 0; a < repeate; a++)
			{
				if (seperatetwo < 2)
				{
					combinedstr = $"{combinedstr}{chars[a]}";
					seperatetwo++;
				}
				else
				{
					combinedstr = $"{combinedstr} {chars[a]}";
					seperatetwo = 1;
				}
			}
			return combinedstr;
		}

		public static void PrintHexDat(string header, string text)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine($"{header}: {text}\n");
			Console.ResetColor();
		}

		public static void Hexify(string Consolestring)
		{
			if (conversionmode == 1 && !togglemode)
			{
				if (!int.TryParse(Consolestring, out int val))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Conversion of INT \"{Consolestring}\" to HEX Failed\nThe reason might be that the given number is out of the intiger value range, or its not a number\n");
					Console.ResetColor();
					return;
				}
				int intiger = BitConverter.ToInt32(BitConverter.GetBytes(val), 0);
				PrintHexDat("INT to HEX", CombineTextAsHex(intiger.ToString("X")));
				return;
			}
			else if (conversionmode == 1 && togglemode)
			{
				string convertedstr = Consolestring.Replace(" ", string.Empty);
				CultureInfo prov = new CultureInfo("en-US");
				if (!int.TryParse(convertedstr, NumberStyles.HexNumber, prov, out int val))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Conversion of HEX \"{Consolestring}\" to INT Failed\nThe reason might be that the given HEX is out of the intiger value range, or its not a valid HEX number\n");
					Console.ResetColor();
					return;
				}
				PrintHexDat("HEX to INT", val.ToString());
				return;
			}

			if (conversionmode == 2 && !togglemode)
			{
				string convablestring = Consolestring.Replace('.', ',');

				if (!double.TryParse(convablestring, out double val))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Conversion of FLOAT \"{Consolestring}\" to HEX Failed\nThe reason might be that the given number is out of the floatingpoint value range, or its not a number\n");
					Console.ResetColor();
					return;
				}
				long floating = BitConverter.DoubleToInt64Bits(val);
				PrintHexDat("FLOAT to HEX", CombineTextAsHex(floating.ToString("X")));
				return;
			}
			else if (conversionmode == 2 && togglemode)
			{
				string convablestring = Consolestring.Replace('.', ',');
				string convertedstr = convablestring.Replace(" ", string.Empty);

				CultureInfo prov = new CultureInfo("en-US");
				if (long.TryParse(convertedstr, NumberStyles.HexNumber, prov, out long val))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Conversion of HEX \"{Consolestring}\" to FLOAT Failed\nThe reason might be that the given HEX is out of the floatingpoint value range, or its not a valid HEX number\n");
					Console.ResetColor();
					Console.WriteLine(val.ToString());
					return;
				}
				return;
			}

			bool conved_ansi = false;
			bool conved_ascii = false;
			bool conved_utf8 = false;
			if (conversionmode == 3 && !togglemode)
			{
				//
				byte[] ansi = Encoding.Default.GetBytes(Consolestring);
				byte[] ascii = Encoding.ASCII.GetBytes(Consolestring);
				byte[] utf8 = Encoding.UTF8.GetBytes(Consolestring);
				//

				//
				string ansi_conv = BitConverter.ToString(ansi);
				string ascii_conv = BitConverter.ToString(ascii);
				string utf8_conv = BitConverter.ToString(utf8);
				//

				//
				ansi_conv = ansi_conv.Replace("-", string.Empty);
				ascii_conv = ascii_conv.Replace("-", string.Empty);
				utf8_conv = utf8_conv.Replace("-", string.Empty);
				//

				//
				PrintHexDat("STRING to HEX (ANSI)", CombineTextAsHex(ansi_conv));
				PrintHexDat("STRING to HEX (ASCII)", CombineTextAsHex(ascii_conv));
				PrintHexDat("STRING to HEX (UTF8)", CombineTextAsHex(utf8_conv));
				//

				return;
			}
			else if (conversionmode == 3 && togglemode)
			{
				string convertedstr = Consolestring.Replace(" ", string.Empty);

				//
				byte[] ansi = new byte[convertedstr.Length / 2];
				byte[] ascii = new byte[convertedstr.Length];
				byte[] utf8 = new byte[convertedstr.Length / 2];
				//

				//
				try
				{
					for (int i = 0; i < ansi.Length; i++)
					{
						ansi[i] = Convert.ToByte(convertedstr.Substring(i * 2, 2), 16);
					}
					conved_ansi = true;
				}
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Conversion of HEX \"{Consolestring}\" to STRING (ANSI) Failed\nThe reason might be that the given HEX isnt a valid HEX number\n");
					Console.ResetColor();
				}
				try
				{
					for (int i = 0; i < ascii.Length; i += 2)
					{
						ascii[i] = Convert.ToByte(convertedstr.Substring(i, 2), 16);
					}
					conved_ascii = true;
				}
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Conversion of HEX \"{Consolestring}\" to STRING (ASCII) Failed\nThe reason might be that the given HEX isnt a valid HEX number\n");
					Console.ResetColor();
				}
				try
				{
					for (int i = 0; i < utf8.Length; i++)
					{
						utf8[i] = Convert.ToByte(convertedstr.Substring(i * 2, 2), 16);
					}
					conved_utf8 = true;
				}
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Conversion of HEX \"{Consolestring}\" to STRING (UTF8) Failed\nThe reason might be that the given HEX isnt a valid HEX number\n");
					Console.ResetColor();
				}
				//

				//
				string ansi_conv = Encoding.Default.GetString(ansi);
				string ascii_conv = Encoding.ASCII.GetString(ascii);
				string utf8_conv = Encoding.UTF8.GetString(ansi);
				//

				//
				if (conved_ansi)
				{
					PrintHexDat("HEX to STRING (ANSI)", CombineTextAsHex(ansi_conv));
				}
				if (conved_ascii)
				{
					PrintHexDat("HEX to STRING (ASCII)", CombineTextAsHex(ascii_conv));
				}
				if (conved_utf8)
				{
					PrintHexDat("HEX to STRING (UTF8)", CombineTextAsHex(utf8_conv));
				}
				//
				return;
			}
		}
	}
}
