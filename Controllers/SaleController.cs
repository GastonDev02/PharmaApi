using Microsoft.AspNetCore.Mvc;
using PharmaApi.Dto;
using PharmaApi.Models;
using PharmaApi.Services;

namespace PharmaApi.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _saleService;

        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("get-sales")]
        public ActionResult<SaleModel> GetAllSales()
        {
            try
            {
                var getSales = _saleService.GetSales();
                var response = new
                {
                    message = "Ventas obtenidas",
                    sales = getSales
                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPost("create-sale", Name = "create-sale")]
        public ActionResult<SaleModel> CreateSale([FromBody] CreateSaleDto saleDto)
        {
            try
            {
                var newSale = _saleService.CreateSale(saleDto);
                var response = new
                {
                    message = "Se realizó la venta",
                    ticket = newSale
                };
                return Ok(newSale);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
