using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EchangeProfesseursEtudiantsApp.Models;

namespace EchangeProfesseursEtudiantsApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> applicationUsers { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
    }


}
