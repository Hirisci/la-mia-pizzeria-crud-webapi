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

            if (!ModelState.IsValid)
            {
                viewModel.Ingredients = _db.Ingredients.ToList();
                viewModel.Categories = _db.Categories.ToList();
                return View(viewModel);
            }

            viewModel.Pizza.Ingredients = _db.Ingredients.Where(x => viewModel.SelectedIngredients.Contains(x.Id)).ToList();
            _db.Pizzas.Add(viewModel.Pizza);
            _db.SaveChanges();
            _toastNotification.Success($"{viewModel.Pizza.Name} aggiunta con successo!");
            
            return RedirectToAction("Create");
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {

            Pizza? Pizza = _db.Pizzas.Include("Ingredients").Include("Category").FirstOrDefault(x => x.Id == id);

            if (Pizza == null)
            {
                _toastNotification.Error("Errore Interno! Impossibile cancellare la pizza selezionata");
                return RedirectToAction("Index");
            }

            PizzaViewModel viewModel = new PizzaViewModel();
            viewModel.Pizza = Pizza;
            foreach(var ing in Pizza.Ingredients)
            {
                viewModel.SelectedIngredients.ToList().Add(ing.Id);
            }
            viewModel.Ingredients = _db.Ingredients.ToList();
            viewModel.Categories = _db.Categories.ToList();

            return View(viewModel);
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PizzaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Ingredients = _db.Ingredients.ToList();
                viewModel.Categories = _db.Categories.ToList();
                return View(viewModel);
            }

            if (viewModel.Pizza.Id == null)
            {
                _toastNotification.Error("Errore Interno! Impossibile modificare la pizza selezionata");
                return RedirectToAction(nameof(Index));
            }

            
            _db.Update(viewModel.Pizza);
            _db.SaveChanges();
            _toastNotification.Success($"{viewModel.Pizza.Name} é ora visibile nel Menu");
            return RedirectToAction(nameof(Index));
        }

        // Post: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Visible(int id)
        {
            Pizza? Pizza = _db.Pizzas.FirstOrDefault(x => x.Id == id);

            if (Pizza == null)
            {
                _toastNotification.Error("Errore Interno! Impossibile cancellare la pizza selezionata");
                return RedirectToAction(nameof(Index));
            }

            Pizza.IsVisible = !Pizza.IsVisible;
            _db.Update(Pizza);
            _db.SaveChanges();

            if (Pizza.IsVisible)
            {
                _toastNotification.Success($"{Pizza.Name} é ora visibile nel Menu");
            }
            else
            {
                _toastNotification.Success($"{Pizza.Name} non é ora visibile nel Menu");
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Pizza? Pizza = _db.Pizzas.FirstOrDefault(x => x.Id == id);

            if (Pizza == null)
            {
                _toastNotification.Error("Errore Interno! Impossibile cancellare la pizza selezionata");
                return RedirectToAction(nameof(Index));
            }

            _db.Pizzas.Remove(Pizza);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
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
