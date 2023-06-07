using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchangeProfesseursEtudiantsApp.Models
{
	public class Group
	{
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("IdUser")]
        public ApplicationUser applicationuser { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
	}
}
