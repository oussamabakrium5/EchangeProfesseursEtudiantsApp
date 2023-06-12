using EchangeProfesseursEtudiantsApp.Models;
using Microsoft.AspNetCore.Identity;

namespace EchangeProfesseursEtudiantsApp.ViewModel
{
    public class UsersViewModel
    {
        public IEnumerable<ApplicationUser> applicationusers { get; set; }
	}
}
