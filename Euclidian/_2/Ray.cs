using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metria.Euclidian._2
{
	public class Ray : Line
	{
	#region Variables

		

	#endregion
	#region Constructors

		public Ray() : base(new Point(), new Vector()) { }
		public Ray(Point P, Vector V) : base (P, V) { }
		public Ray(Point P, Point Q) : base (P, Q) { }
		public Ray(Ray R) : base(R) { }
		public Ray(Line L) : base(L) { }

	#endregion
	#region Methods

		public override Point IntersectionPoint(Line L)
		{
            bool paralela=false;
			float returnedLambida = L_IntersectionPoint(L,ref paralela);
			if(paralela) return null;
            if (returnedLambida < 0) return null;
			return new Point(Origin.X+Director.X*returnedLambida,Origin.Y+Director.Y*returnedLambida);
		}

		public override Line[] Split(Line L, bool Nullable)
		{
            //if (!IsInside(P)) return null;
            Point P = IntersectionPoint(L);
			Line[] lines;
            if (P == null) return null;
			if(P == Origin)
			{
				if(Nullable) return null;
		 		lines = new Line[1];
				lines[0] = this;
				return lines;
			}
			lines = new Line[2];
			lines[0] = new LineSegment(Origin, P);
			lines[1] = new Ray(P, Director);
			return lines;
		}

		public override bool IsInside(Point P)
		{
           return Math.Abs(P.X * _a + P.Y * _b + _c) < float.Epsilon;
        }

        public override Line Cut (Line L, Point reference)
        {
            Point P = IntersectionPoint(L);
            if (P == null) return null;
            bool IsDirAtLeft = Director * L.Director.Normal > 0;
            bool IsRefAtleft = new Metria.Euclidian._2.Vector(P, reference) * L.Director.Normal > 0;
            if (IsDirAtLeft == IsRefAtleft)
                return new Ray(P, Director);
            if ((Director * L.Director.Normal) * (new Metria.Euclidian._2.Vector(P, reference) * L.Director.Normal) == 0)
                return this;
            if(P!=Origin)
                return new LineSegment(Origin, P);
            return null;
        }

	#endregion


	}
}
