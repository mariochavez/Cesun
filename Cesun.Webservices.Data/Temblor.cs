using System;

namespace Cesun.Webservices.Data
{
	public class Temblor
	{
		public int ID {
			get;
			set;
		}
		
		public DateTime Fecha {
			get;
			set;
		}
		
		public double Longitud {
			get;
			set;
		}
		
		public double Latitud {
			get;
			set;
		}
		
		public double Magnitud {
			get;
			set;
		}
		
		public double Profundidad {
			get;
			set;
		}
	}
}
