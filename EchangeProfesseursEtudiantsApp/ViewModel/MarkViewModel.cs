using EchangeProfesseursEtudiantsApp.Models;

namespace EchangeProfesseursEtudiantsApp.ViewModel
{
    public class MarkViewModel
    {
        public IEnumerable<ApplicationUser> applicationusers { get; set; }
        public IEnumerable<Module> modules { get; set; }
        public IEnumerable<Element> elements { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Mark> marks { get; set; }
    }
}
