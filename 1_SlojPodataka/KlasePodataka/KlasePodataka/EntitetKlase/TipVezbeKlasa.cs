using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KlasePodataka.EntitetKlase
{
    [Table("TipVezbe")]
    public class TipVezbeKlasa
    {
        [Key]
        public int TipVezbeID { get; set; }

        [Required]
        [StringLength(30)]
        public string NazivTipa { get; set; }
    }
}
