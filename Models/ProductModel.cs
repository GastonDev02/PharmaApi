namespace PharmaApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string nombre_producto { get; set; }
        public string descripcion_producto { get; set; }
        public string presentacion { get; set; }
        public string laboratorio { get; set; }
        public int stock { get; set; }
        public decimal precio { get; set; }
        public int codigo_producto { get; set; }
        public string categoria { get; set; }

        public bool BeValidPresentation(string presentValid)
        {
            var allowedPresentations = new[] { "compr", "jrb", "pmd" };

            return allowedPresentations.Contains(presentValid);
        }
    }

}