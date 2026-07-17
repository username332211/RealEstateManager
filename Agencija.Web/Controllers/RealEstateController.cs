using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agencija.DAL;
using Agencija.Model;
using Agencija.Web.Models;

namespace Agencija.Web.Controllers
{
	public class RealEstateController(
        RealEstateManagerDbContext _dbContext) : Controller
    {
        [Route("popis-nekretnina")]
        public IActionResult Index()
        {
            var estateQuery = _dbContext.RealEstates.AsQueryable();
			var model = estateQuery.ToList();

            return View(model);
        }

		[HttpPost]
        public IActionResult IndexAjax(RealEstateFilterModel filter = null)
        {
			filter ??= new RealEstateFilterModel();

			var estateQuery = _dbContext.RealEstates.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.Name))
                estateQuery = estateQuery.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Address))
                estateQuery = estateQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Interior))
                estateQuery = estateQuery.Where(p => p.InteriorSize.ToString().ToLower().Contains(filter.Interior.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Price))
                estateQuery = estateQuery.Where(p => p.Price.ToString().ToLower().Contains(filter.Price.ToLower()));

            var model = estateQuery.ToList();
            return PartialView("_IndexTable", model);
        }

        [Authorize]
        public IActionResult Details(int? id = null)
		{
			var realEstate = _dbContext.RealEstates.Include(p => p.Agent).Include(p => p.Neighborhood).Include(p => p.Owner)
				.Where(p => p.ID == id)
				.FirstOrDefault();

			return View(realEstate);
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
		{
			this.FillDropdownValues();
			return View();
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
		public IActionResult Create(RealEstate model)
		{
			if (ModelState.IsValid)
			{
				_dbContext.RealEstates.Add(model);
				_dbContext.SaveChanges();

				return RedirectToAction(nameof(Index));
			}
			else
			{
				this.FillDropdownValues();
				return View();
			}
		}

        [Authorize(Roles = "Admin")]
        [ActionName(nameof(Edit))]
		public IActionResult Edit(int id)
		{
			var model = _dbContext.RealEstates.FirstOrDefault(c => c.ID == id);
			this.FillDropdownValues();
			return View(model);
		}


        [Authorize(Roles = "Admin")]
        [HttpPost]
		[ActionName(nameof(Edit))]
		public async Task<IActionResult> EditPost(int id)
		{
			var realEstate = _dbContext.RealEstates.Single(c => c.ID == id);
			var ok = await this.TryUpdateModelAsync(realEstate);

			if (ok && this.ModelState.IsValid)
			{
				_dbContext.SaveChanges();
				return RedirectToAction(nameof(Index));
			}

			this.FillDropdownValues();


			return View();


		}

		private void FillDropdownValues()
		{
			var selectItems = new List<SelectListItem>();

			var listItem = new SelectListItem();
			listItem.Text = "- odaberite -";
			listItem.Value = "";
			selectItems.Add(listItem);

			foreach (var category in _dbContext.Neighborhoods)
			{
				listItem = new SelectListItem(category.Name, category.ID.ToString());
				selectItems.Add(listItem);
			}

			ViewBag.PossibleNeighborhoods = selectItems;

            var selectItems2 = new List<SelectListItem>();

            var listItem2 = new SelectListItem();
            listItem2.Text = "- odaberite -";
            listItem2.Value = "";
            selectItems2.Add(listItem2);

            foreach (var category in _dbContext.Agents)
            {
                listItem2 = new SelectListItem(category.FullName, category.ID.ToString());
                selectItems2.Add(listItem2);
            }

            ViewBag.PossibleAgents = selectItems2;


            var selectItems3 = new List<SelectListItem>();

            var listItem3 = new SelectListItem();
            listItem3.Text = "- odaberite -";
            listItem3.Value = "";
            selectItems3.Add(listItem3);

            foreach (var category in _dbContext.Owners)
            {
                listItem3 = new SelectListItem(category.FullName, category.ID.ToString());
                selectItems3.Add(listItem3);
            }

            ViewBag.PossibleOwners = selectItems3;

        }

		[Authorize(Roles = "Admin")]
		public JsonResult Delete(int id)
		{
            RealEstate result = _dbContext.RealEstates.Find(id);


			result.Owner = null;
			result.Neighborhood = null;
			result.Agent = null;


			_dbContext.Entry(result).State = EntityState.Deleted;
            _dbContext.SaveChanges();

			return new JsonResult("Data deleted!");
        }
    }
}
