using Microsoft.AspNetCore.Mvc;
using ProyectoMVC.Models;
using ProyectoMVC.Models.ViewModels;
using ProyectoMVC.DAL;
using System.Diagnostics;
using System.Net.NetworkInformation;

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

        public IActionResult AddAnimal(string animalName, string razaAnimal, DateTime fechaNacimientoAnimal, int idTipoAnimal)
        {
            AnimalViewModel viewModel = new AnimalViewModel();

            AnimalDAL animalDAL = new AnimalDAL();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();
            Animal animal = new Animal(animalName, razaAnimal, idTipoAnimal, fechaNacimientoAnimal, tipoAnimalDAL.GetById(idTipoAnimal) ?? new TipoAnimal());

            animalDAL.Add(animal);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            AnimalViewModel viewModel = new AnimalViewModel();
            
            AnimalDAL animalDAL = new AnimalDAL();
            animalDAL.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
