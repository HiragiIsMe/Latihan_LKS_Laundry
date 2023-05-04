using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using XSystem.Security.Cryptography;

namespace Latian_LKS_Laundry
{
    class Utils
    {
        public static string conn = @"Data Source=DESKTOP-HUJGH1E\SQLEXPRESS;Initial Catalog=Latian_LKS_Laundry;Integrated Security=True";
    }

    class Model
    {
        public static int id { set; get; }
        public static string Name { set; get; }
    }
    class Command
    {
        static SqlConnection connection = new SqlConnection(Utils.conn);

        public static void NonQuery(string command)
        {
            SqlCommand cmd = new SqlCommand(command,connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        } 

        public static DataTable getData(string command)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }
    }

    class Encrypt
    {
        public static string Pass(string pass)
        {
            using(SHA256Managed managed = new SHA256Managed())
            {
                byte[] encode = managed.ComputeHash(Encoding.UTF8.GetBytes(pass));
                string getPass = Convert.ToBase64String(encode);

                return getPass;
            }
        }
    }

    class TransModel
    {
        public static int price { set; get; }
        public static int discount { set; get; }
        public static DateTime transDate { set; get; }
    }
}
//#F8FCEB
//#495664
//#333C4A
