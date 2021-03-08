using OrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace OrderingSystem.Controllers
{
    public class CustomerController : ApiController
    {
       private readonly IOrderRepository _entities;
        
        public CustomerController(IOrderRepository entities)
        {
            _entities = entities;
        }

        #region GetCustomerDetails
        [HttpGet]
        [Route("api/Customer")]
        public IHttpActionResult GetCustomer()
        {
            try
            {
                var result = _entities.GetAllCustomer();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region GetCustomerwithId
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            try
            {
                var result = _entities.GetCustomer(id);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region AddCustomer
        [HttpPost]
        [Route("api/Customer")]
        public IHttpActionResult Post([FromBody] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_entities.checkEmailId(customer.EmailId))
                    {
                        return BadRequest("Email '{customer.EmailId}' already exists.");
                    }
                    else
                    {
                        _entities.AddCustomer(customer);
                    }
                }
                var location = $"api/Customer?id";
                return Created(location, "Customer added");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region RegisterCustomer
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _entities.GetCustomer(id);

                    if (result != null)
                    {
                        if (_entities.checkEmailId(result.EmailId))
                        {
                            return BadRequest("Email '{customer.EmailId}' already exists.");
                        }
                        else
                        {
                            result.customerName = customer.customerName;
                            result.EmailId = customer.EmailId;
                            _entities.SaveChanges();
                        }

                        var location = $"api/Customer?id";
                        return Created(location, "Employee updated");
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region GetAllCustomersUsingPagination
        [HttpGet]
        public IHttpActionResult GetCustomerfromUri([FromUri] PagingModel pageModel)
        {
            try
            {
                var result = _entities.GetAllCustomer();

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
