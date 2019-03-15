using System;

namespace Conway {
	struct Cell : IEquatable<Cell> {
		private uint _X;
		private uint _Y;
		
		private bool _Alive;

		private uint _Neighbours;
		private static Random random = new Random();

		private static int _Created = 0;
		private int _Id;
		private int _Generation;

		// Properties

		public uint X
		{
			get { return _X; }
		}
		public uint Y
		{
			get { return _Y; }
		}
		public bool Alive {
			get { return _Alive; }
		}
		public uint Neighbours {
			get { return _Neighbours; }
			set { this._Neighbours = value; }
		}

		// Public methods

		public Cell(uint X, uint Y, int percentage = 30)
		{
			this._X = X;
			this._Y = Y;
			this._Alive = (random.Next(100) < percentage);
			this._Id = _Created;
			_Created++;
			this._Generation = 0;
			this._Neighbours = 0;
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
			this._Generation++;
		}

		// Public overrides

		public override string ToString()
		{
			if(this._Alive)
			{
				return "#";
			}
			return " ";
		}

		// override object.Equals
		public override bool Equals(object obj)
		{
			//
			// See the full list of guidelines at
			//   http://go.microsoft.com/fwlink/?LinkID=85237
			// and also the guidance for operator== at
			//   http://go.microsoft.com/fwlink/?LinkId=85238
			//
			
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			if(obj is Cell)
			{
				return this.Equals((Cell)obj);
			}
			return false;
		}

		public bool Equals(Cell other)
		{
			return (this._Id == other._Id && this._Alive == other._Alive /* && this._Generation == other._Generation */ );
		}

		public static bool operator ==(Cell c1, Cell c2)
		{
			return c1.Equals(c2);
		}

		public static bool operator !=(Cell c1, Cell c2)
		{
			return !c1.Equals(c2);
		}
		
		// override object.GetHashCode
		public override int GetHashCode()
		{
			return this._Id ^ this._Generation;
		}
	}
}