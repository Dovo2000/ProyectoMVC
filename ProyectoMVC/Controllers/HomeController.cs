using Microsoft.AspNetCore.Mvc;
using ProyectoMVC.Models;
using ProyectoMVC.Models.ViewModels;
using ProyectoMVC.DAL;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;

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

        public IActionResult UpdateList(string animalName, string razaAnimal, DateTime fechaNacimientoAnimal, int idTipoAnimal, string submitAction)
        {
            AnimalViewModel viewModel = new AnimalViewModel();
            AnimalDAL animalDAL = new AnimalDAL();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();

            if(submitAction == "Add")
            {
                Animal animal = new Animal(animalName, razaAnimal, idTipoAnimal, fechaNacimientoAnimal, tipoAnimalDAL.GetById(idTipoAnimal) ?? new TipoAnimal());

                animalDAL.Add(animal);
            }
            else if (submitAction == "Update")
            {
                if (ViewData["UpdateTarget"] != null)
                {
                    Animal animal = new Animal(Convert.ToInt32(ViewData["UpdateTarget"]), animalName, razaAnimal, idTipoAnimal, fechaNacimientoAnimal, tipoAnimalDAL.GetById(idTipoAnimal) ?? new TipoAnimal());
                
                    animalDAL.Update(animal);
                }
            }

            ViewData["UpdateTarget"] = null;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            AnimalViewModel viewModel = new AnimalViewModel();
            
            AnimalDAL animalDAL = new AnimalDAL();
            animalDAL.Delete(id);

            ViewData["UpdateTarget"] = null;

            return RedirectToAction("Index");
        }

        public ActionResult ChangeTarget(int id)
        {
            ViewData["UpdateTarget"] = id;

            return RedirectToAction("Index");
        }
    }
}
