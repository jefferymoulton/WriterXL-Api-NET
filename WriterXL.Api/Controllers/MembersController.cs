using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WriterXL.Api.Models;
using WriterXL.Data;
using WriterXL.Domain.Members;

namespace WriterXL.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public MembersController(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<MemberModel[]>> GetAllMembers()
        {
            var members = await _repository.GetAllMembersAsync();
            return _mapper.Map<MemberModel[]>(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberModel>> GetMemberById(int id, bool includeGroups = false)
        {
            var member = await _repository.GetMemberByIdAsync(id, includeGroups);
            return GetMemberFromResults(member);
        }

        [HttpGet("email/{emailAddress}")]
        public async Task<ActionResult<MemberModel>> GetMemberByEmail(string emailAddress, bool includeGroups = false)
        {
            var member = await _repository.GetMemberByEmailAsync(emailAddress, includeGroups);
            return GetMemberFromResults(member);
        }

        [HttpPost]
        public async Task<ActionResult<MemberModel>> CreateMember(MemberModel memberModel)
        {
            try
            {
                var member = _mapper.Map<Member>(memberModel);
                
                _repository.Add(member);
                await _repository.SaveChangesAsync();

                return CreatedAtAction("GetMemberById", new {id = member.Id}, member);
            }
            catch (DbUpdateException)
            {
                return Conflict("A member with that email address already exists.");
            }
        }

        private ActionResult<MemberModel> GetMemberFromResults(Member results)
        {
            if (results == null)
            {
                return NotFound();
            }

            return _mapper.Map<MemberModel>(results);
        }
    }
}