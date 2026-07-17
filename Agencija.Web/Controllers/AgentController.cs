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
	public class AgentController(
        RealEstateManagerDbContext _dbContext, UserManager<AppUser> userManager) : Controller
    {
		[Route("popis-agenata")]
        public IActionResult Index()
		{
			var agentQuery = _dbContext.Agents.AsQueryable();

            var model = agentQuery.ToList();
            return View(model);
        }


        [HttpPost]
        public IActionResult IndexAjax(AgentFilterModel filter = null)
        {
            filter ??= new AgentFilterModel();

			var agentQuery = _dbContext.Agents.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.FullName))
                agentQuery = agentQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Contact))
            agentQuery = agentQuery.Where(p => p.Contact.ToLower().Contains(filter.Contact.ToLower()));

		    if (!string.IsNullOrWhiteSpace(filter.EstatesSold))
                agentQuery = agentQuery.Where(p => p.EstatesSold.ToString().ToLower().Contains(filter.EstatesSold.ToLower()));


            var model = agentQuery.ToList();
            return PartialView("_IndexTable", model);
        }

		[Authorize]
		public IActionResult Details(int? id = null)
		{
			var agents = _dbContext.Agents
				.Where(p => p.ID == id)
				.FirstOrDefault();

			return View(agents);
		}
        
		[Authorize(Roles = "Admin")]
        public IActionResult Create()
		{
			return View();
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
		public IActionResult Create(Agent model)
		{
			if (ModelState.IsValid)
			{
				_dbContext.Agents.Add(model);
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
			var model = _dbContext.Agents.FirstOrDefault(c => c.ID == id);
			return View(model);
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
		[ActionName(nameof(Edit))]
		public async Task<IActionResult> EditPost(int id)
		{
			var realEstate = _dbContext.Agents.Single(c => c.ID == id);
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
            Agent result = _dbContext.Agents.Find(id);
            _dbContext.Entry(result).State = EntityState.Deleted;
            _dbContext.SaveChanges();

            return new JsonResult("Data deleted!");
        }

    }
}
