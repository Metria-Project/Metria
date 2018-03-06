using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metria.Euclidian._2
{
	public class ConvexPoligon : Poligon
	{
	#region Constructor

		public ConvexPoligon () : base () { }

		public ConvexPoligon(List<Line> sides) : base (sides) { }

		public ConvexPoligon(ConvexPoligon C) : base(C) { }

	#endregion
	#region Methods

		

	#endregion
	}
}
