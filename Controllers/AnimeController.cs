using System.Net;
using API_.Net.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_.Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly AnimebddContext animebddContext;

        public AnimeController(AnimebddContext animebddContext)
        {
            this.animebddContext = animebddContext;
        }
        
        [HttpGet("GetAnime")]
        public async Task<ActionResult<List<Anime>>> Get()
        {
            var ListAnime = await animebddContext.Animes.Select(
                s => new Anime()
                {
                    Id = s.Id,
                    Nom = s.Nom,
                    Acronyme = s.Acronyme,
                }
            ).ToListAsync();
            
            if (ListAnime.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return ListAnime;
            }
        }
        
        [HttpGet("GetAnimeById")]
        public async Task<ActionResult<Anime>> GetUserById(int Id)
        {
            Anime? anime = await animebddContext.Animes.Select(s => new Anime()
            {
                Id = s.Id,
                Nom = s.Nom,
                Acronyme = s.Acronyme,
            }).FirstOrDefaultAsync(s => s.Id == Id);

            if (anime == null)
            {
                return NotFound();
            }
            else
            {
                return anime;
            }
        }
        
        [HttpPost("InsertAnime")]
        public async Task < HttpStatusCode > InsertUser(Anime anime) {
            var entity = new Anime() {
                Nom = anime.Nom,
                Acronyme = anime.Acronyme,
            };
            animebddContext.Animes.Add(entity);
            await animebddContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
        
        [HttpPut("UpdateAnime")]
        public async Task < HttpStatusCode > UpdateUser(Anime anime) {
            var entity = await animebddContext.Animes.FirstOrDefaultAsync(s => s.Id == anime.Id);
            if (entity == null) {
                return HttpStatusCode.NotFound;
            }
            entity.Nom = anime.Nom;
            entity.Acronyme = anime.Acronyme;
            await animebddContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteAnime")]
        public async Task < HttpStatusCode > DeleteUser(int Id) {
            var entity = await animebddContext.Animes.FirstOrDefaultAsync(s => s.Id == Id);
            if (entity == null) {
                return HttpStatusCode.NotFound;
            }
            animebddContext.Animes.Remove(entity);
            await animebddContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
