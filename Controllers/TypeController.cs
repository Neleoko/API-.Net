using System.Net;
using API_.Net.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Type = API_.Net.Entities.Type;

namespace API_.Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController : ControllerBase
    {
        private readonly AnimebddContext animebddContext;

        public TypeController(AnimebddContext animebddContext)
        {
            this.animebddContext = animebddContext;
        }

        [HttpGet("GetType")]
        public async Task<ActionResult<List<Type>>> Get()
        {
            var ListType = await animebddContext.Types.Select(
                s => new Type()
                {
                    Id = s.Id,
                    Nom = s.Nom,
                }
            ).ToListAsync();

            if (ListType.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return ListType;
            }
        }
        
        [HttpGet("GetTypeById")]
        public async Task<ActionResult<Type>> GetUserById(int Id)
        {
            Type? type = await animebddContext.Types.Select(s => new Type()
            {
                Id = s.Id,
                Nom = s.Nom,
            }).FirstOrDefaultAsync(s => s.Id == Id);

            if (type == null)
            {
                return NotFound();
            }
            else
            {
                return type;
            }
        }
        
        [HttpPost("InsertType")]
        public async Task<ActionResult<Type>> InsertType(Type type)
        {
            var entity = new Type()
            {
                Nom = type.Nom,
            };
            animebddContext.Types.Add(entity);
            await animebddContext.SaveChangesAsync();
            return entity;
        }
        
        [HttpPut("UpdateType")]
        public async Task<HttpStatusCode> UpdateType(Type type)
        {
            var entity = await animebddContext.Types.FirstOrDefaultAsync(s=>s.Id == type.Id);
            if (entity == null)
            {
                return HttpStatusCode.NotFound;
            }
            entity.Nom = type.Nom;
            await animebddContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteType")]
        public async Task<HttpStatusCode> DeleteType(int Id)
        {
            var entity = await animebddContext.Types.FindAsync(Id);
            if (entity == null)
            {
                return HttpStatusCode.NotFound;
            }
            animebddContext.Types.Remove(entity);
            await animebddContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
    }
}
