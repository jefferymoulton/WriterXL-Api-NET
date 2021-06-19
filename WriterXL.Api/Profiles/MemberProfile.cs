using AutoMapper;
using WriterXL.Api.Models;
using WriterXL.Domain.Groups;
using WriterXL.Domain.Members;

namespace WriterXL.Api.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            this.CreateMap<Member, MemberModel>()
                .ForMember(model => model.MemberOf, 
                    opt => opt.MapFrom(m => m.MemberOf));
        }
    }
}