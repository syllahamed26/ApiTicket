using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Entities.Models
{
    [Table("Tickets")]
    public class Ticket : BaseModel
    {
        public decimal Montant { get; set; }
        public string Type { get; set; }
        public decimal Nombre { get; set; }

        public int EnventId { get; set; }
        public Event Event { get; set; }

        public List<UserTicket> UserTickets { get; set; }
    }
}
