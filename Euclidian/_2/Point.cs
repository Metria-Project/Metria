using System;

namespace Metria.Euclidian._2
{
    public class Point : Base
	{
	#region Attributes

		protected float [] _coordenates;

		protected String _id;

		public float X
		{
			get
			{
				return _coordenates[0];
			}
			set
			{
				_coordenates[0] = value;
			}
		}

		public float Y
	{
		get
		{
			return _coordenates[1];
		}
		set
		{
			_coordenates[1] = value;
		}
	}

	#endregion
	#region Constructors

		/// <summary>
		/// Creates a point in the orign
		/// </summary>
		public Point()
		{
			_coordenates = new float [dimension];

			X = 0;
			Y = 0;

		}

		/// <summary>
		/// Creates a point in the position (x,y)
		/// </summary>
		/// <param name="x">x coordinate</param>
		/// <param name="y">y coordinate</param>
		public Point(float x, float y)
		{
			_coordenates = new float [dimension];

			X = x;
			Y = y;
            //Console.WriteLine("Created the (" + X + ", " + Y + ") point");
		}	

		public Point(Point P)
		{
			_coordenates = new float [dimension];
			
			X = P.X;
			Y = P.Y;
            //Console.WriteLine("Created the (" + X + ", " + Y + ") point");

		}

	#endregion
	#region Methods

		/// <summary>
		/// Calculate and returns Euclidean distance from this point to a point P
		/// </summary>
		/// <param name="P">Point P</param>
		/// <returns>distance from this point to poitn P</returns>
		public double Distance (Point P)
		{
			return Math.Sqrt(Math.Pow(X-P.X,2) + Math.Pow(Y-P.Y,2));
		}
	
		/// <summary>
		/// Calculate and returns Euclidean distance powered by 2 from this point to a point P
		/// </summary>
		/// <param name="P">Point P</param>
		/// <returns>distance powered</returns>
		public double PoweredDistance(Point P)
		{
			return Math.Pow(X - P.X, 2) + Math.Pow(Y - P.Y, 2);
		}

        public Hyperbolic._2.Point ToHyperbolicPoint() => new Hyperbolic._2.Point(X, Y);
	#endregion
	#region Operators

		public static Point operator +(Point P,Vector V)
		{
			return new Point (P.X+V.X,P.Y+V.Y);
		}

        public static bool operator ==(Point T, Point P)
        {
            if (object.ReferenceEquals(T, null))
                return object.ReferenceEquals(P, null);
            if (object.ReferenceEquals(P, null))
                return object.ReferenceEquals(T, null);
            return Math.Abs(T.X - P.X) < float.Epsilon && Math.Abs(T.Y - P.Y) < float.Epsilon;
        }

        public static bool operator !=(Point T, Point P)
        {
            return !(T==P);
        }

        public override bool Equals(object obj)
        {
            return this == obj as Point;
        }

        public override string ToString()
        {
            return "X : "+X+", Y : "+Y;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion
    }
}