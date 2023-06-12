using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchangeProfesseursEtudiantsApp.Models
{
	public class Group
	{
        [Key]
        public int? Idgroup { get; set; }
        
        [ForeignKey("IdUser")]
        public ApplicationUser applicationusergroup { get; set; }

        [Required]
        public string Namegroup { get; set; }
        [Required]
        public string Descriptiongroup { get; set; }
	}
}
