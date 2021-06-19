using System.Collections.Generic;
using WriterXL.Domain.Members;

namespace WriterXL.Api.Models
{
    public class MemberModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public MemberStatus Status { get; set; }
        
        public ICollection<GroupModel> MemberOf { get; set; }
    }
}