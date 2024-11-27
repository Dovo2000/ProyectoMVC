using Microsoft.Data.SqlClient;
using ProyectoMVC.Models;

namespace ProyectoMVC.DAL
{
    public class TipoAnimalDAL
    {
        string _connectionString = "Data Source=85.208.21.117,54321;" +
            "Initial Catalog=DavidAnimales;" +
            "User ID=sa;" +
            "Password=Sql#123456789;" +
            "TrustServerCertificate=True;";

        public TipoAnimalDAL() { }

        // Obtener todos los animales
        public List<TipoAnimal> GetAll()
        {
            var tiposAnimales = new List<TipoAnimal>();
            

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TipoAnimal";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoAnimal tipoAnimal = new TipoAnimal(
                            idTipoAnimal: Convert.ToInt32(reader["IdTipoAnimal"]),
                            tipoDescripcion: new string(Convert.ToString(reader["TipoDescripcion"]))
                        );

                        tiposAnimales.Add(tipoAnimal);
                    }
                }
            }

            return tiposAnimales;
        }

        public TipoAnimal? GetById(int id)
        {
            TipoAnimal? tipoAnimal = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM TipoAnimal WHERE IdTipoAnimal = @IdTipoAnimal";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdTipoAnimal", id);

                conn.Open();

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

        // Insertar un nuevo animal
        public void Insert(TipoAnimal tipoAnimal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Animal (TipoDescripcion) " +
                                    "VALUES (@TipoDescripcion)";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TipoDescripcion", tipoAnimal.TipoDescripcion);

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
        public void Update(TipoAnimal tipoAnimal)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE TipoAnimal SET TipoDescripcion = @TipoDescripcion WHERE IdTipoAnimal = @IdTipoAnimal";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdTipoAnimal", tipoAnimal.IdTipoAnimal);
                    cmd.Parameters.AddWithValue("@TipoDescripcion", tipoAnimal.TipoDescripcion);

                    conn.Open();
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
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM TipoAnimal WHERE IdTipoAnimal = @IdTipoAnimal";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdTipoAnimal", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
