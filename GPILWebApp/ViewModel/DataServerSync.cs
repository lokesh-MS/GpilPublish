#region File Header
/*
--------------------------------------
TeamLiftss IT Systems & solutions pvt. ltd.
Copyright (c) 2021, All rights reserved

Author      : ANANDARAJ G 
Description : Basic connection method implemented.

Revision History:
Rev   Date                   Who                    Description
1.0   28/July/2021          Anandaraj G            Intial version.
--------------------------------------
*/
#endregion


using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace GPIWebApp
{
    enum SPCommand
    {
        Update, Insert, SelectMany, SelectScalar
    }

    internal class Chunk
    {

    }

    /// <summary>
    /// This class handles all the database related functions synchronosly.
    /// </summary>  
    internal class DataServerSync : IDisposable
    {
        bool disposed;

        volatile SqlConnection con = null;
        volatile SqlCommand comm = null;
        volatile SqlDataAdapter adap = null;

        private static volatile DataServerSync instance = null;
        private static object syncRoot = new Object();
        private string connectionString = null;
        public string Constring =null;
        
        public DataServerSync()
        {
            try
            {
                disposed = false;
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                con = new SqlConnection(connectionString);
                comm = new SqlCommand();
                comm.Connection = con;
                Constring = connectionString;
                comm.Connection.Open();
                adap = new SqlDataAdapter();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (comm != null)
                    {
                        if (comm.Connection.State == ConnectionState.Open)
                        {
                            comm.Connection.Close();
                            comm = null;
                        }
                    }
                    instance = null;
                }
                disposed = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);
        }



        public static DataServerSync Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataServerSync();
                    }
                }
                return instance;
            }
        }




        /// <summary>
        /// Gets the result as dataset 
        /// </summary>
        /// <param name="query">sql query</param>
        /// <returns></returns>
        public DataSet GetDataset(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                comm.CommandText = query;
                comm.CommandType = CommandType.Text;
                adap.SelectCommand = comm;
                adap.Fill(ds);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " qry else -> " + query);
            }
            return ds;
        }

        /// <summary>
        /// Gets the result as datatable
        /// </summary>
        /// <param name="query">sql query</param>
        /// <returns></returns>
        public DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                comm.CommandText = query;
                comm.CommandType = CommandType.Text;
                adap.SelectCommand = comm;
                adap.Fill(dt);

            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " qry else -> " + query);
            }
            return dt;
        }

        /// <summary>
        /// Executes insert,update and delete statements.
        /// </summary>
        /// <param name="query">sql query</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(string query)
        {
            bool res = false;
            try
            {
                comm.CommandText = query;
                comm.CommandType = CommandType.Text;
                int x = comm.ExecuteNonQuery();
                res = (x == 1 ? true : false);

            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " qry -> " + query);
            }
            return res;
           
        }

        /// <summary>
        /// Gets the result from the location 0x0.
        /// </summary>
        /// <param name="query">sql query</param>
        /// <returns></returns>
        public object ExecuteScalar(string query)
        {
            object x = null;
            try
            {
                comm.CommandText = query;
                comm.CommandType = CommandType.Text;
                x = comm.ExecuteScalar();

            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " qry -> " + query);
            }
            return x;
        }


        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="parameters">List of Pisces.Business.Parameter</param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <returns></returns>
        public DataTable ExecuteSP(List<SqlParameter> parameters, string spName)
        {
            DataTable dt = new DataTable();
            try
            {
                comm.CommandText = spName;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Clear();
                foreach (SqlParameter pm in parameters)
                {
                    SqlDbType typ = SqlDbType.VarChar;
                    switch (pm.SqlDbType)
                    {
                        case SqlDbType.DateTime:
                            typ = SqlDbType.DateTime;
                            break;
                        case SqlDbType.Int:
                            typ = SqlDbType.Int;
                            break;
                        case SqlDbType.Bit:
                            typ = SqlDbType.Bit;
                            break;
                    }
                    string pName = "@" + pm.ParameterName;
                    comm.Parameters.Add(new SqlParameter(pName, typ));
                    comm.Parameters[pName].Value = pm.Value;
                }

                adap.SelectCommand = comm;
                adap.Fill(dt);
            }

            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " SPNAME " + spName);

            }
            return dt;
        }
        public DataSet ExecuteSP(List<SqlParameter> parameters, string spName, string tableName)
        {
            DataSet ds = new DataSet();
            try
            {
                comm.CommandText = spName;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Clear();
                foreach (SqlParameter pm in parameters)
                {
                    SqlDbType typ = SqlDbType.VarChar;
                    switch (pm.SqlDbType)
                    {
                        case SqlDbType.DateTime:
                            typ = SqlDbType.DateTime;
                            break;
                        case SqlDbType.Int:
                            typ = SqlDbType.Int;
                            break;
                        case SqlDbType.Bit:
                            typ = SqlDbType.Bit;
                            break;
                    }
                    string pName = "@" + pm.ParameterName;
                    comm.Parameters.Add(new SqlParameter(pName, typ));
                    comm.Parameters[pName].Value = pm.Value;
                }

                adap.SelectCommand = comm;
                adap.Fill(ds, tableName);
            }
            catch(Exception ec)
            { }
            //catch (InvalidOperationException ex)
            //{
            //    throw new Exception(ex.Message + " SPNAME " + spName);

            //}
            return ds;
        }















        /// <summary>
        /// Generic function which executes StoredProcedures.
        /// </summary>
        /// <typeparam name="T">The return type can be any</typeparam>
        /// <param name="parameters">List of SQL Parameters</param>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="spCommand">Kind of operation to be performed</param>
        /// <returns></returns>
        public bool ExecuteSP(List<SqlParameter> parameters, string spName, SPCommand spCommand)
        {
            bool res = false;
            int x = 0;
            try
            {
                //lock (comm)
                {
                    comm.CommandText = spName;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Clear();
                    foreach (SqlParameter pm in parameters)
                    {
                        comm.Parameters.Add(pm);
                    }

                    switch (spCommand)
                    {
                        case SPCommand.Insert:
                            x = comm.ExecuteNonQuery();
                            break;
                        case SPCommand.Update:
                            x = comm.ExecuteNonQuery();
                            break;
                            //case SPCommand.SelectMany:
                            //    DataTable dt = new DataTable();
                            //    adap.SelectCommand = comm;
                            //    adap.Fill(dt);
                            //    obj = dt;
                            //    break;
                            //case SPCommand.SelectScalar:
                            //    x = comm.ExecuteScalar();
                            //    break;
                    }
                    res = (x == 1 ? true : false);


                }
            }
            catch (InvalidOperationException ex)
            {
                //Dispose(true);
                //CreateLocalInstance();
                //ExecuteSP<T>(parameters, spName, spCommand);

                string s = "<<Message>> " + ex.Message;
                s += "<<SPNAME>> " + spName;
                foreach (SqlParameter pm in parameters)
                {
                    s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                }
                throw new Exception(s);
            }
            return res;
        }
        public string ExecuteSP_RtnString(List<SqlParameter> parameters, string spName)
        {
            string res = string.Empty;
            int x = 0;
            try
            {
                //lock (comm)
                {
                    comm.CommandText = spName;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Clear();
                    foreach (SqlParameter pm in parameters)
                    {
                        comm.Parameters.Add(pm);
                    } 
                    comm.ExecuteNonQuery();
                    res = (string)comm.Parameters["@OUTPUT"].Value;


                }
            }
            catch (InvalidOperationException ex)
            {
                //Dispose(true);
                //CreateLocalInstance();
                //ExecuteSP<T>(parameters, spName, spCommand);

                string s = "<<Message>> " + ex.Message;
                s += "<<SPNAME>> " + spName;
                foreach (SqlParameter pm in parameters)
                {
                    s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                }
                throw new Exception(s);
            }
            return res;
        }

        public string ExecuteSPP_RtnString(List<SqlParameter> parameters, string spName)
        {
            string res = string.Empty;
            int x = 0;
            try
            {
                //lock (comm)
                {
                    comm.CommandText = spName;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Clear();
                    foreach (SqlParameter pm in parameters)
                    {
                        comm.Parameters.Add(pm);
                    }
                    comm.ExecuteNonQuery();
                    res = (string)comm.Parameters["@ERR"].Value;


                }
            }
            catch (InvalidOperationException ex)
            {
                //Dispose(true);
                //CreateLocalInstance();
                //ExecuteSP<T>(parameters, spName, spCommand);

                string s = "<<Message>> " + ex.Message;
                s += "<<SPNAME>> " + spName;
                foreach (SqlParameter pm in parameters)
                {
                    s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                }
                throw new Exception(s);
            }
            return res;
        }

        /// <summary>
        /// Generic function which executes StoredProcedures and the value of output parameters can also be obtained.
        /// </summary>
        /// <typeparam name="T">The return type can be any</typeparam>
        /// <param name="parameters">List of SQL Parameters</param>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="spCommand">Kind of operation to be performed</param>
        /// <param name="opParams">Collection of output parameters name and value</param>
        /// <returns></returns>
        public T ExecuteSP<T>(List<SqlParameter> parameters, string spName, SPCommand spCommand, out Dictionary<string, object> opParams)
        {
            object obj = null;
            try
            {
                //lock (comm)
                {
                    comm.CommandText = spName;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.Clear();
                    List<string> opParamNames = new List<string>();
                    foreach (SqlParameter pm in parameters)
                    {
                        if (pm.Direction == ParameterDirection.Output) opParamNames.Add(pm.ParameterName);
                        comm.Parameters.Add(pm);
                    }
                    switch (spCommand)
                    {
                        case SPCommand.Insert:
                        case SPCommand.Update:
                            obj = comm.ExecuteNonQuery();
                            break;
                        case SPCommand.SelectMany:
                            DataTable dt = new DataTable();
                            adap.SelectCommand = comm;
                            adap.Fill(dt);
                            obj = dt;
                            break;
                        case SPCommand.SelectScalar:
                            obj = comm.ExecuteScalar();
                            break;
                    }
                    Dictionary<string, object> lclOPParams = new Dictionary<string, object>();
                    lclOPParams.Clear();
                    foreach (string key in opParamNames)
                    {
                        lclOPParams.Add(key, comm.Parameters[key].Value);
                    }
                    opParams = lclOPParams;
                }
            }
            catch (InvalidOperationException ex)
            {
                //Dispose(true);
                //CreateLocalInstance();
                //ExecuteSP<T>(parameters, spName, spCommand,out opParams);

                string s = "<<Message>> " + ex.Message;
                s += "<<SPNAME>> " + spName;
                foreach (SqlParameter pm in parameters)
                {
                    s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                }

                throw new Exception(s);

                /*
                string errMsg = "There is already an open DataReader associated with this Command which must be closed first.";
                if (ex.Message.ToUpper().Equals(errMsg.ToUpper()))
                {
                    if (comm != null)
                    {
                        if (comm.Connection.State == ConnectionState.Open)
                        {
                            string s = "<<Message>> " + ex.Message;
                            s += "<<SPNAME>> " + spName;
                            foreach (SqlParameter pm in parameters)
                            {
                                s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                            }

                            throw new Exception(s);
                            //ExecuteSP<T>(parameters, spName, spCommand, ref opParams);
                        }
                    }
                }
                else
                {
                    string s = "##ELSE## <<Message>> " + ex.Message;
                    s += "<<SPNAME>> " + spName;
                    foreach (SqlParameter pm in parameters)
                    {
                        s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                    }

                    throw new Exception(s);
                }
                 */
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }


        /// <summary>
        /// List of quries will be executed in a transaction.
        /// </summary>
        /// <param name="qryCollection">list of quries</param>
        /// <returns></returns>
        /// 





        public bool Transaction(List<string> qryCollection)
        {
            bool res = false;
            SqlTransaction transaction = null;

            try
            {
                // con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                transaction = con.BeginTransaction();
                // Assign Transaction to Command
                comm.Transaction = transaction;
                foreach (string qry in qryCollection)
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = qry;
                    comm.ExecuteNonQuery();
                }
                transaction.Commit();
                res = true;
            }

            catch (InvalidOperationException ex)
            {
                transaction.Rollback();
                Dispose(true);
                string errMsg = "There is already an open DataReader associated with this Command which must be closed first.";
                if (ex.Message.ToUpper().Equals(errMsg.ToUpper()))
                {
                    if (comm != null)
                    {
                        if (comm.Connection.State == ConnectionState.Open) { }
                        //Transaction(qryCollection);
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Dispose(true);
            }
            finally
            { }
            return res;
        }


    

        public bool TransactionInsert(List<string> qryCollection)
        {
            SqlConnection db = new SqlConnection(connectionString);
            SqlTransaction transaction;
            bool res = false;

            db.Open();
            transaction = db.BeginTransaction();
            try
            {
                foreach (string qry in qryCollection)
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = qry;
                    comm.ExecuteNonQuery();
                }
                transaction.Commit();
                res = true;
            }
            catch (SqlException sqlError)
            {
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

            finally
            { db.Close(); }

            return res;
        }


    }
}