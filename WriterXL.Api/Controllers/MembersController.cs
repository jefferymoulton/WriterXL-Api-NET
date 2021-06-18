using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WriterXL.Data;
using WriterXL.Domain.Members;

namespace WriterXL.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly WxlContext _context;

        public MembersController(WxlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            var member = await _context.Members
                .Where(m => m.Id == id)
                .SingleOrDefaultAsync();

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<Member>> GetMemberByEmail(string email)
        {
            var member = await _context.Members
                .Where(m => m.EmailAddress.Equals(email))
                .SingleOrDefaultAsync();

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemberById", new {id = member.Id}, member);
        }
    }
}