using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchangeProfesseursEtudiantsApp.Models
{
	public class Module
    {
        [Key]
        public int? Idmodule { get; set; }
        
        [ForeignKey("IdGroup")]
        public Group groupmodule { get; set; }

        [ForeignKey("IdUser")]
        public ApplicationUser applicationusermodule { get; set; }

        [Required]
        public string Namemodule { get; set; }

        [Required]
        public string Descriptionmodule { get; set; }


        public int? Coefficientmodule { get; set; }
    }
}
