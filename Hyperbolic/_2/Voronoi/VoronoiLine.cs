namespace Metria.Hyperbolic._2.Voronoi
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

        protected Line _line;
        
        public Point A
        {
            get
            {
                return _line.A;
            }

        }

        public Point B
        {
            get
            {
                return _line.B;
            }

        }

        public double Radius
        {
            get
            {
                return _line.Radius;
            }
        }

        #endregion
        #region Constructors
        public VoronoiLine(Line L)
        {
            IndexNeigbor = -1;
            Neigbor = null;
        }
        public VoronoiLine(Line L, VoronoiCell neigbor)
        {
            IndexNeigbor = -1;
            Neigbor = neigbor;

        }
        public VoronoiLine(Line L, int indexNeigbor)
        {
            IndexNeigbor = indexNeigbor;
            Neigbor = null;
        }
        #region suporte


        #endregion

        #endregion
        #region methods

        public bool Belongs(Point p)
        {
            return _line.Belongs(p);
        }

        public Point IntersectionPoint(Line L)
        {
            return _line.IntersectionPoint(L);
        }

        public Line Cut(Line L, Point Referencia)
        {
            return _line.Cut(L, Referencia);
        }

        public bool IsInTheSameSide(Point p, Point reference)
        {
            return _line.IsInTheSameSide(p, reference);
        }

        public Line Zoom(int i) => _line.Zoom(i);


        #endregion
        #region OVERLOAD

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, null))
                return object.ReferenceEquals(obj, null);
            if (object.ReferenceEquals(obj, null))
                return false;
            if (obj is VoronoiLine)
                return _line == (obj as VoronoiLine)._line;
            if (obj is Line)
                return _line == obj as Line;
            return false;
        }

        public override string ToString() => "voronoi line A " + A.ToString() + " B " + B.ToString();

        public override int GetHashCode() => this.ToString().GetHashCode();

        public static bool operator ==(Line l, VoronoiLine v) => l.Equals(v);

        public static bool operator ==(VoronoiLine v, Line l) => l.Equals(v);

        public static bool operator !=(VoronoiLine v, Line l) => !l.Equals(v);

        public static bool operator !=(Line l, VoronoiLine v) => !l.Equals(v);

        #endregion
    }
}