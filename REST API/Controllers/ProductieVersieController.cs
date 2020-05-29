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
    public class ProductieVersieController : ControllerBase
    {
        IProductVersieService Service;

        public ProductieVersieController(IProductVersieService service)
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
        public async Task Post([FromBody] ProductVersie obj)
        {
            try
            {
                await Service.Create(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // CREATE
        [HttpPost("CreateFromTemplate")]
        public async Task CreateFromTemplate([FromBody] Product obj)
        {
            try
            {
                await Service.CreateFromTemplate(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // CREATE
        [HttpPost("CreateNewFromOtherVersion")]
        public async Task CreateNewFromOtherVersion([FromBody] ProductVersie obj)
        {
            try
            {
                await Service.CreateNewFromOtherVersion(obj);
            }
            catch (Exception ex)
            {

            }
        }


        // READ
        [HttpGet]
        public async Task<ProductVersie> Get(string strProductVersieID)
        {
            try
            {
                long.TryParse(strProductVersieID, out long productVersieID);

                return await Service.GetFrom(productVersieID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Validate Result
        [HttpGet("ValidateVersie")]
        public async Task<string> ValidateVersie(string strProductVersieID)
        {
            try
            {
                long.TryParse(strProductVersieID, out long productVersieID);

                string result = await Service.ValidateVersie(productVersieID);

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] ProductVersie obj)
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
        public async Task Delete([FromBody] ProductVersie obj)
        {
            try
            {
                obj.ProductEigenschap = null;
                obj.ProductVersieCyclus = null;

                await Service.Delete(obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
