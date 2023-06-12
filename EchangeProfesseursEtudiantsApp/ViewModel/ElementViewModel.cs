using EchangeProfesseursEtudiantsApp.Models;

namespace EchangeProfesseursEtudiantsApp.ViewModel
{
    public class ElementViewModel
    {
        public IEnumerable<ApplicationUser> applicationusers { get; set; }
        public IEnumerable<Module> modules { get; set; }
        public IEnumerable<Element> elements { get; set; }
    }
}
