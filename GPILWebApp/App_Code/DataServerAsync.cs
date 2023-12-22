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
using System.Collections.Generic;
using System.Threading;


namespace GPI
{


    /// <summary>
    /// This class handles all the database related functions asynchronosly.
    /// </summary>
    /// 


    //enum SPCommand
    //{
    //    Update, Insert, SelectMany, SelectScalar
    //}

    //internal class Chunk
    //{

    //}



    public class DataServer : IDisposable
    {
        bool disposed;

        SqlConnection con = null;
        SqlCommand comm = null;
        SqlDataAdapter adap = null;

        private static DataServer instance = null;
        private static object syncRoot = new Object();
        private string connectionString1 = null;
        private string connectionString = null;
         public string Constring =null;

        SqlCommand commTans = null;
        string strConnection = null;
        SqlTransaction trans = null;



        public DataServer()
        {
            //////disposed = false;            
            //////connectionString =  System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
            ////////con = new SqlConnection(Config.ConnectionString);
            ////////comm = new SqlCommand();
            ////////comm.Connection = con;
            ////////comm.Connection.Open();
            //////adap = new SqlDataAdapter();


            disposed = false;
              connectionString = ConfigurationManager.AppSettings["ConnectionString"]+"async=true";
            con = new SqlConnection(connectionString);
            Constring = connectionString;
            comm = new SqlCommand();
            comm.Connection = con;
            comm.Connection.Open();
            adap = new SqlDataAdapter();
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



        public static DataServer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataServer();
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
                //lock (comm)
                {
                    using (con = new SqlConnection(connectionString))
                    {
                        using (comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            comm.Connection.Open();
                            comm.CommandText = query;
                            comm.CommandType = CommandType.Text;
                           
                            //IAsyncResult aRes = comm.BeginExecuteReader(CommandBehavior.CloseConnection);
                            //WaitHandle wHandle = aRes.AsyncWaitHandle;
                            //wHandle.WaitOne();
                            //using (IDataReader rdr = comm.EndExecuteReader(aRes))
                            //{
                            //    if (rdr != null)
                            //   {
                            //        ds.Tables.Add("Test");
                            //        ds.Tables[0].Load(rdr);
                            //   }

                            //}
                            adap.SelectCommand = comm;
                            adap.Fill(ds);

                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " qry else -> " + query);
            }
            finally
            {
                if (comm != null)
                {
                    if (comm.Connection.State == ConnectionState.Open)
                        comm.Connection.Close();
                }
            }
            return ds;
        }
        public DataSet GetDatasetDispatch(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                //lock (comm)
                {
                    using (con = new SqlConnection(connectionString))
                    {
                        using (comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            comm.Connection.Open();
                            comm.CommandText = query;
                            comm.CommandType = CommandType.Text;
                            IAsyncResult aRes = comm.BeginExecuteReader(CommandBehavior.CloseConnection);
                            WaitHandle wHandle = aRes.AsyncWaitHandle;
                            wHandle.WaitOne();
                            using (IDataReader rdr = comm.EndExecuteReader(aRes))
                            {
                               if (rdr != null)
                               {
                                   ds.Tables.Add("Test");
                                   ds.Tables[0].Load(rdr);
                               }

                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " qry else -> " + query);
            }
            finally
            {
                if (comm != null)
                {
                    if (comm.Connection.State == ConnectionState.Open)
                        comm.Connection.Close();
                }
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
                //lock (comm)
                {
                    using (con = new SqlConnection(connectionString))
                    {
                        using (comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            comm.Connection.Open();
                            comm.CommandText = query;
                            comm.CommandType = CommandType.Text;
                           //IAsyncResult aRes = comm.BeginExecuteReader(CommandBehavior.Default);
                           // WaitHandle wHandle = aRes.AsyncWaitHandle;
                           // wHandle.WaitOne();
                           // using (IDataReader rdr = comm.EndExecuteReader(aRes))
                           // {
                           //     dt.Load(rdr);
                           // }

                            adap.SelectCommand = comm;
                            adap.Fill(dt);
                           
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {

                throw new Exception(ex.Message + " qry else -> " + query);

            }
            return dt;
        }


        public DataTable GetDataTableDispatch(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                //lock (comm)
                {
                    using (con = new SqlConnection(connectionString))
                    {
                        using (comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            comm.Connection.Open();
                            comm.CommandText = query;
                            comm.CommandType = CommandType.Text;
                            IAsyncResult aRes = comm.BeginExecuteReader(CommandBehavior.Default);
                            WaitHandle wHandle = aRes.AsyncWaitHandle;
                            wHandle.WaitOne();
                            using (IDataReader rdr = comm.EndExecuteReader(aRes))
                            {
                                dt.Load(rdr);
                            }

                            
                        }
                    }
                }
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
                //lock (comm)
                {
                    using (con = new SqlConnection(connectionString))
                    {
                        using (comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            comm.Connection.Open();
                            comm.CommandText = query;
                            comm.CommandType = CommandType.Text;
                            IAsyncResult aRes = comm.BeginExecuteNonQuery();
                            WaitHandle wHandle = aRes.AsyncWaitHandle;
                            wHandle.WaitOne();
                            int x = comm.EndExecuteNonQuery(aRes);
                            res = (x == 1 ? true : false);
                        }
                    }
                }
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
                //lock(comm)
                {
                    using (con = new SqlConnection(connectionString))
                    {
                        using (comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            comm.Connection.Open();
                            comm.CommandText = query;
                            comm.CommandType = CommandType.Text;
                            IAsyncResult aRes = comm.BeginExecuteReader(CommandBehavior.SingleResult);
                            WaitHandle wHandle = aRes.AsyncWaitHandle;
                            wHandle.WaitOne();
                            SqlDataReader rdr = comm.EndExecuteReader(aRes);
                            //rdr.NextResult();
                            if (rdr.Read()) x = rdr.GetValue(0);
                            rdr.Close();
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " qry -> " + query);
            }
            finally
            {
            }
            return x;
        }

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <param name="parameters">List of Business.Parameter</param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <returns></returns>
        public DataTable ExecuteSP(List<SqlParameter> parameters, string spName)
        {
            DataTable dt = new DataTable();
            try
            {
                //lock (comm)
                {
                    using (con = new SqlConnection(connectionString))
                    {
                        using (comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            comm.Connection.Open();

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
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message + " SPNAME " + spName);
            }
            return dt;
        }








        /// <summary>
        /// Generic function which executes StoredProcedures.
        /// </summary>
        /// <typeparam name="T">The return type can be any</typeparam>
        /// <param name="parameters">List of SQL Parameters</param>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="spCommand">Kind of operation to be performed</param>
        /// <returns></returns>


        /// <summary>
        /// Generic function which executes StoredProcedures and the value of output parameters can also be obtained.
        /// </summary>
        /// <typeparam name="T">The return type can be any</typeparam>
        /// <param name="parameters">List of SQL Parameters</param>
        /// <param name="spName">Stored procedure name</param>
        /// <param name="spCommand">Kind of operation to be performed</param>
        /// <param name="opParams">Collection of output parameters name and value</param>
        /// <returns></returns>
       public T ExecuteSP<T>(List<SqlParameter> parameters, string spName,string insUp, out Dictionary<string, object> opParams)
        {
            object obj = null;
            try
            {
                //lock (comm)
                {
                    using (con = new SqlConnection(connectionString))
                    {
                        using (comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            comm.Connection.Open();
                            comm.CommandText = spName;
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.Clear();
                            List<string> opParamNames = new List<string>();
                            foreach (SqlParameter pm in parameters)
                            {
                                if (pm.Direction == ParameterDirection.Output) opParamNames.Add(pm.ParameterName);
                                comm.Parameters.Add(pm);
                            }
                            IAsyncResult aRes = null;
                            WaitHandle wHandle;
                            switch (insUp)
                            {
                                case "Insert":
                                case "Update":
                                    aRes = comm.BeginExecuteNonQuery();
                                    wHandle = aRes.AsyncWaitHandle;
                                    wHandle.WaitOne();
                                    obj = comm.EndExecuteNonQuery(aRes);
                                    //obj = comm.ExecuteNonQuery();
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
                }
            }
            catch (InvalidOperationException ex)
            {
                string s = "<<Message>> " + ex.Message;
                s += "<<SPNAME>> " + spName;
                foreach (SqlParameter pm in parameters)
                {
                    s += "[parameter] <NAME : " + pm.ParameterName + "> <VALUE : " + pm.Value + " >";
                }

                throw new Exception(s);
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// List of quries will be executed in a transaction.
        /// </summary>
        /// <param name="qryCollection">list of quries</param>
        /// <returns></returns>
        public bool Transaction(List<string> qryCollection)
        {
            bool res = false;
            SqlTransaction transaction = null;

            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    using (comm = new SqlCommand())
                    {
                        comm.Connection = con;
                        comm.Connection.Open();

                        transaction = con.BeginTransaction();
                        // Assign Transaction to Command
                        comm.Transaction = transaction;
                        foreach (string qry in qryCollection)
                        {
                            comm.CommandText = qry;
                            comm.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        res = true;
                    }
                }
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
                return res;
            }
            catch
            {
                transaction.Rollback();
                Dispose(true);
                return res;
            }
            finally
            { }
            return res;
        }




        public bool ExecuteNonQueryWithTrans(string query)
        {
            bool res = false;
            try
            {
                commTans = new SqlCommand(query, con, trans);
                int x = commTans.ExecuteNonQuery();
                res = (x == 1 ? true : false);
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }

            return res;
        }


        public DataTable GetDataTableTrans(string query)
        {
            DataTable dt = new DataTable();
            try
            {

                commTans = new SqlCommand(query, con, trans);
                adap.SelectCommand = commTans;
                adap.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //if(comm.Connection.State==ConnectionState.Open)
                //comm.Connection.Close();
            }
            return dt;
        }


        public object ExecuteScalarwithTrans(string query)
        {
            object x = null;
            try
            {
                commTans = new SqlCommand(query, con, trans);
                x = commTans.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                //if(comm.Connection.State==ConnectionState.Open)
                //comm.Connection.Close();
            }
            return x;
        }

        public void commitTans()
        {
            try
            {

                trans.Commit();

            }
            catch (Exception ex)
            {
            }
        }
        public void beginTans()
        {
            try
            {

                trans = con.BeginTransaction();

            }
            catch (Exception ex)
            {
            }
        }
        public void RollBackTans()
        {
            try
            {

                trans.Rollback();

            }
            catch (Exception ex)
            {
            }
        }

        public void disposeTans()
        {
            try
            {

                trans.Dispose();

            }
            catch (Exception ex)
            {
            }
        }

    }
}
