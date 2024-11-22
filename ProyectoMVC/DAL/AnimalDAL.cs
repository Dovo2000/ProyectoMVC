using Microsoft.Data.SqlClient;
using ProyectoMVC.Models;

namespace ProyectoMVC.DAL
{
    public class AnimalDAL
    {
        private string _connectionString = "";
        public AnimalDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Obtener todos los animales
        public List<Animal> GetAllAsync()
        {
            var animals = new List<Animal>();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Animal";

                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var animal = new Animal
                        (
                            idAnimal: Convert.ToInt32(reader["IdAnimal"]),
                            nombreAnimal: reader["NombreAnimal"].ToString(),
                            raza: Convert.IsDBNull(reader["Raza"]) ? null : reader["Raza"].ToString(),
                            rIdTipoAnimal: Convert.ToInt32(reader["RIdTipoAnimal"]),
                            fechaNacimiento: Convert.IsDBNull(reader["FechaNacimiento"]) ? (DateTime?)null : Convert.ToDateTime(reader["FechaNacimiento"]),
                            tipoAnimal: tipoAnimalDAL.GetById(Convert.ToInt32(reader["RIdTipoAnimal"]))
                        );

                        animals.Add(animal);
                    }
                }
            }

            return animals;
        }

        // Obtener un animal por su Id
        public Animal? GetById(int id)
        {
            Animal? animal = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Animal WHERE IdAnimal = @IdAnimal";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdAnimal", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        animal = new Animal(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.IsDBNull(2) ? null : reader.GetString(2),
                            reader.GetInt32(3),
                            reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            null // TipoAnimal se puede cargar en una consulta separada
                        );
                    }
                }
            }

            return animal;
        }

        // Insertar un nuevo animal
        public void Add(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) " +
                                "VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                command.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                command.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Actualizar un animal existente
        public async Task UpdateAsync(Animal animal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Animal SET NombreAnimal = @NombreAnimal, Raza = @Raza, " +
                                "RIdTipoAnimal = @RIdTipoAnimal, FechaNacimiento = @FechaNacimiento WHERE IdAnimal = @IdAnimal";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                command.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                command.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                command.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento ?? (object)DBNull.Value);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        // Eliminar un animal por su Id
        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdAnimal", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
