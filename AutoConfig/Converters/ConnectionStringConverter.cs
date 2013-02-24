using System.Configuration;
using System.Data.Common;

namespace AutoConfig.Converters
{
	static class ConnectionStringConverter
	{
		/// <summary>
		/// Retrieve a connection instance by it's name.
		/// </summary>
		/// <param name="name">ConnectionStrings name.</param>
		/// <returns>Connection instance.</returns>
		internal static Factory<DbConnection> ToDbConnection(ConnectionStringSettings settings)
		{
			var factory = DbProviderFactories.GetFactory(settings.ProviderName);

			return new Factory<DbConnection>(() =>
			{
				var connection = factory.CreateConnection();
				connection.ConnectionString = settings.ConnectionString;
				return connection;
			});
		}

	}
}
