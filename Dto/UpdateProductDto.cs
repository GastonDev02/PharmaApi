namespace PharmaApi.Dto
{
    public class UpdateProductDto
    {
        public string nombre_producto { get; set; }
        public string descripcion_producto { get; set; }
        public string presentacion { get; set; }
        public string laboratorio { get; set; }
        public int stock { get; set; }
        public decimal precio { get; set; }
        public string categoria { get; set;}
    }
}
