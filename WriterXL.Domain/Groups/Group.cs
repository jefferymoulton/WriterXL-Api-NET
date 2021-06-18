using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WriterXL.Domain.Members;

namespace WriterXL.Domain.Groups
{
    public class Group
    {
        public int Id { get; set; }
        
        [Required]
        public string Moniker { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime DateCreated { get; set; }
    }
}