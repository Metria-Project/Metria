using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metria.Euclidian._2.Voronoi
{
	public class VoronoiDiagram
	{
	#region Variables

		private List<VoronoiCell> _cells;
		public List<VoronoiCell> Cells
		{
			get
			{
				return _cells;
			}
		}

	#endregion
	#region Constructors

		public VoronoiDiagram ()
		{
			_cells = new List<VoronoiCell> ();
		}

	#endregion
	#region Methods

		public bool AddPoint (Point P)
		{
			//edge cases
			if (Cells.Count == 0)
			{
				_cells.Add(new VoronoiCell(P));
				return true;
			}
			if (Cells.Count == 1)
			{
				LineSegment SupportSegment = new LineSegment(_cells[0].Center,P);
				Line cut = new Line(SupportSegment.Origin+0.5f*SupportSegment.Director,SupportSegment.Director.Normal);
				
				_cells.Add(new VoronoiCell(P));
				_cells[0].Sides.Add(new VoronoiLine(cut,_cells[1],1));
				_cells[1].Sides.Add(new VoronoiLine(cut,_cells[0],0));
				return true;
			}

			//Finds witch cell contains P
			int initialIndex;
			bool worked = false;
			for (initialIndex = 0; initialIndex < Cells.Count; initialIndex++)
			{
				if(Cells[initialIndex].IsInsideSpecial(P))
				{
                    //Console.WriteLine("Point is inside Cell numeber " + initialIndex);
					worked = true;
					break;
				}
			}
			if(!worked) return false;
			_cells.Add(new VoronoiCell(P));
			
            //Start the algorithm
			Queue<int> indexToCut = new Queue<int>(); //Queue with the index of the voroni cells to cut
			bool [] visited = new bool [Cells.Count];
            for (int i = 0;  i < Cells.Count; i++)
            {
                visited[i] = false;
            }
            visited[initialIndex] = true;
			visited[Cells.Count-1] = true;
            indexToCut.Enqueue(initialIndex);
			List<int> nextIndexes;
			while(indexToCut.Count>0)
			{

				int currentIndex = indexToCut.Dequeue();//gets the next index
				bool haveCut = true; //POG
				LineSegment SupportSegment = new LineSegment(Cells[currentIndex].Center,P);
				nextIndexes = Cells[currentIndex].CutPoligon(new Line(SupportSegment.Origin + 0.5f * SupportSegment.Director, SupportSegment.Director.Normal), ref haveCut);
				if(haveCut)
				{
					Cells[currentIndex].Sides[Cells[currentIndex].Sides.Count - 1].IndexNeigbor = Cells.Count-1;
					Cells[currentIndex].Sides[Cells[currentIndex].Sides.Count - 1].Neigbor = Cells[Cells.Count - 1];

					VoronoiLine support = Cells[currentIndex].Sides[Cells[currentIndex].Sides.Count-1];



					Cells[Cells.Count-1].Sides.Add(new VoronoiLine(support._line));
					Cells[Cells.Count - 1].Sides[Cells[Cells.Count - 1].Sides.Count - 1].IndexNeigbor = currentIndex;
					Cells[Cells.Count - 1].Sides[Cells[Cells.Count - 1].Sides.Count - 1].Neigbor = Cells[currentIndex];
				}
                for (int i = 0; i < nextIndexes.Count; i++)
				{
					if(!visited[nextIndexes[i]])
					{
						visited[nextIndexes[i]] = true;
						indexToCut.Enqueue(nextIndexes[i]);
					}
				}
			}
			return true;
			
		}

	#endregion
	}
}