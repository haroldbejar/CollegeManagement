using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class CollegeContext : DbContext
    {
        public CollegeContext(DbContextOptions<CollegeContext> options) : base(options) { }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<AlumnoGrado> AlumnosGrados { get; set; }
        public DbSet<User> Users { get; set; }

    }
}