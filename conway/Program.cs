using System;
using System.Threading;

namespace Conway
{
	class Program
	{
		static void Main(string[] args)
		{
			Grid grid = new Grid(20);
			Grid oldGrid;
			//string str;
			Console.Clear();
			Console.Write(grid.ToString());
			//Console.Write("Press ENTER to start ...");
			//str = Console.ReadLine();
			do
			{
				oldGrid = grid.DeepClone();
				grid.Evolve();
				Console.Clear();
				Console.Write(grid.ToString());
				//Console.Write("Press ENTER to continue ...");
				//str = Console.ReadLine();
				Thread.Sleep(30);
			} while(oldGrid != grid);
			Console.WriteLine("DONE");
		}
	}
}
