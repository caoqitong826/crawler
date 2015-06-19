using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BJAirQuality
{
    public static class SqlHelper
    {
        public static string connectionStr = "your connection string";
        //ConfigurationManager.ConnectionStrings["MySqlCon"].ConnectionString;
 
        /// <summary>
        ///Prepare parameters for the implementation of the command
        /// </summary>
        /// <param name="cmd">mySqlCommand command</param>
        /// <param name="conn">database connection that is existing</param>
        /// <param name="trans">database transaction processing </param>
        /// <param name="cmdType">SqlCommand command type (stored procedures, T-SQL statement, and so on.) </param>
        /// <param name="cmdText">Command text, T-SQL statements such as Select * from Products</param>
        /// <param name="cmdParms">return the command that has parameters</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 120;
            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
        }


        //public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        //{
        //    MySqlCommand cmd = new MySqlCommand();

        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
        //        int val = cmd.ExecuteNonQuery();
        //        cmd.Parameters.Clear();
        //        return val;
        //    }
        //}
        public static int ExecuteNonQuery1(MySqlConnection connect, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 120;
            using (MySqlConnection conn = connect)
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                return val;
                
            }
            
        }
        public static DataTable GetDataSet(string connectionString, string cmdText, params MySqlParameter[] commandParameters)
        {
            DataSet retSet = new DataSet();
            using (MySqlDataAdapter msda = new MySqlDataAdapter(cmdText, connectionString))
            {
                msda.Fill(retSet);
            }
            return retSet.Tables[0];
        }

    }
}
