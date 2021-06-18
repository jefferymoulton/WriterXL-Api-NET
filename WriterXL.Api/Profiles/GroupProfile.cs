using AutoMapper;
using WriterXL.Api.Models;
using WriterXL.Domain.Groups;

namespace WriterXL.Api.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            this.CreateMap<Group, GroupModel>();
        }
    }
}