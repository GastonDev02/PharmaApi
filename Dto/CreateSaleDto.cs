namespace PharmaApi.Dto
{
    public class CreateSaleDto
    {
        public int? id_producto { get; set; }
        public int id_vendedor { get; set; }
        public List<int>? lista_de_productos { get; set; }
        public int cantidad { get; set; }
        public string tipo_de_pago { get; set; }
        public DateTime horario_de_venta { get; set; }
        public decimal descuento { get; set; }
        public decimal precio_final { get; set; }
    }
}
