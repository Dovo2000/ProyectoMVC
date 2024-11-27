using System.ComponentModel.DataAnnotations;

namespace ProyectoMVC.Models.ViewModels
{
    public class CrearAnimalViewModel
    {
        [Required(ErrorMessage = "El nombre del animal es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del animal no puede superar los 50 caracteres.")]
        [Display(Name = "Nombre del animal")]
        public string NombreAnimal { get; set; }

        [StringLength(50, ErrorMessage = "La raza del animal no puede superar los 50 caracteres.")]
        [Display(Name = "Raza")]
        public string Raza { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de animal.")]
        [Display(Name = "Tipo de animal")]
        public int RIdTipoAnimal { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "Debe ingresar una fecha válida.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaNacimiento { get; set; }
    }
}
