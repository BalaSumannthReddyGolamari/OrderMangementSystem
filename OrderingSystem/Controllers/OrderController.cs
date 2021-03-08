using Newtonsoft.Json;
using OrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace OrderingSystem.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _entities;

        public OrderController(IOrderRepository entities)
        {
            _entities = entities;
        }

        #region GetAllOrder
        [HttpGet]
        [Route("api/Order")]
        public IHttpActionResult GetOrder()
        {
            try
            {
                var result = _entities.GetAllOrder();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion
        
        #region PlaceOrder
        [HttpPost]
        [Route("api/Order")]
        public IHttpActionResult Post([FromBody] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _entities.AddOrder(order);
                }
                var location = $"api/Order?id";
                return Created(location, "Order added");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region GetOrderSpecifictoCustomer
        [HttpGet]
        public IHttpActionResult GetOrder(int id)
        {
            try
            {
                var result = _entities.GetOrderByCust(id);

                if (result != null)
                {
                    return Ok(result);
                }
                //var location = $"api/Customer?id";
                //return Created(location, "Employee not found with id :- " + id);
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region GetOrdersBasedonPageSizeandIndex
        [HttpGet]
        public IHttpActionResult GetOrderusingPagination([FromUri] PagingModel pageModel)
        {
            try
            {
                var result = _entities.GetAllOrder();
                
                var count = result.Count();

                int CurrentPage = pageModel.pageNumber;

                int PageSize = pageModel.pageSize;

                int TotalCount = count;

                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                var items = result.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                var previousPage = CurrentPage > 1 ? "Yes" : "No";

                var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

                var paginationMetadata = new
                {
                    totalCount = TotalCount,
                    pageSize = PageSize,
                    currentPage = CurrentPage,
                    totalPages = TotalPages,
                    previousPage,
                    nextPage
                };

                HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

                return Ok(items);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

    }
}
