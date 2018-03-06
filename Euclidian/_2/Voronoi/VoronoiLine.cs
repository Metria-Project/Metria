using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metria.Euclidian._2.Voronoi
{
	public class VoronoiLine
	{
	#region Variables

		protected VoronoiCell _neigbor;
		public VoronoiCell Neigbor
		{
			get
			{
				return _neigbor;
			}
			set
			{
				_neigbor = value;
			}
		}
		protected int _indexNeigbor;
		public int IndexNeigbor
		{
			get
			{
				return _indexNeigbor;
			}
			set
			{
				_indexNeigbor = value;
			}
		}

        public Line _line;

        public Point Origin
        {
            get
            {
                return _line.Origin;
            }
            set
            {
                _line.Origin = value;
            }
        }

        public Vector Director
        {
            get
            {
                return _line.Director;
            }
            set
            {
                _line.Director = value;
            }
        }

	#endregion
	#region Constructors
		public VoronoiLine(Line L)
		{
			IndexNeigbor = -1;
			Neigbor = null;
			_line = ParseLine(L);
		}
		public VoronoiLine(Line L, VoronoiCell neigbor)
		{
			IndexNeigbor = -1;
			Neigbor = neigbor;
			_line = ParseLine(L);
           
		}
		public VoronoiLine(Line L, int indexNeigbor)
		{
			IndexNeigbor = indexNeigbor;
			Neigbor = null;
			_line = ParseLine(L);
		}
		public VoronoiLine(Line L, VoronoiCell neigbor, int indexNeigbor)
		{
			IndexNeigbor = indexNeigbor;
			Neigbor = neigbor;
			_line = ParseLine(L);
		} 
		#region suporte

		private Line ParseLine(Line L)
		{
			if (L is Ray)  return new Ray(L);
			else if (L is LineSegment) return new LineSegment(L);
			else return new Line(L);
		}

		#endregion

	#endregion
    #region methods

       	public Point IntersectionPoint (Line L)
        {
            return _line.IntersectionPoint(L);
        }

        public float L_IntersectionPoint(Line L, ref bool paralela )
        {
            return _line.L_IntersectionPoint(L,ref paralela);
        }

        public Line[] Split(Line L)
        {
            return _line.Split(L,true);
        }

        public Line Cut (Line L, Point Referencia)
        {
            return _line.Cut(L, Referencia);
        }

        public bool IsInside(Point P)
        {
            return _line.IsInside(P);
        }
    #endregion
    }
}
