using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WriterXL.Domain.Groups;

namespace WriterXL.Domain.Members
{
    public class Member
    {
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string EmailAddress { get; set; }
        
        [Required]
        public MemberStatus Status { get; set; }
        
        [Required]
        public DateTime MemberSince { get; set; }
    }

    public enum MemberStatus
    {
        Active,
        Inactive,
        Deleted
    }
}