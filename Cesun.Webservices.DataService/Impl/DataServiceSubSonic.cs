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
		    var temblores = from t in repository.All<Temblor>()
		                    where t.Fecha >= fecha.Date && t.Fecha < fecha.Date.AddDays(1) 
                            select t;
            return temblores.ToArray();
		}
		
		public Temblor[] GetTembloresByMagnitud(double magnitud)
		{
		    var temblores = from t in repository.All<Temblor>()
		                    where t.Magnitud >= magnitud && t.Magnitud < (magnitud + 1)
		                    select t;

			return temblores.ToArray();
		}
		
		public Temblor[] GetTembloresByProfundidad(double profundidad)
		{
            var temblores = from t in repository.All<Temblor>()
                            where t.Profundidad >= profundidad && t.Profundidad < (profundidad + 1)
                            select t;

            return temblores.ToArray();
		}

	    public Temblor GetById(int id)
	    {
	        var temblor = repository.Single<Temblor>(x => x.Id == id);
	        return temblor;
	    }

	    public void Save(Temblor temblor)
	    {
            if (temblor.Id != 0)
                repository.Update(temblor);
            else
                repository.Add(temblor);
	    }
	}
}
