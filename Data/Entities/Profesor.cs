using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Profesor
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }

    }
}