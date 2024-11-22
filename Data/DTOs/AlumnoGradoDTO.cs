namespace Data.DTOs
{
    public class AlumnoGradoDTO
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }

        // public string AlumnoNombre { get; set; }
        public int GradoId { get; set; }
        // public string GradoNombre { get; set; }
        public string Grupo { get; set; }
    }
}