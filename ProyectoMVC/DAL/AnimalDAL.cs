using Microsoft.Data.SqlClient;
using ProyectoMVC.Models;

namespace ProyectoMVC.DAL
{
    public class AnimalDAL
    {
        private string _connectionString = "Data Source=85.208.21.117,54321;" +
            "Initial Catalog=DavidAnimales;" +
            "User ID=sa;" +
            "Password=Sql#123456789;" +
            "TrustServerCertificate=True;";

        public AnimalDAL(){ }

        // Obtener todos los animales
        public List<Animal> GetAll()
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
                        Animal animal = new Animal(   
                            idAnimal: Convert.ToInt32(reader["IdAnimal"]),
                            nombreAnimal: new string(Convert.ToString(reader["NombreAnimal"])),
                            raza: Convert.IsDBNull(reader["Raza"]) ? null : reader["Raza"].ToString(),
                            rIdTipoAnimal: Convert.ToInt32(reader["RIdTipoAnimal"]),
                            fechaNacimiento: Convert.IsDBNull(reader["FechaNacimiento"]) ? null : Convert.ToDateTime(reader["FechaNacimiento"]),
                            tipoAnimal: tipoAnimalDAL.GetById(Convert.ToInt32(reader["RIdTipoAnimal"])) ?? new TipoAnimal()
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

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdAnimal", id);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        animal = new Animal(
                            idAnimal: Convert.ToInt32(reader["IdAnimal"]),
                            nombreAnimal: new string(Convert.ToString(reader["NombreAnimal"])),
                            raza: Convert.IsDBNull(reader["Raza"]) ? null : reader["Raza"].ToString(),
                            rIdTipoAnimal: Convert.ToInt32(reader["RIdTipoAnimal"]),
                            fechaNacimiento: Convert.IsDBNull(reader["FechaNacimiento"]) ? null : Convert.ToDateTime(reader["FechaNacimiento"]),
                            tipoAnimal: new TipoAnimal() // TipoAnimal se puede cargar en una consulta separada
                        );
                    }
                }
            }

            return animal;
        }

        // Insertar un nuevo animal
        public void Insert(Animal animal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) " +
                                    "VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento)";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                    cmd.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento ?? (object)DBNull.Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Actualizar un animal existente
        public void Update(Animal animal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Animal SET NombreAnimal = @NombreAnimal, Raza = @Raza, " +
                                    "RIdTipoAnimal = @RIdTipoAnimal, FechaNacimiento = @FechaNacimiento WHERE IdAnimal = @IdAnimal";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                    cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                    cmd.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento ?? (object)DBNull.Value);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // Eliminar un animal por su Id
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdAnimal", id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
