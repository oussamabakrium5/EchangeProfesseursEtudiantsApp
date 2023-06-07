using EchangeProfesseursEtudiantsApp.Models;

namespace EchangeProfesseursEtudiantsApp.ViewModel
{
    public class GroupViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<ApplicationUser> applicationusers { get; set; }
    }
}
