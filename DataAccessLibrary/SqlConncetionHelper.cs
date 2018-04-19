using System;
using System.Configuration;
using System.Data.Common;

namespace DataAccessLibrary
{
    public class SqlConncetionHelper
    {
        public static DbConnection Connection
        {
            get
            {
                //Connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;
                DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager
                                            .ConnectionStrings["DB171"].ProviderName);
                DbConnection connection = providerFactory.CreateConnection();

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["DB171"].ConnectionString;
                return connection;
            }
        }

        public static DbDataAdapter DataAdapter
        {
            get
            {
                DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager
                                            .ConnectionStrings["DB171"].ProviderName);

                return providerFactory.CreateDataAdapter();
            }
        }

        public static DbProviderFactory ProviderFactory
        {
            get
            {
                return  DbProviderFactories.GetFactory(ConfigurationManager
                                            .ConnectionStrings["DB171"].ProviderName);
            }
        }

        public static bool IsAdminModuleEnabled()
        {
            bool isAdminModuleEnabled;
            Boolean.TryParse(ConfigurationManager.AppSettings["isAdminModuleEnabled"],
                                                            out isAdminModuleEnabled);

            return isAdminModuleEnabled;
        }

        public static bool ExecuteCommands(params DbCommand[] commands)
        {
            using (DbConnection connection = Connection)
            {
                connection.Open();

                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (var command in commands)
                    {
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (DbException ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
