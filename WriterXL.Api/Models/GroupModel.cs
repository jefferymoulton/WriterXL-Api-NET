using System.Collections.Generic;

namespace WriterXL.Api.Models
{
    public class GroupModel
    {
        public string Moniker { get; set; }
        public string Name { get; set; }
        public List<MemberModel> Members { get; set; }
    }
}