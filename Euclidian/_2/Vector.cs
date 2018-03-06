using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metria.Euclidian._2
{
	public class Vector : Point
	{
	#region Variables
		public double Norm
		{
			get
			{
				return Distance(new Point());
			}
		}

		public double NormPowered
		{
			get
			{
				return PoweredDistance(new Point());
			}
		}

		public Vector Normal
		{
			get
			{
				return new Vector(new Point(-Y,X));
			}
		}
	#endregion
	#region Constructors
		public Vector()
		{
			X = 1;
			Y = 1;
		}

		public Vector(Point P) : base(P) { }

		public Vector(Point P, Point Q) : base(Q.X-P.X,Q.Y-P.Y) { }

		public Vector(float x, float y) : base(x, y) {  }

		public Vector(Vector V) : base(V) { }
	#endregion
	#region Operators

		public static float operator * (Vector V, Vector W)
		{
			return V.X*W.X+V.Y*W.Y;
		}

		public static Vector operator * (float F, Vector V)
		{
			return new Vector(new Point(V.X*F,V.Y*F));
		}

		public static Vector operator *(Vector V,float F)
		{
			return F*V;
		}

		public static Vector operator - (Vector V, Vector W)
		{
			return new Vector(new Point(V.X-W.X,V.Y-W.Y));
		}

		//public static operator=(Vector V,)

	#endregion
	}
}
