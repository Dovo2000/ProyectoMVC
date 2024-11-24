﻿using Microsoft.Data.SqlClient;
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
        public List<Animal> GetAll()
        {
            var animals = new List<Animal>();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL(_connectionString);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Animal";

                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var animal = new Animal(   
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
        public void Add(Animal animal)
        {
            try
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

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                    command.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                    command.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                    command.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento ?? (object)DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
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

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdAnimal", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}