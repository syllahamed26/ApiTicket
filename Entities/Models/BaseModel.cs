using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Entities.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public BaseModel()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
