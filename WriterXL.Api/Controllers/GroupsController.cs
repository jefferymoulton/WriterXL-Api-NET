using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WriterXL.Data;
using WriterXL.Domain.Groups;

namespace WriterXL.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly WxlContext _context;

        public GroupsController(WxlContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Group>>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroupById(int id)
        {
            var group = await _context.Groups
                .Where(g => g.Id == id)
                .SingleOrDefaultAsync();

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupById", new {id = group.Id}, group);
        }
    }
}