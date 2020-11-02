using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ApolloQA.Source.Helpers
{
    class SQL
    {

        public static List<Dictionary<String, dynamic>> executeQuery(String query)
        {
            return executeQuery(query, ("",""));
        }
        public static List<Dictionary<String, dynamic>> executeQuery(String query, params (string key, dynamic value)[] parameters)
        {

            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SQL_CONNECTIONSTRING")))
            {
                SqlCommand command = new SqlCommand(query, connection);


                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.key, parameter.value);
                }
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    List<Dictionary<String, dynamic>> results = new List<Dictionary<string, dynamic>>();
                    while (reader.Read())
                    {
                        results.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(keyIndex => reader.GetName(keyIndex), valueIndex => reader.GetValue(valueIndex)));

                    }
                    return results;
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }
    }
}
