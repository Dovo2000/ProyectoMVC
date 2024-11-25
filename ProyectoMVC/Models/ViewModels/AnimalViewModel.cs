namespace ProyectoMVC.Models.ViewModels
{
    public class AnimalViewModel
    {
        public List<Animal> Animals { get; set; }
        public List<TipoAnimal> TiposAnimales { get; set; }

        public AnimalViewModel() 
        {
            Animals = new List<Animal>();
            TiposAnimales = new List<TipoAnimal>();
        }
    }
}
