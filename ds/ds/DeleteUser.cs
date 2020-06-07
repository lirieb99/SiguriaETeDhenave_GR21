using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace ds
{
    public class DeleteUser
    {
        DatabaseConnection DB = new DatabaseConnection();

        public void DeleteRsaKey(string privateKeyPath, string publicKeyPath)
        {
            File.Delete(privateKeyPath);
            File.Delete(publicKeyPath);
        }

        public void DeleteRsaKey(string keyPath)
        {
            File.Delete(keyPath);
        }

        public void DeletefromDB(string username)
        {
            try
            {

                String query = "DELETE FROM users WHERE USERNAME=" + "'" + username + "';";

                DB.Open();
                SqlDataReader row;
                row = DB.ExecuteReader(query);
                Console.WriteLine("Eshte fshire shfrytezuesi " + username);
                DB.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}