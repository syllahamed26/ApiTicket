using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Entities.Models
{
    [Table("UsersTickets")]
    public class UserTicket : BaseModel
    {
        public string CodeQr { get; set; }
        public DateTime DateValide { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
