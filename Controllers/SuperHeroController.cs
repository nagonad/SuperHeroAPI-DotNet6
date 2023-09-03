using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetSingleHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null) return BadRequest("Hero not found");
            return Ok(hero);
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero requestHero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(requestHero.Id);
            if (dbHero == null) return BadRequest("Hero not found");
            dbHero.Name = requestHero.Name;
            dbHero.FirstName = requestHero.FirstName;
            dbHero.LastName = requestHero.LastName;
            dbHero.Place = requestHero.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = _context.SuperHeroes.Find(id);
            if (dbHero == null) return BadRequest("Hero not found");
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
