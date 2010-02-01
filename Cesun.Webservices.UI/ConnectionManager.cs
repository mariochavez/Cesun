using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SubSonic.Repository;

namespace Cesun.Webservices.UI
{
	public class ConnectionManager : IDisposable
	{
	    const string ConnectionName = "mydb";

		SqlConnection sqlConnection;
		IRepository repository;
		static ConnectionManager connectionManager;

        public static SqlConnection GetSQLiteConnection()
		{
			if(connectionManager == null) 
				connectionManager = new ConnectionManager();
			
			if(connectionManager.GetOpenedSQLConnection() == null)
				connectionManager.OpenSqliteConnection();
			
			return connectionManager.GetOpenedSQLConnection();
		}
		
		public static IRepository GetRepository()
		{
			if(connectionManager == null)
				connectionManager = new ConnectionManager();
			
			if(connectionManager.GetCurrentRepository() == null)
				connectionManager.CreateRepository();
			
			return connectionManager.GetCurrentRepository();
		}
		
		public static void CloseAll()
		{
			if(connectionManager != null)
				connectionManager.Dispose();
		}

        SqlConnection GetOpenedSQLConnection()
		{
			return sqlConnection;
		}
		
		IRepository GetCurrentRepository()
		{
			return repository;
		}
		
		void OpenSqliteConnection()
		{
			sqlConnection = new SqlConnection(ConnectionString());
			sqlConnection.Open();
		}
		
		void CreateRepository()
		{
            repository = new SimpleRepository(ConnectionName, SimpleRepositoryOptions.RunMigrations);
		}
		
		string ConnectionString()
		{
            ConnectionStringSettingsCollection connectionStrings =
                    ConfigurationManager.ConnectionStrings;

		    return connectionStrings[ConnectionName].ConnectionString;
		}
		
		public void Dispose(){
			if(sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
				sqlConnection.Close();
		}
	}
}
