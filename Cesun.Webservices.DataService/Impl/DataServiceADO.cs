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
        const string getTembloresByDate = "SELECT * FROM temblors WHERE fecha >= @fecha1 AND fecha < @fecha2";
        const string getTembloresByProfundidad = "SELECT * FROM temblors WHERE profundidad >= @profundidad1 AND profundidad < @profundidad2";
        const string getTembloresById = "SELECT * FROM temblors WHERE id = @id";

	    const string updateTemblor = "UPDATE temblors SET fecha = @fecha, longitud = @longitud, latitud = @latitud,  magnitud = @magnitud, profundidad = @profundidad WHERE id = @id";
        const string insertTemblor = "INSERT INTO temblors (fecha, longitud, latitud, magnitud, profundidad) VALUES (@fecha, @longitud, @latitud, @magnitud, @profundidad); SELECT @@IDENTITY";

	    readonly IDbConnection connection;
		
		public DataServiceADO (IDbConnection connection)
		{
			this.connection = connection;
		}
		
		public Temblor[] GetTembloresByDate(DateTime date)
		{
            SqlCommand command = new SqlCommand(getTembloresByDate, connection as SqlConnection);
            command.Parameters.AddWithValue("@fecha1", date.Date);
            command.Parameters.AddWithValue("@fecha2", date.Date.AddDays(1));
            command.CommandType = CommandType.Text;

            return LoadTemblores(command);
		}
		
		public Temblor[] GetTembloresByMagnitud(double magnitud)
		{
            SqlCommand command = new SqlCommand(getTembloresByMagnitud, connection as SqlConnection);
			command.Parameters.AddWithValue("@magnitud1", magnitud);
			command.Parameters.AddWithValue("@magnitud2", magnitud + 1);
			command.CommandType = CommandType.Text;

		    return LoadTemblores(command);
		}

        public Temblor[] GetTembloresByProfundidad(double profundidad)
        {
            SqlCommand command = new SqlCommand(getTembloresByProfundidad, connection as SqlConnection);
            command.Parameters.AddWithValue("@profundidad1", profundidad);
            command.Parameters.AddWithValue("@profundidad2", profundidad + 1);
            command.CommandType = CommandType.Text;

            return LoadTemblores(command);
        }

        public Temblor GetById(int id)
        {
            SqlCommand command = new SqlCommand(getTembloresById, connection as SqlConnection);
            command.Parameters.AddWithValue("@id", id);
            command.CommandType = CommandType.Text;

            var temblores = LoadTemblores(command);
            return temblores.Length > 0 ? temblores[0] : null;
        }

        public void Save(Temblor temblor)
        {
            SqlCommand command = new SqlCommand(temblor.Id != 0 ? insertTemblor : updateTemblor , connection as SqlConnection);
            command.Parameters.AddWithValue("@magnitud", temblor.Magnitud);
            command.Parameters.AddWithValue("@profundidad", temblor.Profundidad);
            command.Parameters.AddWithValue("@latitud", temblor.Latitud);
            command.Parameters.AddWithValue("@longitud", temblor.Longitud);
            command.Parameters.AddWithValue("@fecha", temblor.Fecha);
            command.CommandType = CommandType.Text;

            if (temblor.Id != 0)
            {
                command.Parameters.AddWithValue("@id", temblor.Id);
                command.ExecuteNonQuery();
            }else
            {
                int id = (int) command.ExecuteScalar();
                temblor.Id = id;
            }
        }

        Temblor[] LoadTemblores(SqlCommand command)
	    {
	        List<Temblor> temblores = new List<Temblor>();
			
	        using(SqlDataReader reader = command.ExecuteReader()) {
	            if(reader.HasRows) {
	                while (reader.Read()) {
						
	                    Temblor temblor = new Temblor
	                                          {
	                                              Id = reader.GetInt32(0),
	                                              Fecha = reader.GetDateTime(1),
	                                              Latitud = reader.GetDouble(2),
	                                              Longitud = reader.GetDouble(3),
	                                              Magnitud = reader.GetDouble(4),
	                                              Profundidad = reader.GetDouble(5)
	                                          };

	                    temblores.Add(temblor);
	                }
	            }
	        }
	        return temblores.ToArray();
	    }
	}
}
