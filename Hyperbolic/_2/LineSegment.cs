using System;
using System.Collections.Generic;
using System.Text;

namespace Metria.Hyperbolic._2
{
    public class LineSegment : Line
    {

        public LineSegment(Point P, Point Q) : base(P,Q)
        {
            if (P.Y == 0 || Q.Y == 0)
                throw new Exception("Não é possivel criar um segmento com uma ou duas bordas no infinito");
            if(P.X==Q.X)
            {
                if(P.Y<Q.Y)
                {
                    this._a = P.Clone() as Point;
                    this._b = Q.Clone() as Point;
                }
                else
                {
                    this._b = P.Clone() as Point;
                    this._a = Q.Clone() as Point;
                }
            }
            else
            {
                if(P.X>Q.X)
                {
                    this._b = P.Clone() as Point;
                    this._a = Q.Clone() as Point;
                }
                else
                {
                    this._a = P.Clone() as Point;
                    this._b = Q.Clone() as Point;

                }
            }
        }

        public new object Clone() => new LineSegment(A, B);

        public new Point IntersectionPoint(Line l)
        {
            Point retorno = base.IntersectionPoint(l);
            if (retorno == null) return null;
            if (Belongs(retorno))
                return retorno;
            return null;

        }

        public new bool Belongs(Point p)
        {
            bool retorno = base.Belongs(p);
            //vertical case
            if (Radius == 0)
                return retorno && (B.Y >= p.Y == p.Y >= A.Y);
            return retorno && (B.X <= A.X == p.X <= A.X);
        }

        public new Line Cut(Line cut_line, Point reference)
        {
            if (cut_line.Belongs(reference))
            {
                throw new Exception(message: "Não é possivel realizar um corte com o ponto de referencia pertencendo a reta de corte");
            }
            Point intersection = this.IntersectionPoint(cut_line);

            if(intersection == A)
            {
                if(IsInTheSameSide(B,reference))
                {
                    return this.Clone() as LineSegment;
                }
                return null;
            }

            if (intersection == B)
            {
                if (IsInTheSameSide(A, reference))
                {
                    return this.Clone() as LineSegment;
                }
                return null;
            }

            if (intersection == null)
            {
                if (cut_line.IsInTheSameSide(B, reference))
                {
                    return this.Clone() as LineSegment;
                }
                else
                {
                    return null;
                }
            }

            if (cut_line.IsInTheSameSide(A, reference))
            {
                return new LineSegment(A, intersection);
            }
            return new LineSegment(B, intersection);
        }

        public new LineSegment Zoom(int i) => new LineSegment(new Point(_a.X * i, _a.Y * i), new Point(_b.X * i, _b.Y * i));

    }
}
