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
    public class ProductVersieCyclusController : ControllerBase
    {
        IProductVersieCyclusService Service;

        public ProductVersieCyclusController(IProductVersieCyclusService service)
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
        public async Task Post([FromBody] ProductVersieCyclus obj)
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
        public async Task<IEnumerable<ProductVersieCyclus>> Get(string strVersieID)
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
        public async Task Put([FromBody] ProductVersieCyclus obj)
        {
            try
            {
              await  Service.Update(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // DELETE:
        [HttpDelete()]
        public async Task Delete([FromBody] ProductVersieCyclus obj)
        {
            try
            {
            await    Service.Delete(obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
