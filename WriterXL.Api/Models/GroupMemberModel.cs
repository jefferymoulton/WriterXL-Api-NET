using System;
using WriterXL.Domain;

namespace WriterXL.Api.Models
{
    public class GroupMemberModel
    {
        public GroupModel Group { get; set; }
        public MemberModel Member { get; set; }
        
        public MemberRole Role { get; set; }
        public DateTime MemberSince { get; set; }
        public string Title { get; set; }
    }
}