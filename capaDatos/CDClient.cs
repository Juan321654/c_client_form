using capaEntidad;
using MySql.Data.MySqlClient;
using System.Data;

namespace capaDatos
{
    public class CDClient
    {
        string connectionSettings = "Server=localhost;User=juan;Password=password;Port=3306;database=curso_cs;";

        public void checkConnection()
        {
            // Encapsulating the connection in a using statement
            using (MySqlConnection connection = new MySqlConnection(connectionSettings))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Connected");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: " + ex.Message);
                    throw;
                }
                // No need to explicitly call connection.Close() here as it's handled by the 'using' statement
            }
        }

        public void Create(CEClients cE)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionSettings))
            {
                connection.Open();
                string query = "INSERT INTO `clients` (`name`, `last_name`, `photo_path`) VALUES (@name, @lastName, @photoPath);";
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    mySqlCommand.Parameters.AddWithValue("@name", cE.Name);
                    mySqlCommand.Parameters.AddWithValue("@lastName", cE.LastName);
                    mySqlCommand.Parameters.AddWithValue("@photoPath", cE.PhotoPath);

                    mySqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Client added successfully.");
                }
            }
        }

        public DataSet GetList()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionSettings))
            {
                connection.Open();
                string query = "SELECT * FROM `clients`;";
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        DataSet dataSet = new DataSet();
                        mySqlDataAdapter.Fill(dataSet, "clientsTable"); // clientsTable is the name we give as a reference, could be anything
                        return dataSet;
                    }
                }
            }
        }

        public void Update(CEClients cE)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionSettings))
            {
                connection.Open();
                string query = "UPDATE `clients` SET `name` = @name, `last_name` = @lastName, `photo_path` = @photoPath WHERE `id` = @id;";
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                {
                    mySqlCommand.Parameters.AddWithValue("@name", cE.Name);
                    mySqlCommand.Parameters.AddWithValue("@lastName", cE.LastName);
                    mySqlCommand.Parameters.AddWithValue("@photoPath", cE.PhotoPath);
                    mySqlCommand.Parameters.AddWithValue("@id", cE.Id);

                    mySqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Client updated successfully.");
                }
            }
        }

        public void Delete(CEClients cE)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionSettings))
            {
                connection.Open();
                string query = "DELETE FROM `clients` WHERE `id` = @id;";
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, connection))
                {
                    mySqlCommand.Parameters.AddWithValue("@id", cE.Id);

                    mySqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Client deleted successfully.");
                }
            }
        }
    }
}