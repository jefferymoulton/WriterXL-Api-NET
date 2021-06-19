using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WriterXL.Api.Models;
using WriterXL.Data;
using WriterXL.Domain.Groups;
using WriterXL.Domain.Members;

namespace WriterXL.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _repository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GroupsController(IGroupRepository repository, IMemberRepository memberRepository, IMapper mapper)
        {
            _repository = repository;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<GroupModel[]>> GetAllGroups(bool includeMembers = false)
        {
            var groups = await _repository.GetAllGroupsAsync(includeMembers);
            return _mapper.Map<GroupModel[]>(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupModel>> GetGroupById(int id, bool includeMembers = false)
        {
            var group = await _repository.GetGroupByIdAsync(id, includeMembers);
            return GetGroupFromResults(group);
        }

        [HttpGet("moniker/{moniker}")]
        public async Task<ActionResult<GroupModel>> GetGroupByMoniker(string moniker, bool includeMembers = false)
        {
            var group = await _repository.GetGroupByMonikerAsync(moniker, includeMembers);
            return GetGroupFromResults(group);
        }

        [HttpPost]
        public async Task<ActionResult<GroupModel>> CreateGroup(Group group)
        {
            try
            {
                _repository.Add(group);
                if (await _repository.SaveChangesAsync())
                {
                    return CreatedAtAction("GetGroupById", new {id = group.Id}, group);
                }
                else
                {
                    throw new DbUpdateException("Unable to add member");
                }

                // TODO: Figure out how to return the model, not the object.
            }
            catch (DbUpdateException)
            {
                return Conflict("A results with that moniker already exists.");
            }

        }

        [HttpPut("{moniker}/member/{emailAddress}")]
        public async Task<ActionResult<GroupModel>> AddMemberToGroup(string moniker, string emailAddress)
        {
            try
            {
                var group = await _repository.GetGroupByMonikerAsync(moniker, true);
                var member = await _memberRepository.GetMemberByEmailAsync(emailAddress);

                if (group.Members.Exists(m => m.Id == member.Id)) return Ok(group);

                var success = await _repository.AddMemberToGroup(member, moniker);

                if (success)
                {
                    return _mapper.Map<GroupModel>(group);
                }
                else
                {
                    throw new DbUpdateException("Unable to add member to the group.");
                }
            }
            catch (DbUpdateException e)
            {
                return Problem(e.Message);
            }
        }

        private ActionResult<GroupModel> GetGroupFromResults(Group results)
        {
            if (results == null)
            {
                return NotFound();
            }

            return _mapper.Map<GroupModel>(results);
        }
    }
}