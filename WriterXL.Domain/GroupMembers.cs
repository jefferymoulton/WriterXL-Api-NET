using System;
using System.ComponentModel.DataAnnotations;

namespace WriterXL.Domain
{
    public class GroupMembers
    {
        public int GroupId { get; set; }
        public int MemberId { get; set; }
        
        [Required]
        public MemberRole Role { get; set; }
        
        [Required]
        public DateTime MemberSince { get; set; }
        
        public string Title { get; set; }
    }

    public enum MemberRole
    {
        Administrator,
        Member,
        Auditor
    }
}