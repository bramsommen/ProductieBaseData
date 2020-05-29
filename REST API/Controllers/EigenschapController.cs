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
    public class EigenschapController : ControllerBase
    {
        IEigenschapService Service;

        public EigenschapController(IEigenschapService service)
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
        public async Task Post([FromBody] Eigenschap obj)
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
        public async Task<IEnumerable<Eigenschap>> Get(string strMachineOnderdeelID)
        {
            try
            {
                long.TryParse(strMachineOnderdeelID, out long machineOnderdeelID);

                return await Service.GetFromMachineOnderdeel(machineOnderdeelID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // READ
        [HttpGet("GetFromPoolNaamType")]
        public async Task <IEnumerable<Eigenschap>> GetFromPoolNaamType(string strMachineOnderdeelID, string strdataType)
        {
            try
            {
                long.TryParse(strMachineOnderdeelID, out long machineOnderdeelID);

                return await Service.GetFromMachineOnderdeelType(machineOnderdeelID, strdataType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] Eigenschap obj)
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
        public async Task Delete([FromBody] Eigenschap obj)
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
