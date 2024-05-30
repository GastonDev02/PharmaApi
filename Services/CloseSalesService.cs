using Microsoft.EntityFrameworkCore;
using PharmaApi.Data;
using PharmaApi.Dto;
using PharmaApi.Models;
using PharmaApi.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PharmaApi.Services
{
    public class CloseSalesService
    {
        private readonly ApplicationDbContext _context;
        private readonly SaleService _saleService;

        public CloseSalesService(ApplicationDbContext context, SaleService saleService)
        {
            _context = context;
            _saleService = saleService;
        }

        public List<SaleModel> GetDaySales(CloseDaySalesDto closeInfo)
        {
            try
            {
                Console.WriteLine("From date: " + closeInfo.FromDate);
                Console.WriteLine("To date: " + closeInfo.ToDate);

                var sales = _saleService.GetSales();

                var filteredSales = sales.Where(sale =>
                    sale.horario_de_venta >= closeInfo.FromDate &&
                    sale.horario_de_venta <= closeInfo.ToDate)
                    .ToList();

                return filteredSales;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex);
                throw new Exception($"{ex.Message}");
            }
        }



        public CloseSales CloseDaySales(List<SaleModel> sales)
        {
            try
            {
                Console.WriteLine(sales);
                var closeSales = new CloseSales
                {
                    Id = RandomIdGenerator.GenerateRandomId(),
                    date_cierre = DateTime.UtcNow,
                    precio_cierre = sales.Sum(s => s.precio_final),
                    listado_de_ventas_dia = new List<SaleModel>()
                };

                foreach (var sale in sales)
                {
                    _context.Entry(sale).State = EntityState.Unchanged;
                    closeSales.listado_de_ventas_dia.Add(sale);
                }

                _context.CloseSales.Add(closeSales);
                _context.SaveChanges();
                return closeSales;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
