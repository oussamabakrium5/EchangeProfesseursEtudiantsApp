using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchangeProfesseursEtudiantsApp.Models
{
	public class Element
    {
        [Key]
        public int? Idelement { get; set; }
        
        [ForeignKey("IdGroup")]
        public Module moduleelement { get; set; }

        [ForeignKey("IdUser")]
        public ApplicationUser applicationuserelement { get; set; }

        public string Nameelement { get; set; }

        public string Descriptionelement { get; set; }

        public int? Coefficientelement { get; set; }
    }
}
