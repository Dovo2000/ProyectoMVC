using System.ComponentModel.DataAnnotations;

namespace ProyectoMVC.Models
{
    public class TipoAnimal
    {
        [Key]
        public int IdTipoAnimal { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La descripción no puede superar los 50 carácteres.")]
        public string TipoDescripcion { get; set; }

        public TipoAnimal() 
        {
            IdTipoAnimal = 0;
            TipoDescripcion = string.Empty;
        }

        public TipoAnimal(int idTipoAnimal, string tipoDescripcion)
        {
            IdTipoAnimal = idTipoAnimal;
            TipoDescripcion = tipoDescripcion;
        }
    }
}
