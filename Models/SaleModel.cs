namespace PharmaApi.Models
{
    public class SaleModel
    {
        public int id_venta { get; set; }
        public int? id_producto { get; set; }

        public List<int>? lista_de_productos { get; set; }

        public IEnumerable<ProductModel>? ticket {  get; set; }
        public int id_vendedor { get; set; }
        public int cantidad { get; set; }
        public string tipo_de_pago { get; set; }
        public DateTime horario_de_venta { get; set; }
        public decimal descuento { get; set; }
        public decimal precio_final {get; set; }

        public ProductModel? Product { get; set; }

    }
}
