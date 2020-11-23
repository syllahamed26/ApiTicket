using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace apiTckets.Entities.Models
{
    [Table("Events")]
    public class Event : BaseModel
    {
        public string Nom { get; set; }
        public DateTime Date { get; set; }
        public DateTime Heure { get; set; }
        public string Lieu { get; set; }
        public string Image { get; set; }
        public string CodeEvent { get; set; }
        public string Statut { get; set; }

        public int TypeId { get; set; }
        public TypeEvent Type { get; set; }
        public int PromoteurId { get; set; }
        public Promoteur Promoteur { get; set; }
    }
}
