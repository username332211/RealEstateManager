using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agencija.DAL;
using Agencija.Model;
using Agencija.Web.Models;

namespace Agencija.Web.Controllers
{
	public class OwnerController(
        RealEstateManagerDbContext _dbContext, UserManager<AppUser> userManager) : Controller
    {
        [Route("popis-vlasnika")]

        [Authorize(Roles = "Admin, Manager")]
		public IActionResult Index()
		{
			var ownerQuery = _dbContext.Owners.AsQueryable();
			var model = ownerQuery.ToList();

			return View(model);

		}

		[HttpPost]
        public IActionResult IndexAjax(OwnerFilterModel filter = null)
        {
            filter ??= new OwnerFilterModel();

			var ownerQuery = _dbContext.Owners.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.FullName))
                ownerQuery = ownerQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Contact))
                ownerQuery = ownerQuery.Where(p => p.Contact.ToLower().Contains(filter.Contact.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.ContractSigned))
                ownerQuery = ownerQuery.Where(p => p.ContractSigned.ToString().ToLower().Contains(filter.ContractSigned.ToLower()));


            var model = ownerQuery.ToList();
            return PartialView("_IndexTable", model);
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Details(int? id = null)
		{
			var owners = _dbContext.Owners
				.Where(p => p.ID == id)
				.FirstOrDefault();

			return View(owners);
		}

        [Authorize(Roles = "Admin")]

        public IActionResult Create()
		{
			return View();
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
		public IActionResult Create(Owner model)
		{
			if (ModelState.IsValid)
			{
				_dbContext.Owners.Add(model);
				_dbContext.SaveChanges();

				return RedirectToAction(nameof(Index));
			}
			else
			{
				return View();
			}
		}

        [Authorize(Roles = "Admin")]
        [ActionName(nameof(Edit))]
		public IActionResult Edit(int id)
		{
			var model = _dbContext.Owners.FirstOrDefault(c => c.ID == id);
			return View(model);
		}


        [Authorize(Roles = "Admin")]
        [HttpPost]
		[ActionName(nameof(Edit))]
		public async Task<IActionResult> EditPost(int id)
		{
			var realEstate = _dbContext.Owners.Single(c => c.ID == id);
			var ok = await this.TryUpdateModelAsync(realEstate);

			if (ok && this.ModelState.IsValid)
			{
				_dbContext.SaveChanges();
				return RedirectToAction(nameof(Index));
			}

			return View();


		}

        public JsonResult Delete(int id)
        {
            Owner result = _dbContext.Owners.Find(id);
            _dbContext.Entry(result).State = EntityState.Deleted;
            _dbContext.SaveChanges();

            return new JsonResult("Data deleted!");
        }

    }
}
