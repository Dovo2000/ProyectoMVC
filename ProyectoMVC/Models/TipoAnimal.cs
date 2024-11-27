
namespace ProyectoMVC.Models
{
    public class TipoAnimal
    {
        public int IdTipoAnimal { get; set; }

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
