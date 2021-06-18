using AutoMapper;
using WriterXL.Api.Models;
using WriterXL.Domain.Members;

namespace WriterXL.Api.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            this.CreateMap<Member, MemberModel>();
        }
    }
}