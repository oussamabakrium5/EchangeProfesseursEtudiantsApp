using EchangeProfesseursEtudiantsApp.Models;

namespace EchangeProfesseursEtudiantsApp.ViewModel
{
    public class StudentViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<ApplicationUser> applicationusers { get; set; }
        public IEnumerable<Student> students { get; set; }
    }
}
