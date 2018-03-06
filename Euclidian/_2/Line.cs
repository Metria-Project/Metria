using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metria.Euclidian._2
{
	public class Line
	{
	#region Variables
		private Point _origin;
		public Point Origin
		{
			get
			{
				return _origin;
			}
			set
			{
				_origin = new Point(value);
				calcCoefficients();
			}
		}

		private Vector _director;
		public Vector Director
		{
			get
			{
				return _director;
			}
			set
			{
				_director = new Vector(value);
				calcCoefficients();

			}
		}
		//The following variables are referent to the line equation coefficients
		protected float _a, _b, _c;
	#endregion
	#region Constructor

		public Line(Point P, Vector V)
		{
			_origin = new Point(P);
			Director = new Vector(V);
		}

		public Line(Point P, Point Q)
		{
			_origin = new Point(P);
			Director = new Vector(P,Q);
		}

		public Line(Line L)
		{
			_origin = new Point(L.Origin);
			Director = new Vector(L.Director);
		}

	#endregion
	#region Methods
		public virtual Point IntersectionPoint (Line L)
		{
			if(_a*L._b-L._a*_b == 0) return null;
			float Y = (L._a*_c-_a*L._c)/(_a*L._b - L._a * _b); 
			float X;
			if(_a != 0 )
				X = -(_b*Y+_c)/_a;
			else
				X = -(L._b * Y + L._c) / L._a;
            return new Point(X, Y);
            //Vector OP = new Vector(Origin, Impreciso);
            //float lambida = (Director * OP) / (Director * Director);
            //return Origin +( lambida * Director);
        }
		/*
        public float L_IntersectionPoint(Line L, ref bool paralela )
        {
            paralela = false;
            if (_a * L._b - L._a * _b == 0)
            {
                paralela = true;
                return 0;
            }
            float Y = (L._a * _c - _a * L._c) / (_a * L._b - L._a * _b);
            float X;
            if (_a != 0)
                X = -(_b * Y + _c) / _a;
            else
                X = -(L._b * Y + L._c) / L._a;
            //Point Impreciso = new Point(X, Y);
            //Vector OP = new Vector(Origin, Impreciso);
            return (X!=0)?X/Director.X:Y/Director.Y;
        }
		/**/

		public float L_IntersectionPoint(Line L, ref bool paralela)
		{
			
			paralela = true;
			float divisorMagico = L.Director.X*Director.Y-L.Director.Y*Director.X;
			if(divisorMagico == 0)
			return 0;
			paralela = false;
			return ((Origin.X*L.Director.Y)-(Origin.Y*L.Director.X)-(L.Origin.X*L.Director.Y)+(L.Origin.Y*L.Director.X))/divisorMagico;
		
		}

        protected void calcCoefficients ()
		{
			_a = -Director.Y;
			_b = Director.X;
			_c = -(_a * Origin.X + _b * Origin.Y);
		}

		public virtual Line[] Split(Line L, bool Nullable)
		{
			//Nullable only exist to force it to childrens
			Point P = IntersectionPoint(L);
			if(P==null) return null;
			Ray [] rays = new Ray[2];
			rays[0] = new Ray(P,Director);
			rays[1] = new Ray(P,-1*Director);
			return rays;
		}

        public virtual Line Cut(Line L, Point Reference)
        {
            Point P = IntersectionPoint(L);
            if(P==null) return null;
            bool IsRefAtLeft = new Metria.Euclidian._2.Vector(P,Reference)*L.Director.Normal>0;
            //Console.WriteLine("" + new Metria.Euclidian._2.Vector(P, Reference) * L.Director.Normal);
            bool IsDirAtLeft = Director * L.Director.Normal > 0;
            if (IsDirAtLeft == IsRefAtLeft) 
                return new Ray(P,Director);
            if ((new Metria.Euclidian._2.Vector(P, Reference) * L.Director.Normal) * (Director * L.Director.Normal)==0)
                return new Line(this);
            return new Ray(P, -1*Director);
        }
        

		public virtual bool IsInside(Point P)
		{
			return P.X*_a + P.Y*_b + _c == 0;
		}

	#endregion
	}
}
