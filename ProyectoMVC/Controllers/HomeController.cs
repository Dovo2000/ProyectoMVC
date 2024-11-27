using Microsoft.AspNetCore.Mvc;
using ProyectoMVC.Models;
using ProyectoMVC.Models.ViewModels;
using ProyectoMVC.DAL;
using System.Diagnostics;

namespace ProyectoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            AnimalDAL animalDAL = new AnimalDAL();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();
            AnimalViewModel viewModel = new AnimalViewModel();

            viewModel.Animals = animalDAL.GetAll();
            viewModel.TiposAnimales = tipoAnimalDAL.GetAll();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult UpdateList(string animalName, string razaAnimal, DateTime fechaNacimientoAnimal, int idTipoAnimal, int? id)
        {
            AnimalViewModel viewModel = new AnimalViewModel();
            AnimalDAL animalDAL = new AnimalDAL();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();

            if (id == null)
            {
                Animal animal = new Animal(animalName, razaAnimal, idTipoAnimal, fechaNacimientoAnimal, tipoAnimalDAL.GetById(idTipoAnimal) ?? new TipoAnimal());

                animalDAL.Insert(animal);
            }
            else
            {
                Animal animal = new Animal((int) id, animalName, razaAnimal, idTipoAnimal, fechaNacimientoAnimal, tipoAnimalDAL.GetById(idTipoAnimal) ?? new TipoAnimal());

                animalDAL.Update(animal);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            AnimalViewModel viewModel = new AnimalViewModel();
            
            AnimalDAL animalDAL = new AnimalDAL();
            animalDAL.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
