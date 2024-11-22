using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Alumno
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}