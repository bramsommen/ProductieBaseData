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
    public class CyclusTypeController : ControllerBase
    {
        ICyclusTypeService service;

        public CyclusTypeController(ICyclusTypeService _service)
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
        public async Task Post([FromBody] CyclusType obj)
        {
            try
            {
                await service.Create(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // READ
        [HttpGet]
        public async Task<IEnumerable<CyclusType>> Get(string strMachineOnderdeelID)

        {
            try
            {
                long.TryParse(strMachineOnderdeelID, out long machineOnderdeelID);

                return await service.GetFromPoolNaam(machineOnderdeelID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] CyclusType obj)
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
        public async Task Delete([FromBody] CyclusType obj)
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
