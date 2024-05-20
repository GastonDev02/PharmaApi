using PharmaApi.Data;
using PharmaApi.Dto;
using PharmaApi.Models;
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

        public List<SaleModel> CloseDaySales(CloseDaySalesDto closeInfo)
        {
            try
            {
                var sales = _saleService.GetSales();
                if (!DateTime.TryParseExact(closeInfo.FromDate, "d 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"), DateTimeStyles.None, out DateTime fromDate) ||
                    !DateTime.TryParseExact(closeInfo.ToDate, "d 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"), DateTimeStyles.None, out DateTime toDate))
                {
                    throw new ArgumentException("Formato de fecha inválido.");
                }

                if (!TimeSpan.TryParseExact(closeInfo.FromTime, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan fromTime) ||
                    !TimeSpan.TryParseExact(closeInfo.ToTime, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan toTime))
                {
                    throw new ArgumentException("Formato de hora inválido.");
                }

                DateTime fromDateTime = fromDate.Date + fromTime;
                DateTime toDateTime = toDate.Date + toTime;

                var filteredSales = sales.Where(sale =>
                    sale.horario_de_venta >= fromDateTime &&
                    sale.horario_de_venta <= toDateTime)
                    .ToList();

                return filteredSales;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error " + ex);
                throw new Exception($"{ex.Message}");
            }
        }

    }
}
