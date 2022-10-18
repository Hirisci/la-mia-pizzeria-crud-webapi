using AspNetCoreHero.ToastNotification.Abstractions;
using La_mia_pizzeria_refactoring.Data;
using La_mia_pizzeria_refactoring.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace La_mia_pizzeria_refactoring.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;
        private readonly INotyfService _toastNotification;

        public CategoryController(ILogger<HomeController> logger, AppDbContext db, INotyfService toastNotification)
        {
            _logger = logger;
            _db = db;
            _toastNotification = toastNotification;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Category.Name))
            {
                _toastNotification.Error("Impossibile creare Categoria! La categoria necessita di un nome.");
                return RedirectToAction("Create","Pizza", viewModel);
            }

            if (_db.Categories.Where(x=> x.Name.ToLower() == viewModel.Category.Name.ToLower()).Count() > 0)
            {
                _toastNotification.Warning($"{viewModel.Category.Name} é gia esistente");
                return RedirectToAction("Create", "Pizza", viewModel);
            }

            _toastNotification.Success($"{viewModel.Category.Name} aggiunta con successo");
            _db.Categories.Add(viewModel.Category);
            _db.SaveChanges();

            viewModel.Ingredients = SetViewIngredient(viewModel.SelectedIngredients);
            viewModel.Categories = SetViewCategory(viewModel.Pizza.CategoryId);

            return RedirectToAction("Create", "Pizza", viewModel);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
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

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
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

            //public void OnGet()
            //{
            //    _toastNotification.Success("A success for christian-schou.dk");
            //    _toastNotification.Information("Here is an info toast - closes in 6 seconds.", 6);
            //    _toastNotification.Warning("Be aware, here is a warning toast.");
            //    _toastNotification.Error("Ouch - An error occured. This message closes in 4 seconds.", 4);
            //}
        }

        private List<SelectListItem> SetViewIngredient(IEnumerable<int> ingredients)
        {

            List<SelectListItem> selectListItems = _db.Ingredients.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.Id.ToString(),
                                              Text = a.Name,
                                              Selected = ingredients.Contains(a.Id)
                                          }).ToList();
            return selectListItems;
        }
        private List<SelectListItem> SetViewCategory(int CategoryId)
        {

            List<SelectListItem> selectListItems = _db.Categories.Select(a =>
                                          new SelectListItem
                                          {
                                              Value = a.Id.ToString(),
                                              Text = a.Name,
                                              Selected = a.Id == CategoryId,
                                          }).ToList();
            return selectListItems;
        }
    }
}
