using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Grado
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        [ForeignKey(nameof(Profesor))]
        public int ProfesorId { get; set; }
    }
}