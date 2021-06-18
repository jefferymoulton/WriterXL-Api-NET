using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WriterXL.Api.Models;
using WriterXL.Data;
using WriterXL.Domain.Groups;

namespace WriterXL.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _repository;
        private readonly IMapper _mapper;

        public GroupsController(IGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<GroupModel[]>> GetAllGroups()
        {
            var groups = await _repository.GetAllGroupsAsync();
            return _mapper.Map<GroupModel[]>(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupModel>> GetGroupById(int id)
        {
            var group = await _repository.GetGroupByIdAsync(id);
            return GetGroupFromResults(group);
        }

        [HttpGet("moniker/{moniker}")]
        public async Task<ActionResult<GroupModel>> GetGroupByMoniker(string moniker)
        {
            var group = await _repository.GetGroupByMonikerAsync(moniker);
            return GetGroupFromResults(group);
        }

        [HttpPost]
        public async Task<ActionResult<GroupModel>> CreateGroup(Group group)
        {
            try
            {
                _repository.Add(group);
                await _repository.SaveChangesAsync();

                // TODO: Figure out how to return the model, not the object.
                return CreatedAtAction("GetGroupById", new {id = group.Id}, group);
            }
            catch (DbUpdateException)
            {
                return Conflict("A results with that moniker already exists.");
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