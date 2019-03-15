using System;

namespace Conway
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            Console.Write(grid.ToString());
            for(int x = 0; x < 20; ++x)
            {
                grid.Evolve();
                Console.Write(grid.ToString());
            }
        }
    }
}
