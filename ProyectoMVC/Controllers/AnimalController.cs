using Microsoft.AspNetCore.Mvc;
using ProyectoMVC.DAL;
using ProyectoMVC.Models;
using ProyectoMVC.Models.ViewModels;

namespace ProyectoMVC.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            DetailAnimalViewModel viewModel = new DetailAnimalViewModel(id);

            return View(viewModel);
        }
    }
}
