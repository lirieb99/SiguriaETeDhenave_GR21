using System;
using System.Data;
using System.Data.SqlClient;

namespace ds
{
    public class DatabaseConnection
    {
        SqlConnection conn;
        private static string strProvider = "Data Source=DESKTOP-3KKS0C2\\SQLEXPRESS;Initial Catalog = DB; Integrated Security = True";

        public bool Open()
        {
            try
            {
                conn = new SqlConnection(strProvider);
                conn.Open();
                return true;
            }
            catch (Exception er)
            {
                throw new Exception("Connection Error ! " + er.Message);
            }

        }

        public SqlDataReader ExecuteReader(string sql)
        {
            try
            {
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet DataSet(string sql)
        {
            DataSet DS = new DataSet();
            var SqlCommnad = new SqlCommand(sql, conn);
            var DA = new SqlDataAdapter(SqlCommnad);
            try
            {
                DA.Fill(DS);
                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Close()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}