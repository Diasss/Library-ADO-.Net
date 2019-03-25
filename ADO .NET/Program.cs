using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET
{
    class Program
    {

        SqlConnection conn = null;
        public Program() {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = (localdb)\v11.0; Initial Catalog = Library; Integrated Security = SSPI;";
                }
        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.ReadData();
        }
        public void InsertQuery()
        {
            try
            {
                conn.Open();
                string insertString = @"insert into Authors (FirstName, LastName) values('Ass', 'Lan')";
                SqlCommand cmd = new SqlCommand(insertString, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void ReadData()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Authors", conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Console.WriteLine(rdr[1] + " " + rdr[2]);
                }
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void ReadData1()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Authors", conn);
                rdr = cmd.ExecuteReader();
                int line = 0;
                while (rdr.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            Console.WriteLine(rdr.GetName(i).ToString() + " ");
                        }
                    }
                    Console.WriteLine();
                    line++;
                    Console.WriteLine(rdr[0] + " " + rdr[1] + " " + rdr[2]);

                }
                Console.WriteLine("Обработао записей: " + line.ToString());
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void ReadData2()
        {
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Authors; select * from Books", conn);
                rdr = cmd.ExecuteReader();
                int line = 0;
                do
                {
                    while (rdr.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                Console.WriteLine(rdr.GetName(i).ToString() + "\t");
                            }
                            Console.WriteLine();
                        }
                        line++;
                        Console.WriteLine(rdr[0] + "\t" + rdr[1] + "\t" + rdr[2]);
                    }
                    Console.WriteLine("Всего обработано записей: " + line.ToString());
                } while (rdr.NextResult());
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public void ExecStoredProcedire()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("getBooksNumber", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AuthorId", SqlDbType.Int).Value = 2;
            SqlParameter outputParam = new SqlParameter("@BookCount", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParam);
            cmd.ExecuteNonQuery();
            Console.WriteLine(cmd.Parameters["@BookCount"].
            Value.ToString());
        }
    }
}














































































































































