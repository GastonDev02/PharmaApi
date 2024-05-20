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
                if (sale.id_producto.HasValue)
                {
                    var existingProduct = _context.Products.Find(sale.id_producto.Value);
                    if (existingProduct == null)
                    {
                        throw new Exception($"El producto con ID {sale.id_producto} especificado no existe.");
                    }
                }

                var productsToTicket = _productService.GetProducts();
                var newTicket = productsToTicket.Where(product => sale.lista_de_productos.Contains(product.id_producto)).ToList();
                Console.WriteLine(newTicket.Count);

                var newSale = new SaleModel
                {
                    id_producto = sale.id_producto,
                    lista_de_productos = sale.lista_de_productos,
                    id_venta = RandomIdGenerator.GenerateRandomId(),
                    cantidad = sale.cantidad,
                    tipo_de_pago = sale.tipo_de_pago,
                    ticket = newTicket,
                    descuento = sale.descuento,
                    precio_final = sale.precio_final,
                    horario_de_venta = sale.horario_de_venta
                };

                _context.Sales.Add(newSale);
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


