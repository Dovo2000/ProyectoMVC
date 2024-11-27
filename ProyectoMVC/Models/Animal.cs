using System.ComponentModel.DataAnnotations;

namespace ProyectoMVC.Models
{
    public class Animal
    {
        [Key]
        public int IdAnimal { get; set; }

        [Required(ErrorMessage = "El nombre del animal es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 carácteres.")]
        public string NombreAnimal { get; set; }

        [StringLength(50, ErrorMessage = "La raza no puede superar los 50 carácteres.")]
        public string? Raza {  get; set; }

        [Required(ErrorMessage = "IDTipoAnimal es obligatorio.")]
        public int RIdTipoAnimal { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Introduzca una fecha válida.")]
        public DateTime? FechaNacimiento { get; set; }

        // Navegación hacia TipoAnimal
        public TipoAnimal TipoAnimal { get; set; }

        public Animal()
        {
            IdAnimal = 0;
            NombreAnimal = string.Empty;
            Raza = string.Empty;
            RIdTipoAnimal = 0;
            FechaNacimiento = DateTime.MinValue;
            TipoAnimal = new TipoAnimal();
        }

        public Animal(string nombreAnimal, string? raza, int rIdTipoAnimal, DateTime? fechaNacimiento, TipoAnimal tipoAnimal)
        {
            IdAnimal = 0;
            NombreAnimal = nombreAnimal;
            Raza = raza;
            RIdTipoAnimal = rIdTipoAnimal;
            FechaNacimiento = fechaNacimiento;
            TipoAnimal = tipoAnimal;
        }

        public Animal(int idAnimal, string nombreAnimal, string? raza, int rIdTipoAnimal, DateTime? fechaNacimiento, TipoAnimal tipoAnimal)
        {
            IdAnimal = idAnimal;
            NombreAnimal = nombreAnimal;
            Raza = raza;
            RIdTipoAnimal = rIdTipoAnimal;
            FechaNacimiento = fechaNacimiento;
            TipoAnimal = tipoAnimal;
        }
    }
}
