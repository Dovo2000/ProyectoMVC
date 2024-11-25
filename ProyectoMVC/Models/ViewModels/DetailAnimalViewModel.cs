using ProyectoMVC.DAL;

namespace ProyectoMVC.Models.ViewModels
{
    public class DetailAnimalViewModel
    {
        public Animal AnimalDetail { get; set; } = new Animal();

        public DetailAnimalViewModel(int id)
        {
            AnimalDAL animalDAL = new AnimalDAL();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();
            AnimalDetail = animalDAL.GetById(id) ?? new Animal();
            AnimalDetail.TipoAnimal = tipoAnimalDAL.GetById(AnimalDetail.RIdTipoAnimal) ?? new TipoAnimal();
        }
    }
}
