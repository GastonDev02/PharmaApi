using System;
using System.Collections.Generic;
using System.Linq;
using PharmaApi.Data;
using PharmaApi.Dto;
using PharmaApi.Models;
using PharmaApi.Utils;

namespace PharmaApi.Services
{
    public class SaleService
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductService _productService;
        private List<SaleModel> _saleItems;

        public SaleService(ApplicationDbContext context)
        {
            _context = context;
            _productService = new ProductService(context);
            _saleItems = new List<SaleModel>();
        }


        public List<SaleModel> GetSales()
        {
            try
            {
                var sales = _context.Sales.ToList();
                return sales;
            }
            catch(Exception ex)
            {
                throw new Exception("Error al encontrar todas las ventas", ex);
            }
        }

        public SaleModel CreateSale(CreateSaleDto sale)
        {
            try
            {
                var newSale = new SaleModel
                {
                    Id = RandomIdGenerator.GenerateRandomId(),
                    cantidad = sale.cantidad,
                    tipo_de_pago = sale.tipo_de_pago,
                    Ticket = sale.Ticket.Select(product => _context.Products.Find(product.Id)).ToList(),
                    descuento = sale.descuento,
                    precio_final = sale.precio_final,
                    horario_de_venta = sale.horario_de_venta
                };

                Console.Write("La nueva venta: ", newSale.ToString());

                _context.Sales.Add(newSale);
                foreach (var productInTicket in newSale.Ticket)
                {
                    var product = _context.Products.Find(productInTicket.Id);
                    if (product != null)
                    {
                        product.stock -= newSale.cantidad;
                    }
                }
                _context.SaveChanges();
                return newSale;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error al crear la venta", ex);
            }
        }



    }
}


