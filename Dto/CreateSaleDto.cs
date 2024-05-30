﻿using PharmaApi.Models;

namespace PharmaApi.Dto
{
    public class CreateSaleDto
    {
        public int cantidad { get; set; }
        public string tipo_de_pago { get; set; }
        public DateTime horario_de_venta { get; set; }
        public decimal descuento { get; set; }
        public decimal precio_final { get; set; }
        public List<ProductModel> Ticket { get; set; } = new List<ProductModel>();
    }
}
