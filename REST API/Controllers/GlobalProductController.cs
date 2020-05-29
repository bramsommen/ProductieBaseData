using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsBaseData;
using BLL;
using BLL.Interfaces;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalProductController : ControllerBase
    {
        IGlobalProductService service;

        public GlobalProductController(IGlobalProductService _service)
        {
            try
            {
                service = _service;
            }
            catch (Exception ex)
            {

            }
        }

        // CREATE
        [HttpPost]
        public async Task Post([FromBody] GlobalProduct obj)
        {
            try
            {
                await service.Create(obj);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPost("CopyFrom")]
        public async Task CopyFrom([FromBody] GlobalProduct obj, string _artikelCode)
        {
            try
            {
                await service.CopyFrom(obj, _artikelCode);
            }
            catch (Exception ex)
            {

            }
        }

        // READ
        [HttpGet("GetFromArtikelCode")]
        public async Task<GlobalProduct> GetFromArtikelCode(string _artikelCode)

        {
            try
            {
                return await service.GetFromArtikelCode(_artikelCode);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<GlobalProduct>> GetAll()

        {
            try
            {

                return await service.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] GlobalProduct obj)
        {
            try
            {
                await service.Update(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // DELETE:
        [HttpDelete()]
        public async Task Delete([FromBody] GlobalProduct obj)
        {
            try
            {
                await service.Delete(obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
