using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Metria.Euclidian._2
{
	public class Poligon 
	{
	#region Variables

		protected List<Line> _sides;

		public List<Line> Sides
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

		public Poligon()
		{
			_sides = new List<Line>();
		}

		public Poligon(List<Line> sides)
		{
			Sides = sides;
		}

		public Poligon(Poligon P)
		{
			_sides = P._sides;
		}

	#endregion
	#region Methods

		public virtual bool IsInside (Point P)
		{	
			throw new NotImplementedException("Still not defined how is going to implement this");
		}

		public virtual Poligon[] CutPoligon (Line L)
		{
			throw new NotImplementedException("Still not defined how is going to implement this");
		}

	#endregion
	}
}
