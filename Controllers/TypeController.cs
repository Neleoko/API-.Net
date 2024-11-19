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
            animebddContext.Types.Add(type);
            await animebddContext.SaveChangesAsync();
            return CreatedAtAction("GetTypeById", new { Id = type.Id }, type);
        }
        
        [HttpPut("UpdateType")]
        public async Task<HttpStatusCode> UpdateType(Type type)
        {
            animebddContext.Entry(type).State = EntityState.Modified;
            await animebddContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
        [HttpDelete("DeleteType")]
        public async Task<HttpStatusCode> DeleteType(int Id)
        {
            var type = await animebddContext.Types.FindAsync(Id);
            if (type == null)
            {
                return HttpStatusCode.NotFound;
            }
            animebddContext.Types.Remove(type);
            await animebddContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        
    }
}
