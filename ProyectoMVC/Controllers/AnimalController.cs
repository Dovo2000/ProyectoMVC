using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoMVC.DAL;
using ProyectoMVC.Models;
using ProyectoMVC.Models.ViewModels;
using System.Windows.Markup;

namespace ProyectoMVC.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet]
        public ActionResult Crear()
        {
            TipoAnimalDAL dal = new TipoAnimalDAL();

            List<TipoAnimal> tiposDeAnimal = dal.GetAll();

            ViewBag.TiposDeAnimal = tiposDeAnimal.Select(t => new SelectListItem
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
