using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoMVC.DAL;
using ProyectoMVC.Models;
using ProyectoMVC.Models.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoMVC.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            return View();
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

            return RedirectToAction("Index", "Home");
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
