using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Entities.Models
{
    [Table("Promoteurs")]
    public class Promoteur : BaseModel
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Contact { get; set; }
        [EmailAddress,Required]
        public string Mail { get; set; }
        public string Photo { get; set; }

        public List<Event> Events { get; set; }
    }
}
