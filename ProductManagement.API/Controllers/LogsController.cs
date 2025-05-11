using Microsoft.AspNetCore.Mvc;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Persistence;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public LogsController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLogs(
            [FromQuery] DateTime? since,
            [FromQuery] string? productName,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.LowStockLogs.AsQueryable();

            // Tarih filtresi
            if (since.HasValue)
            {
                query = query.Where(x => x.LoggedAt >= since.Value);
            }

            // Ürün adına göre arama
            if (!string.IsNullOrWhiteSpace(productName))
            {
                query = query.Where(x => x.ProductName.ToLower().Contains(productName.ToLower()));
            }

            var totalCount = query.Count();

            // Sayfalama
            var logs = query
                .OrderByDescending(x => x.LoggedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Items = logs
            });
        }
    }
}
