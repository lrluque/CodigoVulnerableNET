 SqlCommand command = new SqlCommand("SELECT * FROM TableName WHERE ColumnName = @param", connection);
 command.Parameters.AddWithValue("@param", userInput);
using System.Data.SqlClient;

class InsecureDatabaseAccess
{
    static void Main(string[] args)
    {
        Console.WriteLine("Introduce el nombre del empleado para buscar sus detalles:");
        string input = Console.ReadLine();

        string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        string query = $"SELECT * FROM Employees WHERE Name = '{input}'";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron empleados.");
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al acceder a la base de datos: " + ex.Message);
        }
    }
}
