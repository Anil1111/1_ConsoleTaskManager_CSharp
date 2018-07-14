using System;
using System.Collections.Generic;
using System.Text;

namespace Warsztat_1
{
	public static class ConsoleEx
	{
		public static void WriteLine(string text, ConsoleColor x)
		{
			Console.ForegroundColor = x;
			Console.WriteLine(text);
			Console.ResetColor();
		}

		public static void Write(string text, ConsoleColor x)
		{
			Console.ForegroundColor = x;
			Console.Write(text);
			Console.ResetColor();
		}
	}
}
