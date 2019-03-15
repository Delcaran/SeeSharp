using System;

namespace Conway {
	class Cell {
		private uint _X;
		public uint X
		{
			get { return _X; }
		}
		private uint _Y;
		public uint Y
		{
			get { return _Y; }
		}
		private bool _Alive;
		public bool Alive {
			get { return _Alive; }
		}

		private uint _Neighbours;
		public uint Neighbours {
			get { return _Neighbours; }
			set { this._Neighbours = value; }
		}
		static Random random = new Random();

		public Cell(uint X, uint Y, int percentage = 30)
		{
			this._X = X;
			this._Y = Y;
			this._Alive = (random.Next(100) < percentage);
		}

		public void Evolve()
		{
			if(this._Alive)
			{
				if (this._Neighbours < 2 || this._Neighbours > 3)
				{
					this._Alive = false;
				}
			}
			else
			{
				if(this._Neighbours == 3) {
					this._Alive = true;
				}
			}
		}

		public override string ToString()
		{
			if(this._Alive)
			{
				return "#";
			}
			return " ";
		}
	}
}