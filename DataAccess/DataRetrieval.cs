/************************************************************************************************************
 * Program Id:      Data Access                                                                             *
 * Author:          Glenn Seplowitz                                                                         *
 * Purpose:         Common methods for data access without having to define ado.net code each time.         *
 * Written:         September 15, 2013                                                                      *
 *                                                                                                          *
 * Modifications:                                                                                           *
 ***********************************************************************************************************/  

// import namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

// define class namespace
namespace DataAccess
{
    // define class
    public static class DataRetrieval
    {       
        #region Constructors

        #endregion

        #region Members

        public static void SetSqlConnection(string connString)
        {
            try
            {
                // initialize the connection object
                ConnectString = connString;
                ConnectionSql = new SqlConnection(connString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Establishes 
        /// </summary>
        /// <param name="connString"></param>
        /// <returns>connection object.  if null no connection made</returns>
        public static SqlConnection GetSqlConnection(string connString)
        {
            // declare variables
            SqlConnection conn;

            try
            {
                // connect to the database
                conn = new SqlConnection(connString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // return the connection
            return conn;
        }

        /// <summary>
        /// Will query the database and return results as a data table
        /// </summary>
        /// <param name="sql">the query as a string</param>
        /// <param name="conn">the connection object, must be active</param>
        /// <returns>query results in form of data table</returns>
        public static DataTable QueryDataResults(string sql, SqlConnection conn, bool isStoredProc)
        {
            // declare variables
            DataTable dt = new DataTable();
            SqlCommand cmd;
            SqlDataReader dr;

            try
            {
                // execute the query
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }
  
            // return the results
            return dt;
        }
        
        /// <summary>
        /// Calls a stored procedure with parameters
        /// </summary>
        /// <param name="storedProc">the name of the stored procedure</param>
        /// <param name="conn">the connection object</param>
        /// <param name="param">the parameters</param>
        /// <returns>a datatable with results</returns>
        public static DataTable QueryDataResults(string storedProc, SqlConnection conn, List<DAParameter> param)
        {
            // declare variables
            DataTable dt = new DataTable();
            SqlCommand cmd;
            SqlDataReader dr;

            try
            {
                // define the command object
                cmd = new SqlCommand(storedProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // add the parameters
                foreach(DAParameter pa in param)
                {
                    cmd.Parameters.Add(pa.PAParam);
                }

                // execute the query
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            // return the results
            return dt;
        }

        /// <summary>
        /// Queries data and returns values from specific parameters
        /// </summary>
        /// <param name="storedProc">the name of the stored procedure</param>
        /// <param name="conn">the connection object</param>
        /// <param name="param">the parameters</param>
        /// <returns>the output for the parameters</returns>
        public static List<DAParameter> QueryData(string storedProc, SqlConnection conn, List<DAParameter> param)
        {
            // declare variables
            int result = 0;
            SqlCommand cmd;
            List<DAParameter> paResults = null;

            try
            {
                // define the command object
                cmd = new SqlCommand(storedProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // add the parameters
                foreach (DAParameter pa in param)
                {
                    cmd.Parameters.Add(pa.PAParam);
                }

                // execute the query
                conn.Open();
                result = cmd.ExecuteNonQuery();
                conn.Close();

                // get the return parameters
                foreach (SqlParameter sqlParam in cmd.Parameters)
                {
                    if (sqlParam.Direction == ParameterDirection.InputOutput ||
                        sqlParam.Direction == ParameterDirection.Output ||
                        sqlParam.Direction == ParameterDirection.ReturnValue)
                    {
                        paResults.Add(new DAParameter(sqlParam));
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            // return the results
            return paResults;
        }

        /// <summary>
        /// executes a dml query or some query that doesn't return data
        /// </summary>
        /// <param name="sql">the sql text to execute</param>
        /// <param name="conn">the connection object</param>
        /// <returns>int - number of rows affected</returns>
        public static int ExecuteData(string sql, SqlConnection conn, bool isStoredProc)
        {
            // declare variables
            int result = 0;
            SqlCommand cmd;

            try
            {
                // execute the query
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = isStoredProc ? CommandType.Text : CommandType.StoredProcedure;
                conn.Open();
                result = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }
            
            // return the results
            return result;
        }
        
        /// <summary>
        /// Executes a dml or other query that doesn't return results with parameters
        /// </summary>
        /// <param name="storedProc">the name of the stored procedure</param>
        /// <param name="conn">the connection object</param>
        /// <param name="param">the parameters</param>
        /// <returns></returns>
        public static int ExecuteData(string storedProc, SqlConnection conn, List<DAParameter> param)
        {
            // declare variables
            int result = 0;
            SqlCommand cmd;

            try
            {
                // define the command object
                cmd = new SqlCommand(storedProc, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // pass the parameters
                foreach (DAParameter pa in param)
                {
                    cmd.Parameters.Add(pa.PAParam);
                }

                // execute the stored procedure
                conn.Open();
                result = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                result = -1;
                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }

            // return the results
            return result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets the connection string
        /// </summary>
        public static string ConnectString { get; set; }

        public static SqlConnection ConnectionSql { get; set; }

        #endregion
    }
}
