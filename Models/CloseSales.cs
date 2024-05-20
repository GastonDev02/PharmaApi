namespace PharmaApi.Models
{
    public class CloseSales
    {
        public int id_cierre_venta {  get; set; }
        public int id_venta { get; set; }
        public DateTime horario_cierre { get; set; }
        public int precio_cierre { get; set; }
    }
}
