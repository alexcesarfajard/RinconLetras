
namespace RinconLetras.Models
    {
        public class Libro
        {
            public int IdLibro { get; set; }
            public string Nombre { get; set; }              
            public decimal Precio { get; set; }             
            public int CantidadInventario { get; set; }     
            public string Editorial { get; set; }          
            public string Genero { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }
 
    }
}
