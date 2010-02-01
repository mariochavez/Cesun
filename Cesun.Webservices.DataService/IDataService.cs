using System;
using Cesun.Webservices.Data;

namespace Cesun.Webservices.DataService
{
	public interface IDataService
	{
		Temblor[] GetTembloresByDate(DateTime fecha);
		Temblor[] GetTembloresByMagnitud(double magnitud);
		Temblor[] GetTembloresByProfundidad(double profundidad);
	}
}