using OrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderingSystem.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IOrderRepository _entities;

        public ProductController(IOrderRepository entities) 
        {
            _entities = entities;
        }

        #region GetProductDetails
        [HttpGet]
        [Route("api/Product")]
        public IHttpActionResult GetProduct()
        {
            try
            {
                var result = _entities.GetAllProduct();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion

        #region GetproductWithName
        [HttpGet]
        [Route("api/Product/{name}")]
        public IHttpActionResult GetProduct(string name)
        {
            try
            {
                var result = _entities.GetProduct(name);

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


    }
}
