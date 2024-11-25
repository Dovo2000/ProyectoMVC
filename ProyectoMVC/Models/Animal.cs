namespace ProyectoMVC.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        public string NombreAnimal { get; set; }
        public string? Raza {  get; set; }
        public int RIdTipoAnimal { get; set; }
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
