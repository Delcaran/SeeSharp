using System;
using System.Linq;

namespace Conway
{
	public interface IDeepCloneable
	{
		object DeepClone();
	}
	public interface IDeepCloneable<T> : IDeepCloneable
	{
		T DeepClone();
	}

	class Grid : IDeepCloneable<Grid>
	{
		private uint _Size;
		private bool _ConnectedEastWest;
		private bool _ConnectedNorthSouth;
		private Cell[][] _Population;
		
		public Grid(uint size = 4, bool eastWest = false, bool northSouth = false, int percentage = 30)
		{
			this._Size = size;
			this._ConnectedEastWest = eastWest;
			this._ConnectedNorthSouth = northSouth;
			this._Population = Enumerable.Range(0, Convert.ToInt32(this._Size)).Select(
				(x) => Enumerable.Range(0, Convert.ToInt32(this._Size)).Select(
					(y) => new Cell(Convert.ToUInt32(x), Convert.ToUInt32(y), percentage)
				).ToArray()
			).ToArray();
			for(int x = 0; x < this._Size; x++)
			{
				for(int y = 0; y < this._Size; y++)
				{
					this._Population[x][y].Neighbours = this._CountAliveNeighbours(this._GetCell(x, y));
				}
			}
		}

		private Cell _GetCell(int x, int y) {
			if(x < 0) {
				if(this._ConnectedEastWest) {
					x = Convert.ToInt32(this._Size - 1);
				}
				else
				{
					x = 0;
				}
			}
			if(x >= this._Size) {
				if(this._ConnectedEastWest) {
					x = 0;
				}
				else
				{
					x = Convert.ToInt32(this._Size - 1);
				}
			}
			if(y < 0) {
				if(this._ConnectedNorthSouth) {
					y = Convert.ToInt32(this._Size - 1);
				}
				else
				{
					y = 0;
				}
			}
			if(y >= this._Size) {
				if(this._ConnectedNorthSouth) {
					y = 0;
				}
				else
				{
					y = Convert.ToInt32(this._Size - 1);
				}
			}
			return this._Population[x][y];
		}

		private uint _CountAliveNeighbours(Cell c)
		{
			uint alive = 0;
			System.Collections.Generic.HashSet<Cell> parsed = new System.Collections.Generic.HashSet<Cell>();
			parsed.Add(c);
			for(int dx = -1; dx <= 1; dx++)
			{
				for(int dy = -1; dy <= 1; dy++)
				{
					Cell parsing = this._GetCell(Convert.ToInt32(c.X + dx), Convert.ToInt32(c.Y + dy));
					if(parsed.Add(parsing) && parsing.Alive)
					{
						alive++;
					}
				}
			}
			return alive;
		}

		public Grid DeepClone()
		{
			Grid current = new Grid(this._Size, this._ConnectedEastWest, this._ConnectedNorthSouth);
			current._Population = new Cell[this._Size][];
			for (int i = 0; i < this._Size; i++)
			{
				current._Population[i] = (Cell[]) this._Population[i].Clone();
			}
			return current;
		}
		object IDeepCloneable.DeepClone()
		{
			return this.DeepClone();
		}

		public void Evolve()
		{
			// Salvo la popolazione attuale
			Grid current = this.DeepClone();
			
			for(int x = 0; x < this._Size; x++)
			{
				for(int y = 0; y < this._Size; y++)
				{
					this._Population[x][y].Evolve();
				}
			}
			for(int x = 0; x < this._Size; x++)
			{
				for(int y = 0; y < this._Size; y++)
				{
					this._Population[x][y].Neighbours = this._CountAliveNeighbours(this._GetCell(x, y));
				}
			}
		}

		public override string ToString()
		{
			string output = "";
			string output_num = "";
			for(int x = 0; x < this._Size; x++)
			{
				for(int y = 0; y < this._Size; y++)
				{
					output += this._Population[x][y].ToString();
					output_num += Convert.ToString(this._Population[x][y].Neighbours);
				}
				output += "\n";
				output_num += "\n";
			}
			output += "--------------\n" + output_num+"======================================\n";
			return output;
		}
	}
}