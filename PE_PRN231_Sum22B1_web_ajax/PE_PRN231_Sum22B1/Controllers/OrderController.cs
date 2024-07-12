using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PE_PRN231_Sum22B1.DTO;
using PE_PRN231_Sum22B1.Models;

namespace PE_PRN231_Sum22B1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private PRN_Sum22_B1Context _context;
        private IMapper _mapper;
        public OrderController(PRN_Sum22_B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Route("GetAllOrder")]
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            List<Order> orders = _context.Orders.Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Employee.Department).ToList();
            return Ok(_mapper.Map<List<OrderDTO>>(orders));
        }
        [HttpGet("GetOrderByDate/{From}/{To}")]
        //[Route("GetOrderByDate")]
        public IActionResult GetOrderByDate(DateTime From, DateTime To)
        {
            List<Order> orders = _context.Orders.Include(o => o.Customer)
                            .Include(o => o.Employee)
                            .Include(o => o.Employee.Department).Where(o => o.OrderDate >= From && o.OrderDate <= To).ToList();
            return Ok(_mapper.Map<List<OrderDTO>>(orders));
        }
    }
}
