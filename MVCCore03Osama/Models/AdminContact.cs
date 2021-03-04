using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore03Osama.Models
{
    public class AdminContact
    {
        [Key]
        public int ID { get; set; }

        public string Meessage { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { set; get; }

        public virtual ApplicationUser User { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SendAt { get; set; }


        public bool Seen { get; set; }

        public string Response { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ResponserID { get; set; }
    }
}
