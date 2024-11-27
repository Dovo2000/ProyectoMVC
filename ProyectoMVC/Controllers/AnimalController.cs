using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoMVC.DAL;
using ProyectoMVC.Models;
using ProyectoMVC.Models.ViewModels;

namespace ProyectoMVC.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet]
        public ActionResult Crear()
        {
            TipoAnimalDAL dal = new TipoAnimalDAL();

            List<TipoAnimal> tiposDeAnimal = dal.GetAll();

            ViewBag.TiposAnimales = tiposDeAnimal.Select(t => new SelectListItem
            {
                Value = t.IdTipoAnimal.ToString(),
                Text = t.TipoDescripcion
            });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(CrearAnimalViewModel model)
        {
            if(ModelState.IsValid)
            {
                AnimalDAL animalDAL = new AnimalDAL();
                Animal animal = new Animal();

                animal.NombreAnimal = model.NombreAnimal;
                animal.RIdTipoAnimal = model.RIdTipoAnimal;
                animal.Raza = model.Raza;
                animal.FechaNacimiento = model.FechaNacimiento;

                animalDAL.Insert(animal);

                TempData["Success"] = "Se ha creado el animal.";

                return RedirectToAction("Index", "Home");
            }

            TipoAnimalDAL dal = new TipoAnimalDAL();
            List<TipoAnimal> tiposAnimales = new List<TipoAnimal>();

            ViewBag.TiposAnimales = tiposAnimales.Select(t => new SelectListItem
            {
                Value = t.IdTipoAnimal.ToString(),
                Text = t.TipoDescripcion
            });

            ViewBag.Error = "No se ha podido crear el animal";

            return View(model);
        }

        [HttpGet]
        public IActionResult Detail()
        {
            if (TempData["Animal"] != null)
            {
                var json = TempData["Animal"] as string;
                var vm = JsonConvert.DeserializeObject<DetailAnimalViewModel>(json);

                return View(vm);
            }

            return RedirectToAction("Lista", "Home");
        }

        [HttpPost]
        public IActionResult AnimalDetail(int id)
        {
            AnimalDAL dal = new AnimalDAL();
            TipoAnimalDAL tipoAnimalDAL = new TipoAnimalDAL();
            DetailAnimalViewModel vm = new DetailAnimalViewModel();

            vm.AnimalDetail = dal.GetById(id);
            vm.AnimalDetail.TipoAnimal = tipoAnimalDAL.GetById(vm.AnimalDetail.RIdTipoAnimal);

            TempData["Animal"] = JsonConvert.SerializeObject(vm);

            return RedirectToAction("Detail", "Animal");
        }
    }
}
