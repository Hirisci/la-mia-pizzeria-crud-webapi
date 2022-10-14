using AspNetCoreHero.ToastNotification.Abstractions;
using La_mia_pizzeria_refactoring.Data;
using La_mia_pizzeria_refactoring.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace La_mia_pizzeria_refactoring.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;
        private readonly INotyfService _toastNotification;

        public PizzaController(ILogger<HomeController> logger, AppDbContext db, INotyfService toastNotification)
        {
            _logger = logger;
            _db = db;
            _toastNotification = toastNotification;
        }

        // GET: PizzaController
        public ActionResult Index()
        {
            List<Pizza> List = _db.Pizzas.Include("Ingredients").Include("Category").ToList();
            return View(List);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {
            PizzaViewModel viewModel = new PizzaViewModel();
            viewModel.Ingredients = _db.Ingredients.ToList();
            viewModel.Categories = _db.Categories.ToList();
            return View(viewModel);
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaViewModel viewModel)
        {
            //ModelState.Remove("Category.Name");
            //ModelState.Remove("Category.Pizzas");
            //ModelState.Remove("Ingredient.Icon");
            //ModelState.Remove("Ingredient.Name");
            //ModelState.Remove("Ingredient.Color");
            //ModelState.Remove("Ingredient.Pizzas");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (_db.Pizzas.Where(x => x.Name.ToLower() == viewModel.Pizza.Name.ToLower()).Count() > 0)
            {
                _toastNotification.Warning($"{viewModel.Pizza.Name} é gia esistente");
                return RedirectToAction("Create", "Pizza", viewModel);
            }

            _db.Pizzas.Add(viewModel.Pizza);
            _db.SaveChanges();
            _toastNotification.Success($"{viewModel.Pizza.Name} aggiunta con successo!");
            
            return RedirectToAction("Create");
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //public void OnGet()
        //{
        //    _toastNotification.Success("A success for christian-schou.dk");
        //    _toastNotification.Information("Here is an info toast - closes in 6 seconds.", 6);
        //    _toastNotification.Warning("Be aware, here is a warning toast.");
        //    _toastNotification.Error("Ouch - An error occured. This message closes in 4 seconds.", 4);
        //}
    }
}
