using AspNetCoreHero.ToastNotification.Abstractions;
using La_mia_pizzeria_refactoring.Data;
using La_mia_pizzeria_refactoring.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace La_mia_pizzeria_refactoring.Controllers
{
    [Authorize]
    public class IngredientController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;
        private readonly INotyfService _toastNotification;

      
        public IngredientController(ILogger<HomeController> logger, AppDbContext db, INotyfService toastNotification)
        {
            _logger = logger;
            _db = db;
            _toastNotification = toastNotification;
        }
        // GET: IngredientController
        public ActionResult Index()
        {
            return View();
        }

        // GET: IngredientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: IngredientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Ingredient.Name))
            {
                _toastNotification.Error("Impossibile creare Allergene!L'allergene necessita di un nome.");
                return RedirectToAction("Create", "Pizza", viewModel);
            }

            if (_db.Categories.Where(x => x.Name.ToLower() == viewModel.Ingredient.Name.ToLower()).Count() > 0)
            {
                _toastNotification.Warning($"{viewModel.Ingredient.Name} é gia esistente");
                return RedirectToAction("Create", "Pizza", viewModel);
            }

            if (string.IsNullOrEmpty(viewModel.Ingredient.Icon))
            {
                _toastNotification.Error("Impossibile creare Allergene! L'allergene necessita di un Icona.");
                return RedirectToAction("Create", "Pizza", viewModel);
            }
            if (!viewModel.Ingredient.Icon.Contains("<i class="))
            {
                _toastNotification.Error("Impossibile creare Allergene! Seleziona l'icona dal sito linkato");
                return RedirectToAction("Create", "Pizza", viewModel);
            }


            _toastNotification.Success($"{viewModel.Ingredient.Name} aggiunto con successo");
            _db.Ingredients.Add(viewModel.Ingredient);
            _db.SaveChanges();

            viewModel.Ingredients = _db.Ingredients.ToList();
            viewModel.Categories = _db.Categories.ToList();

            return RedirectToAction("Create", "Pizza", viewModel);
        }

        // GET: IngredientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IngredientController/Edit/5
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

        // GET: IngredientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IngredientController/Delete/5
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
    }
}
