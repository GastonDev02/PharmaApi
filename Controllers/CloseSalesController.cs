using Microsoft.AspNetCore.Mvc;
using PharmaApi.Dto;
using PharmaApi.Models;
using PharmaApi.Services;
using System;
using System.Collections.Generic;

namespace PharmaApi.Controllers
{
    [ApiController]
    [Route("api/close-sale")]
    public class CloseSalesController : ControllerBase
    {
        private readonly CloseSalesService _closeSalesService;

        public CloseSalesController(CloseSalesService closeSalesService)
        {
            _closeSalesService = closeSalesService;
        }

        [HttpPost("get-day-sales")]
        public ActionResult<List<SaleModel>> GetDaySales([FromBody] CloseDaySalesDto closeInfo)
        {
            try
            {
                var sales = _closeSalesService.GetDaySales(closeInfo);
                return Ok(sales);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost("close-day-sales")]
        public ActionResult CloseDaySales([FromBody] List<SaleModel> closeInfo)
        {
            try
            {
                var closedSales = _closeSalesService.CloseDaySales(closeInfo);
                return Ok(new
                {
                    message = "Ventas cerradas correctamente",
                    closeSales = closedSales
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

    }
}
