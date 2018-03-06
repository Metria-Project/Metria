using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metria.Euclidian._2.Voronoi
{
	public class VoronoiCell : ConvexPoligon
	{
	#region Variables

		protected Point _center;

		public Point Center
		{
			get
			{
				return _center;
			}
		}

		private new List<VoronoiLine> _sides;

		public new List<VoronoiLine> Sides
		{
			get
			{
				return _sides;
			}
			set
			{
				_sides = value;
			}
		}
	#endregion
	#region Constructors
		//must have to make lists
		public VoronoiCell() : base() 
		{
			_sides = new List<VoronoiLine>();
		}

		public VoronoiCell( Point center) : base ()
		{
			_center = center;
			_sides = new List<VoronoiLine>();
		}

		public VoronoiCell( Point center, List<VoronoiLine> sides)
		{
			_sides = sides;
			_center = center;
		}

		public VoronoiCell(VoronoiCell C) : base()
		{
			_sides = new List<VoronoiLine>(C._sides);
			_center = new Point(C._center);
		}
	#endregion
	#region Methods

		public override bool IsInside (Point P)
		{
			bool Inside = true; //Variable used to return the value
			for(int i = 0; i < _sides.Count; i++)
			{
				float DP = _sides[i].Director.Normal * new Vector(_sides[i].Origin, P);// Dot product of Direction with Origin->P 
				float DC = _sides[i].Director.Normal * new Vector(_sides[i].Origin, Center);// Dot product of Direction with Origin->Center

				if((DC * DP < 0)) //if arent both in the same side
				{
					Inside = false;
					break; //Break the loop
				}
			}
			return Inside;
		}
		
        public bool IsInsideSpecial(Point P)
        {
            return IsInside(P) && P != Center;
        }

		/// <summary>
		/// Cut the voronoi cells in two, and discard the one that does not have the center of the cell included
		/// </summary>
		/// <param name="L"></param>
		/// <returns></returns>
		public List<int> CutPoligon(Line L, ref bool haveCut) //TODO Impelementar uma solução mais elegante para havecut
		{
			List<int> cutIndex = new List<int>();//next poligons to be cutted
			//bool isCenterAtLeft = (L.Director * (L.Origin - Center) < 0);//gets in wich side of L is center
            Point intersection = null;
			List<Point> intersections = new List<Point>();
			for (int i = Sides.Count-1; i >= 0; i--)
			{
				intersection = Sides[i].IntersectionPoint(L);
                if (intersection != null)//if there is a intersection wih the side[i]
                {
                    intersections.Add(intersection);
                    cutIndex.Add(Sides[i].IndexNeigbor);

                    Line cut = Sides[i].Cut(L, Center);
                    if (cut != null) 
                        Sides.Add(new VoronoiLine(cut, Sides[i].Neigbor, Sides[i].IndexNeigbor));
                    Sides.RemoveAt(i);
                }
                else if((new Vector(L.Origin, Center) * L.Director.Normal)*(new Vector(L.Origin,Sides[i].Origin)*L.Director.Normal)<0)
                {
                    Sides.RemoveAt(i);
                }
			}
			
			//Add the new side
			//Todo: Refactorate
            for (int i = intersections.Count-1; i >= 0; i--)
            {
                for (int j = intersections.Count-1; i < j; j--)
                {
                    if (i != j && intersections[i] == intersections[j])
                        intersections.RemoveAt(j);
                }
            }

			Line NewSide = null;
			bool gambiarra = true;
			switch(intersections.Count)
			{
				case 0:
					NewSide = L;
					break;
				case 1:
					if (IsInsideSpecial(intersections[0] + L.Director))	NewSide = new Ray(intersections[0], L.Director);
					else if (IsInsideSpecial(intersections[0] + (-1f * L.Director))) NewSide = new Ray(intersections[0], -1f * L.Director);
					else gambiarra = false;
					
					break;
				case 2:
					NewSide = new LineSegment(intersections[0],intersections[1]);
					break;
				default:
					throw new NotImplementedException();
			}
			haveCut=true;
			if(gambiarra) Sides.Add( new VoronoiLine(NewSide));
			else haveCut = false;
			return cutIndex;
       }
	#endregion
	}
}