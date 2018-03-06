using System;
using System.Collections.Generic;
using System.Text;

namespace Metria.Hyperbolic._2.Voronoi
{
    public class VoronoiCell
    {
        #region Properties
        private List<VoronoiLine> _sides;

        public List<VoronoiLine> Sides
        {
            get => _sides;
        }

        public Point Center { get => _center.Clone() as Point;}

        private Point _center;

        #endregion
        #region Constructors

        public VoronoiCell(Point center)
        {
            _center = center.Clone() as Point ?? throw new ArgumentNullException(nameof(center));
            _sides = new List<VoronoiLine>();
        }

        #endregion
        #region Methods

        public bool IsInside(Point p)
        {
            bool inside = true;
            for (int i = 0; i < _sides.Count && inside; i++)
            {
                inside = _sides[i].IsInTheSameSide(p, Center);
            }
            return inside;
        }

        public void Split(Line cut)
        {
            //Begining of sanity check
            if (cut.Belongs(Center)) throw new Exception("The center of the cell belongs to the cuting line");
            bool _alfadentro = IsInside(cut.Alfa);

            //Pegando os pontos
            List<Point> buf = new List<Point>();
            //Teste de uso da lupa
            {
                for (int lupa = 1; lupa < 5 && buf.Count >2; lupa++)
                {
                    buf = new List<Point>();
                    for (int i = _sides.Count - 1; i >= 0; i--)
                    {
                        Point p = _sides[i].Zoom(lupa).IntersectionPoint(cut.Zoom(lupa));
                        if (p != null)
                        {
                            if(!buf.Contains(p))
                            buf.Add(new Point(p.X/lupa,p.Y/lupa));
                        }
                    }
                }
                //Teste de sanidade
                for (int j = 0; j < buf.Count && buf.Count >2 ; j++)
                {
                    for (int i = buf.Count; i >= 0; i++)
                    {
                        if (buf[j].EuclidianDistance(buf[i]) <= Base.Epslon) buf.RemoveAt(i);       
                    }
                }

                if (buf.Count > 2) throw new Exception("To dark for me");
            }
            //Cut those fellows up
            Line temp;

            for (int i = _sides.Count-1; i>=0; i--)
            {
                temp = _sides[i].Cut(cut, Center);
                if (temp == null) _sides.RemoveAt(i);
                else if (temp != _sides[i]) _sides[i] = new VoronoiLine(temp.Clone() as Line);
            }

            //Colocando a nova reta

            switch(buf.Count)
            {
                case 0:
                    _sides.Add(new VoronoiLine(cut.Clone() as Line));
                    break;
                case 1:
                    if (cut.Radius != 0)
                    {
                        if (_alfadentro)
                            _sides.Add(new VoronoiLine(new Ray(buf[0], cut.Alfa)));
                        else
                            _sides.Add(new VoronoiLine(new Ray(buf[0], cut.Beta)));
                    }
                    else
                    {
                        if (_alfadentro)
                            _sides.Add(new VoronoiLine(new Ray(buf[0], cut.Alfa)));
                        else
                            _sides.Add(new VoronoiLine(new Ray(buf[0], new Point(buf[0].X, buf[0].Y+1000))));
                    }
                    break;
                case 2:
                    _sides.Add(new VoronoiLine(new LineSegment(buf[0], buf[1])));
                    break;
                default:
                    throw new Exception("Some cool message here");
            }


        }

        #endregion
    }
}
