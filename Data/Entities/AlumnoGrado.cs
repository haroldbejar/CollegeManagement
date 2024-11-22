using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class AlumnoGrado
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Alumno))]
        public int AlumnoId { get; set; }

        [ForeignKey(nameof(Grado))]
        public int GradoId { get; set; }
        public string Grupo { get; set; }
    }
}