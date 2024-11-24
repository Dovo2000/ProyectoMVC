using Microsoft.Data.SqlClient;
using ProyectoMVC.Models;

namespace ProyectoMVC.DAL
{
    public class TipoAnimalDAL
    {
        string _connectionString = "";

        public TipoAnimalDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TipoAnimal? GetById(int id)
        {
            TipoAnimal? tipoAnimal = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Animal WHERE IdAnimal = @IdAnimal";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdAnimal", id);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tipoAnimal = new TipoAnimal(
                            idTipoAnimal: Convert.ToInt32(reader["IdTipoAnimal"]), 
                            tipoDescripcion: new string(Convert.ToString(reader["TipoDescripcion"]))
                        );
                    }
                }
            }

            return tipoAnimal;
        }
    }
}
