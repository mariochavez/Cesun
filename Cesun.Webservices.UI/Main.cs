using System;
using System.Collections.Generic;
using Cesun.Webservices.DataService;
using Cesun.Webservices.Data;

namespace Cesun.Webservices.UI
{
	class MainClass
	{
		public static void Main (string[] args)
		{			
            IDataService dataService = new DataServiceADO(ConnectionManager.GetSQLiteConnection());
            Temblor[] temblores = dataService.GetTembloresByMagnitud(5);
            PrintData("ADO", temblores);

		    Console.WriteLine();
            
            dataService = new DataServiceSubSonic(ConnectionManager.GetRepository());
            temblores = dataService.GetTembloresByMagnitud(4);
			PrintData("Subsonic", temblores);

		    Temblor temblor = dataService.GetById(1);
		    temblor.Profundidad = 10.5;
            dataService.Save(temblor);
            temblor = dataService.GetById(1);
            Console.WriteLine("Temblor actualizado, profundidad {0}", temblor.Profundidad);

            ConnectionManager.CloseAll();
		    Console.ReadLine();
		}
		
		static void PrintData(string source, IEnumerable<Temblor> temblores)
		{
			Console.WriteLine(String.Format("Usando {0}", source));
			
			foreach (Temblor temblor in temblores) {
				Console.WriteLine(String.Format("Ocurrido el {0}, en Long {1} Lat {2} a una profundidad de {3}",
				                  temblor.Fecha, temblor.Longitud, temblor.Latitud, temblor.Profundidad));
			}
		}
	}
}
