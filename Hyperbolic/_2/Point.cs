using System;

namespace Metria.Hyperbolic._2
{
    /// <summary>
    /// This class represents a point in the H² upper half plane of Poincaret
    /// </summary>
	public class Point
    {
        #region Attributes

        private double _x, _y;

        /// <summary>
        /// X coordinate
        /// </summary>
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        /// <summary>
        /// y coordinate
        /// </summary>
		public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        #endregion
        #region Constructors

        /// <summary>
        /// Creates a point in the orign
        /// </summary>
        public Point()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Creates a point in the position (x,y)
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate, must be non negative or infinity. See conventions.md</param>
        public Point(double x, double y)
        {
            X = x;
            if (y < 0)
            {
                throw new Exception("Invalid point, y coordinate must be non negative, got "+y.ToString()+" instead");
            }
            Y = y;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Returns the euclidian distance between two hyperbolic points
        /// This method does not modify its arguments by any means
        /// </summary>
        /// <param name="P">Point P wich you want to get the distance</param>
        /// <returns>The euclidian distance between this and P</returns>
        public double EuclidianDistance(Point P)
        {
            return Math.Sqrt(Math.Pow(this.X - P.X, 2) + Math.Pow(this.Y - P.Y, 2));
        }

        /// <summary>
        /// Returns the euclidian distance between two hyperbolic points powered by two
        /// This method is good when you need to make comparisons and not the exact value, see tests for more
        /// This method does not modify its arguments by any means
        /// </summary>
        /// <param name="P">Point P wich you want to get the distance</param>
        /// <returns>The euclidian distance powered by two between this and P</returns>
        public double EuclidianPoweredDistance(Point P)
        {
            return Math.Pow(this.X - P.X, 2) + Math.Pow(this.Y - P.Y, 2);
        }

        public Euclidian._2.Point ToEuclidianPoint() => new Euclidian._2.Point((float)X, (float)Y);

        #endregion
        #region Operation
        public override bool Equals(object obj)
        {
            return this == obj as Point;
        }

        public static bool operator ==(Point P, Point Q)
        {
            if (object.ReferenceEquals(Q, null))

                return object.ReferenceEquals(P, null);

            if (object.ReferenceEquals(P, null))

                return object.ReferenceEquals(Q, null);

            return Math.Abs(Q.X - P.X) < Base.Epslon && Math.Abs(Q.Y - P.Y) < Base.Epslon;
        }

        public static bool operator !=(Point P, Point Q)
        {
            return !(P == Q);
        }

        public override string ToString()
        {
            return "("+X+", "+Y+")";
        }

        public override int GetHashCode() => this.ToString().GetHashCode();
        #endregion
        #region IClonable
        /// <summary>
        /// Returns a clone of the point
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Point(this.X, this.Y);
        }

        #endregion
    }
}
