using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchangeProfesseursEtudiantsApp.Models
{
    public class Student
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("IdUser")]
        public ApplicationUser applicationuser { get; set; }

        [ForeignKey("IdGroup")]
        public Group group { get; set; }
    }
}
