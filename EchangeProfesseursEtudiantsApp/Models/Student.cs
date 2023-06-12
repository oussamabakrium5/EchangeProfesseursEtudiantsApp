using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchangeProfesseursEtudiantsApp.Models
{
    public class Student
    {
        [Key]
        public int? Idstudent { get; set; }

        [ForeignKey("IdUser")]
        public ApplicationUser applicationuserstudent { get; set; }

        [ForeignKey("IdGroup")]
        public Group groupstudent { get; set; }
    }
}
