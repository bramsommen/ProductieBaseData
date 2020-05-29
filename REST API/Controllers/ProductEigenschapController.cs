using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsBaseData;
using BLL;


namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductEigenschapController : ControllerBase
    {
        IProductEigenschapService Service;

        public ProductEigenschapController(IProductEigenschapService service)
        {
            try
            {
                Service = service;
            }
            catch (Exception ex)
            {

            }
        }


        // CREATE
        [HttpPost]
        public async Task Post([FromBody] ProductEigenschap obj)
        {
            try
            {
                await Service.Create(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // READ
        [HttpGet]
        public async Task<IEnumerable<ProductEigenschap>> Get(string strVersieID)
        {
            try
            {
                long.TryParse(strVersieID, out long versieID);

                return await Service.GetFrom(versieID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] ProductEigenschap obj)
        {
            try
            {
                await Service.Update(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // DELETE:
        [HttpDelete()]
        public async Task Delete([FromBody] ProductEigenschap obj)
        {
            try
            {
                await Service.Delete(obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
