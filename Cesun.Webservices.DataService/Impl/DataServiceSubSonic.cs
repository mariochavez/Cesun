using System;
using System.Linq;
using Cesun.Webservices.Data;
using SubSonic.Repository;

namespace Cesun.Webservices.DataService
{
	public class DataServiceSubSonic : IDataService
	{
	    readonly IRepository repository;
		
		public DataServiceSubSonic (IRepository repository)
		{
			this.repository = repository;
		}
		
		public Temblor[] GetTembloresByDate(DateTime fecha)
		{
			return null;
		}
		
		public Temblor[] GetTembloresByMagnitud(double magnitud)
		{
		    var temblores = from t in repository.All<Temblor>()
		                    where t.Magnitud >= magnitud && t.Magnitud < (magnitud + 1)
		                    select t;

		    var temblor = temblores.ToArray()[0];
		    repository.Update(temblor);

			return temblores.ToArray();
		}
		
		public Temblor[] GetTembloresByProfundidad(double profundidad)
		{
			return null;
		}
	}
}
