using EchangeProfesseursEtudiantsApp.Models;

namespace EchangeProfesseursEtudiantsApp.ViewModel
{
    public class ModuleViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<ApplicationUser> applicationusers { get; set; }
        public IEnumerable<Module> modules { get; set; }
    }
}
