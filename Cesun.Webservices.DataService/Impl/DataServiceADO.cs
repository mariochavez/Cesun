using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Cesun.Webservices.Data;

namespace Cesun.Webservices.DataService
{
	public class DataServiceADO : IDataService
	{
        const string getTembloresByMagnitud = "SELECT * FROM temblors WHERE magnitud >= @magnitud1 AND magnitud < @magnitud2";

	    readonly IDbConnection connection;
		
		public DataServiceADO (IDbConnection connection)
		{
			this.connection = connection;
		}
		
		public Temblor[] GetTembloresByDate(DateTime date)
		{
			return null;
		}
		
		public Temblor[] GetTembloresByMagnitud(double magnitud)
		{
            SqlCommand command = new SqlCommand(getTembloresByMagnitud, connection as SqlConnection);
			command.Parameters.AddWithValue("@magnitud1", magnitud);
			command.Parameters.AddWithValue("@magnitud2", magnitud + 1);
			command.CommandType = CommandType.Text;
			
			List<Temblor> temblores = new List<Temblor>();
			
			using(SqlDataReader reader = command.ExecuteReader()) {
				if(reader.HasRows) {
					while (reader.Read()) {
						
						Temblor temblor = new Temblor();
						
						temblor.ID = reader.GetInt32(0);
						temblor.Fecha = reader.GetDateTime(1);
						temblor.Latitud = reader.GetDouble(2);
						temblor.Longitud = reader.GetDouble(3);
						temblor.Magnitud = reader.GetDouble(4);
						temblor.Profundidad = reader.GetDouble(5);
						
						temblores.Add(temblor);
					}
				}
			}
			
			return temblores.ToArray();
		}
		
		public Temblor[] GetTembloresByProfundidad(double profundidad)
		{
			return null;
		}
	}
}
