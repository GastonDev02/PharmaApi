namespace PharmaApi.Models
{
    public class CloseSales
    {
        public int Id {  get; set; }
        public DateTime date_cierre { get; set; }
        public List<SaleModel> listado_de_ventas_dia { get; set; } = new List<SaleModel>();
        public decimal precio_cierre { get; set; }
    }
}
