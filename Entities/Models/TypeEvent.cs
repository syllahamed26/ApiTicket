using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Entities.Models
{
    [Table("Types")]
    public class TypeEvent : BaseModel
    {
        [Required]
        public string Libelle { get; set; }

        public List<Event> Events { get; set; }
    }
}
