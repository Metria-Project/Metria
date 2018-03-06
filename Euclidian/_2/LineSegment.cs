using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metria.Euclidian._2
{
	public class LineSegment : Line
	{
	#region Variables

		public Point B
		{
			get
			{
				return Origin + Director;
			}
			set
			{
				Director = new Vector(Origin, value);
				calcCoefficients();
			}
		}

	#endregion
	#region Constructor

		public LineSegment (Point A, Point B) : base(A, new Vector(A,B)) {  }

		public LineSegment(Point A, Vector V) : base(A, V) { }

		public LineSegment(LineSegment L) : base(L) { }

		public LineSegment(Line L) : base(L) { }
	#endregion
	#region Methods

		/*public new Point IntersectionPoint (Line L)
		{
			Point Intersection = base.IntersectionPoint(L);
			if (Intersection == null) return null;
			float t = (Director.X!=0)?(Intersection-Origin).X/Director.X:(Intersection-Origin).X/Director.Y;
			if(t>=0 && t<=1) return Intersection;
			return null;
		}*/

        public override Point IntersectionPoint(Line L)
        {
            bool paralela = false;
            float returnedLambida = L_IntersectionPoint(L, ref paralela);
            if (paralela) return null;
            if (returnedLambida < 0 || returnedLambida > 1) return null;
            return Origin + (returnedLambida * Director);
        }

        public override Line[] Split(Line L,bool Nullable)
		{
			Point P = IntersectionPoint(L);
			LineSegment[] lines;
			if (P == null) return null;
			if(P != Origin && P != B)
			{ 
				lines = new LineSegment[2];
				lines[0] = new LineSegment(Origin, P);
				lines[1] = new LineSegment(P, B);
                return lines;
			}
			if(Nullable) return null;
			lines = new LineSegment[1];
			lines[0] = this;
			return lines;

		}

        public override Line Cut (Line L, Point reference)
        {
            Point P = IntersectionPoint(L);
            if(P==null) return null;
            bool IsRefAtLeft = new Vector(P, reference) * L.Director.Normal > 0;
            bool IsOriginAtLeft = L.Director.Normal * new Vector(P,Origin)  > 0;
            if (IsRefAtLeft == IsOriginAtLeft)
                return new LineSegment(P,Origin);
            if ((new Vector(P, reference) * L.Director.Normal) * (L.Director.Normal * new Vector(P, Origin)) == 0)
                return this;
            return new LineSegment(P, B);
        }




	#endregion
	}
}
