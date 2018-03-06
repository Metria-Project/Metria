using System;
using System.Collections.Generic;
using System.Text;

namespace Metria.Hyperbolic._2
{
    public class Ray : Line
    {

        public Ray (Point P, Point Q) : base(P,Q)
        {
            _a = P.Clone() as Point;
            _b = Q.Clone() as Point;
        }

        public new object Clone() => new Ray(A, B);

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
                return retorno && (B.Y < A.Y == p.Y <= A.Y);
            return retorno && (B.X < A.X == p.X <= A.X);
        }

        public new Line Cut(Line cut_line, Point reference)
        {

            if (cut_line.Belongs(reference))
            {
                throw new Exception(message: "Não é possivel realizar um corte com o ponto de referencia pertencendo a reta de corte");
            }
            Point intersection = this.IntersectionPoint(cut_line);

            if (intersection == null)
            {
                if(cut_line.IsInTheSameSide(B,reference))
                {
                    return this.Clone() as Ray;
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

            if (Radius != 0)
            {
                return new Ray(intersection, Beta);
            }
            return new Ray(intersection, new Point(intersection.X, (A.Y < B.Y) ? intersection.Y * 100 : intersection.Y / 2));
        }

        public new Ray Zoom(int i) => new Ray(new Point(_a.X * i, _a.Y * i), new Point(_b.X * i, _b.Y * i));

    }
}
