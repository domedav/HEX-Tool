using System;
using System.Threading;

namespace Stuffie
{
	public class Program
	{
		/// <summary>
		/// 1 = int
		/// 2 = float
		/// 3 = string
		/// </summary>
		private static void Main(string[] args)
		{
			Thread mainThread = new Thread(new ThreadStart(MainThread));
			mainThread.Start();
			HexConvert.Start();
		}
		private static void MainThread()
		{
			DateTime _nextLoop = DateTime.Now;
			while (true)
			{
				while (_nextLoop < DateTime.Now)
				{
					HexConvert.Update();
					_nextLoop = _nextLoop.AddMilliseconds(ConstantVars.ticks);
					if (_nextLoop > DateTime.Now)
					{
						Thread.Sleep(_nextLoop - DateTime.Now);
					}
				}
			}
		}
	}
}
