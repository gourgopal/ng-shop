using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class SQLConnect
    {
        static SqlCommand SqlComm = null;
        private static string mConnectionString;
        public static void SetConnectionString(string connectionString)
        {
            mConnectionString = connectionString;
        }
        public static string ConnectionString
        {
            get
            {
                return mConnectionString;
            }
        }
        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
        public static void CloseSqlDataReader(SqlDataReader reader)
        {
            reader.Close();
        }

        internal static object ExecuteNonQuery(string v, List<SqlParameter> addToCartParameters, object storedProcedure)
        {
            throw new NotImplementedException();
        }

        // Summary:
        //     Executes the query, and returns the first column of the first row in the result
        //     set returned by the query. Additional columns or rows are ignored.
        //
        // Returns:
        //     The first column of the first row in the result set, or a null reference (Nothing
        //     in Visual Basic) if the result set is empty. Returns a maximum of 2033 characters.
        public static object GetScalar(string CommandText)
        {
            return GetScalar(CommandText, null, CommandType.Text);
        }

        // Summary:
        //     Executes the query, and returns the first column of the first row in the result
        //     set returned by the query. Additional columns or rows are ignored.
        //
        // Returns:
        //     The first column of the first row in the result set, or a null reference (Nothing
        //     in Visual Basic) if the result set is empty. Returns a maximum of 2033 characters.
        public static object GetScalar(string CommandText, List<SqlParameter> ParameterValues, CommandType CommandType)
        {
            using (var command = new SqlCommand(CommandText, Connection))
            {
                command.Connection.Open();
                command.CommandTimeout = 600;
                command.CommandType = CommandType;
                if ((ParameterValues != null))
                {
                    foreach (var Parameter in ParameterValues)
                    {
                        command.Parameters.Add(Parameter);
                    }
                }
                return command.ExecuteScalar();
            }
        }
        public static SqlDataReader GetSqlDataReader(string CommandText)
        {
            return GetSqlDataReader(CommandText, null, CommandType.Text);
        }
        public static SqlDataReader GetSqlDataReader(string CommandText, List<SqlParameter> ParameterValues, CommandType CommandType)
        {
            using (var command = new SqlCommand(CommandText, Connection))
            {
                command.Connection.Open();
                command.CommandTimeout = 600;
                command.CommandType = CommandType;
                if ((ParameterValues != null))
                {
                    foreach (var Parameter in ParameterValues)
                    {
                        command.Parameters.Add(Parameter);
                    }
                }
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        public static int ExecuteNonQuery(string CommandText)
        {
            return ExecuteNonQuery(CommandText, null, CommandType.Text);
        }
        public static int ExecuteNonQuery(string CommandText,
               List<SqlParameter> ParameterValues)
        {
            return ExecuteNonQuery(CommandText, ParameterValues, CommandType.Text);
        }
        public static int ExecuteNonQuery(string CommandText,
               List<SqlParameter> ParameterValues, CommandType CommandType)
        {
            var res = 0;
            var SqlConn = Connection;
            SqlConn.Open();
            try
            {
                SqlComm = new SqlCommand(CommandText, SqlConn)
                {
                    CommandTimeout = 600,
                    CommandType = CommandType
                };
                if ((ParameterValues != null))
                {
                    foreach (var Parameter in ParameterValues)
                    {
                        SqlComm.Parameters.Add(Parameter);
                    }
                }
                res = SqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + ex.StackTrace, ex.InnerException);
            }
            finally
            {
                SqlConn.Close();
            }
            return res;
        }
    }
}