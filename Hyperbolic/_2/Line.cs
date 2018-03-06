using System;
using System.Collections.Generic;
using System.Text;

namespace Metria.Hyperbolic._2
{
    public class Line
    {

        #region properties
        protected Point _a, _b, _alfa = null, _beta = null, _center = null;
        protected double _radius = 0;

        /**
         * DISCLAIMER - Why isnt A equals to Alfa and B equals to Beta?
         * Simple, because if you need to change A or B things get dirty
         * when A is Alfa and B is Beta
         **/

        /// <summary>
        /// The A point that describes the line
        /// </summary>
        public Point A
        {
            get => _a.Clone() as Point;

        }

        /// <summary>
        /// The B point that describes the line
        /// </summary>
        public Point B
        {
            get => _b.Clone() as Point;

        }

        /// <summary>
        /// The Alfa point of a line, in case of doubts read the docs 
        /// </summary>
        public Point Alfa
        {
            get => _alfa.Clone() as Point;
        }

        /// <summary>
        ///  The Beta point of a line, in case of doubts read the docs 
        /// </summary>
        public Point Beta
        {
            get => _beta.Clone() as Point;
        }

        /// <summary>
        /// The center of a line, in case of doubts read the docs
        /// </summary>
        public Point Center
        {
            get => _center.Clone() as Point;
        }

        public double Radius
        {
            get => _radius;
        }

        #endregion
        #region Constructors
        public Line(Point P, Point Q)
        {
            if (P == Q)
                throw new Exception("It is not allowed to create a line with two equal points");
            _a = P.Clone() as Point;
            _b = Q.Clone() as Point;
            _fixValues();
        }
        #endregion
        #region Methods
        private void _fixValues()
        {
            //Fist we need to check if it is a vertical line
            if (A.X == B.X)
            {
                _alfa = new Point(A.X, 0);
                _beta = new Point(A.X, 0);
                _radius = 0d;
                _center = new Point(A.X, 0);
                //Now we set the lower to A and the higher to B
                if (A.Y > B.Y)
                {
                    Point temp = A.Clone() as Point;
                    _a = _b.Clone() as Point;
                    _b = temp; //Need to check is going to free temp
                }
            }
            //And we need to treat case it is not
            else
            {
                //First we find the center;
                //Edge case where A and B are in the infinity
                if (A.X > B.X)
                {
                    Point temp = A.Clone() as Point;
                    _a = _b.Clone() as Point;
                    _b = temp; //Need to check is going to free temp
                }
                if (A.Y == 0 && B.Y == 0)
                {
                    _center = new Point((A.X + B.X) / 2, 0);
                    _radius = Math.Abs(A.X - B.X) / 2;
                }
                else
                {

                    //Sorry about the pasta code, read the docs
                    {
                        Euclidian._2.Line aux = new Euclidian._2.Line(A.ToEuclidianPoint(), B.ToEuclidianPoint());
                        Euclidian._2.Point meio = aux.Origin + (aux.Director * 0.5f);
                        _center = (new Euclidian._2.Line(meio,aux.Director.Normal)).IntersectionPoint(new Euclidian._2.Line(new Euclidian._2.Point(), new Euclidian._2.Point(1, 0))).ToHyperbolicPoint();
                    }
                   //The the radius will be the distance from te center to the point
                    _radius = Center.EuclidianDistance(A);
                }
                if (_radius == 0) throw new Exception("Sorry, the points provided are to close to make a line");
                //Now it is easy to get Alfa and Beta
                _alfa = new Point(Center.X - Radius, 0);
                _beta = new Point(Center.X + Radius, 0);
                //The last thing to do is to swap A and B if necessary
                
            }
        }

        public object Clone() => new Line(A, B);

        /// <summary>
        /// Intersect two lines and returns the intersection point
        /// </summary>
        /// <param name="l"></param>
        /// <returns>Return null if theres is no intersection</returns>
        public Point IntersectionPoint(Line l)
        {
            if (this == l)
                throw new Exception("The lines intersect in infinty points");
            
            // In this case we need to check for every case, and there are a lot
            Point p = null;

            // Case this is a vertical line
            if (Radius == 0)
            {
                //Case l is a vertiacal line
                if (l.Radius == 0)
                {
                    p = null;
                }
                //Case l is not vertical
                else
                {
                    if (this.Center.EuclidianDistance(l.Center) > l.Radius)
                        return null;
			        return new Point(this.Center.X, Math.Sqrt(l.Center.EuclidianPoweredDistance(l.A) - Math.Pow(l.Center.X - this.Center.X, 2)));
                }
            }
            else
            {
                // Case this is not vertical
                // Case l is vertical
                if (l.Radius == 0)
                {
                    if (Math.Abs(this.Center.X - l.Center.X) <= this.Radius)
                    {
                        // I'm using PoweredEuclidianDistance to get more acuracy in the results
                        return new Point(l.Center.X, Math.Sqrt(this.Center.EuclidianPoweredDistance(this.Alfa) - Math.Pow(l.Center.X - this.Center.X, 2)));
                    }
                }
                //Case both l and this are not vertical
                else
                {
                    if (Math.Abs(this.Center.X - l.Center.X) <= (this.Radius + l.Radius))
                    {
                        if (this.Center.EuclidianDistance(l.Alfa) < this.Radius != this.Center.EuclidianDistance(l.Beta) < this.Radius)
                        {
                            float X = (float)((Math.Pow(this.Center.X, 2) - Math.Pow(l.Center.X, 2) - Math.Pow(this.Radius, 2) + Math.Pow(l.Radius, 2)) / (2 * (this.Center.X - l.Center.X)));
                            float Y = (float)Math.Sqrt((this._radius * this._radius) - ((X - this._center.X) * (X - this._center.X)));
                            p = new Point(X, Y);
                        }
                        else
                        {
                            float X = (float)((Math.Pow(l.Center.X, 2) - Math.Pow(this.Center.X, 2) - Math.Pow(l.Radius, 2) + Math.Pow(this.Radius, 2)) / (2 * (l.Center.X - this.Center.X)));
                            float Y = (float)Math.Sqrt(d: (l.Radius * l.Radius) - ((X - l.Center.X) * (X - l.Center.X)));
                            p = new Point(X, Y);
                        }
                    }
                }
            }

            if (p != null && this.Belongs(p) && l.Belongs(p))
                return p;
            else
                return null;
        }

        /// <summary>
        /// Returns if a point belongs to the line
        /// </summary>
        /// <param name="P">A point p</param>
        /// <returns></returns>
        public bool Belongs(Point p)
        {
            if (p == null)
            {
                throw new Exception("Não é pssovel verificar pontos nulos");
            }
            if(Radius != 0)
                return Math.Abs(Center.EuclidianDistance(p) - Radius) < Base.Epslon;
            return Math.Abs(p.X - Center.X) < Base.Epslon;
        }

        public bool IsInTheSameSide(Point p, Point reference)
        {
            if (Belongs(p) || Belongs(reference))
                throw new Exception("Cant check sides if one of the points belongs to the line");
            //Case this is vertical
            if(Radius == 0)
                return p.X < Center.X == reference.X < Center.X;
            return Center.EuclidianDistance(p) < Radius == Center.EuclidianDistance(reference) < Radius;
        }

        public Line Cut(Line cut_line, Point reference)
        {
            if (cut_line.Belongs(reference))
            {
                throw new Exception(message: "Não é possivel realizar um corte com o ponto de referencia pertencendo a reta de corte");
            }
            Point intersection = this.IntersectionPoint(cut_line);

            if(intersection == null)
            {
                if (cut_line.IsInTheSameSide(A, reference) || cut_line.IsInTheSameSide(B,reference))
                {
                    return this.Clone() as Line;
                }
                else
                {
                    return null;
                }
            }
            if (Radius == 0)
            {
                if (cut_line.IsInTheSameSide(Alfa, reference))
                {
                    return new Ray(intersection, Alfa);
                }
                return new Ray(intersection, new Point(intersection.X, intersection.Y + 1000));
            }
            if (cut_line.IsInTheSameSide(Alfa, reference))
            {
                return new Ray(intersection,Alfa);
            }
            return new Ray(intersection, Beta);
        }

        public Line Zoom(int i) => new Line(new Point(_a.X * i, _a.Y * i), new Point(_b.X * i, _b.Y * i));
        #endregion

        public static bool operator ==(Line P, Line Q)
        {
            if (object.ReferenceEquals(Q, null))

                return object.ReferenceEquals(P, null);

            if (object.ReferenceEquals(P, null))

                return object.ReferenceEquals(Q, null);

            return P.Center == Q.Center && Math.Abs(P.Radius - Q.Radius) < Base.Epslon;
        }

        public static bool operator !=(Line P, Line Q)
        {
            return !(P == Q);
        }

        public override bool Equals(object obj)
        {
            if(obj is Line)
                return this == obj as Line;
            return false;
        }

        public override string ToString()
        {
            return Center.ToString()+" : "+Radius;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
