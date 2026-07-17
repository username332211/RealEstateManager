using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agencija.DAL;
using Agencija.Model;
using Agencija.Web.Models;

namespace Agencija.Web.Controllers
{

	[Route("api/agent")]
    [ApiController]
	public class AgentApiController(
        RealEstateManagerDbContext _dbContext) : Controller
    {

        public IActionResult Get()
        {
            var agents = _dbContext.Agents.Select(p => new AgentDTO()
            {
                ID = p.ID,
                FullName = p.FullName,
                Contact = p.Contact,
                DateOfBirth = p.DateOfBirth,
                EstatesSold = p.EstatesSold,
                WorkExperience = p.WorkExperience
            }).ToList();

            return Ok(agents);
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {


            var agent = _dbContext.Agents.Where(p => p.ID == id).Select(p => new AgentDTO()
            {
                ID = p.ID,
                FullName = p.FullName,
                Contact = p.Contact,
                DateOfBirth = p.DateOfBirth,
                EstatesSold = p.EstatesSold,
                WorkExperience = p.WorkExperience

            }).FirstOrDefault();


            return Ok(agent);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AgentDTO agent)
        {
            if (agent == null)
            {
                return BadRequest();
            }

            string[] name = agent.FullName.Split(' ');

            _dbContext.Agents.Add(new Agent()
            {
                FirstName = name[0],
                LastName = name[1],
                Contact = agent.Contact,
                DateOfBirth = agent.DateOfBirth,
                EstatesSold= agent.EstatesSold,
                WorkExperience = agent.WorkExperience
            });

            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody] AgentDTO agent)
        {
            var agents = _dbContext.Agents.First(p => p.ID == id);

            string[] name = agent.FullName.Split(' ');

            agents.FirstName = name[0];
            agents.LastName = name[1];
            agent.Contact = agent.Contact;
            agents.DateOfBirth = agent.DateOfBirth;
            agents.EstatesSold = agent.EstatesSold;
            agents.WorkExperience = agent.WorkExperience;
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var agents = _dbContext.Agents.First(p => p.ID == id);
            _dbContext.Entry(agents).State = EntityState.Deleted;
            _dbContext.SaveChanges();

            return Ok();
        }


    }
}
