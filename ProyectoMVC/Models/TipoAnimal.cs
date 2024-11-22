namespace ProyectoMVC.Models
{
    public class TipoAnimal
    {
        public int IdTipoAnimal { get; set; }
        public required string TipoDescripcion { get; set; }

        TipoAnimal(int idTipoAnimal, string tipoDescripcion)
        {
            IdTipoAnimal = idTipoAnimal;
            TipoDescripcion = tipoDescripcion;
        }
    }
}
