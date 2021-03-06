﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Entities.Models
{
    [Table("Users")]
    public class User : BaseModel
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Contact { get; set; }
        [EmailAddress]
        [Required]
        public string Mail { get; set; }
        public string Image { get; set; }
        public List<UserTicket> UserTickets { get; set; }
    }
}
