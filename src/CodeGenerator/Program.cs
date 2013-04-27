using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            string directory = string.Empty;
            string server = string.Empty;
            string database = string.Empty;
            string userName = string.Empty;
            string password = string.Empty;

            foreach (string s in args)
            {
                if (s.Contains("/dir:"))
                    directory = s.Substring(5);
                else if (s.Contains("/s:"))
                    server = s.Substring(3);
                else if (s.Contains("/d:"))
                    database = s.Substring(3);
                else if (s.Contains("/u:"))
                    userName = s.Substring(3);
                else if (s.Contains("/p:"))
                    password = s.Substring(3);
            }

            string connectionstring = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3}", server, database, userName, password);

            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(connectionstring))
            {
                cn.Open();
                dt = cn.GetSchema("Tables");

                foreach (DataRow row in dt.Rows)
                {
                    DataTable columns = new DataTable();
                    string[] restrictions = new string[4]{ database, null, row[2].ToString(), null };
                    columns = cn.GetSchema("Columns", restrictions);

                    Dictionary<string, string> col = new Dictionary<string, string>();

                    foreach (DataRow row2 in columns.Rows)
                    {
                        col.Add(row2[3].ToString(), row2[7].ToString());
                    }

                    ClassMaker.MakeClass(directory, row[2].ToString(), col);
                }
            }
        }
    }
}
