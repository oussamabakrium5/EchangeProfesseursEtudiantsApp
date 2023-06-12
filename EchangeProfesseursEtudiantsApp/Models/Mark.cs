using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchangeProfesseursEtudiantsApp.Models
{
	public class Mark
    {
        [Key]
        public int? Idmark { get; set; }
        
        [ForeignKey("IdElement")]
        public Element elementmark { get; set; }

        [ForeignKey("IdUser")]
        public ApplicationUser applicationusermark { get; set; }

        public int? themark { get; set; }
    }
}
