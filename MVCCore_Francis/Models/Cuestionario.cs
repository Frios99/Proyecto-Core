namespace MVCCore_Francis.Models
{
    public class Cuestionario
    {
        public int CuestionarioID { get; set; }
        public string? Pregunta { get; set; }
        public string? Opcion1 { get; set; }
        public string? Opcion2 { get; set; }
        public string? Opcion3 { get; set; }
        public string? Opcion4 { get; set; }
        public int Puntaje1 { get; set; }
        public int Puntaje2 { get; set; }
        public int Puntaje3 { get; set; }
        public int Puntaje4 { get; set; }



    }
}
