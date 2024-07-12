using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PE_PRN231_Sum22B1.Models;
using System.Collections.Generic;
using System.Net;

namespace PE_PRN231_Sum22B1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private PRN_Sum22_B1Context _context;
        private IMapper _mapper;
        public CustomerController(PRN_Sum22_B1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("Delete/{CustomerId}")]
        public IActionResult Post(string CustomerId)
        {
            try
            {
                Customer customer = _context.Customers.Where(c => c.CustomerId.Equals(CustomerId)).FirstOrDefault();
                if (customer == null)
                {
                    return NotFound();
                }
                List<Order> countOrder = _context.Orders.Where(o => o.CustomerId.Equals(CustomerId)).ToList();
                int countOrderDetail = 0;
                List<OrderDetail> listAllOrderDetailsDelete = new List<OrderDetail>();
                foreach (Order order in countOrder)
                {
                    List<OrderDetail> orderDetails = _context.OrderDetails.Where(o => o.OrderId == order.OrderId).ToList();
                    if(orderDetails.Count > 0)
                    {
                        listAllOrderDetailsDelete.AddRange(orderDetails);
                    }
                    countOrderDetail += orderDetails.Count;
                }
                CustomerNew rs = new CustomerNew()
                {
                    customerDeleteCount = 1,
                    orderDeleteCount = countOrder.Count,
                    orderDetaildeleteCount = countOrderDetail,
                };
                _context.OrderDetails.RemoveRange(listAllOrderDetailsDelete);
                _context.Orders.RemoveRange(countOrder);
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return Conflict("The was an unknown error when performing data deletion");
            }
        }
    }
}
