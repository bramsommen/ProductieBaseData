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
    public class CyclusController : ControllerBase
    {
        ICyclusService EigenschapService;

        public CyclusController(ICyclusService _RoleTagService)
        {
            try
            {
                EigenschapService = _RoleTagService;
            }
            catch (Exception ex)
            {

            }
        }


        // CREATE
        [HttpPost]
        public async Task Post([FromBody] Cyclus obj)
        {
            try
            {
                await EigenschapService.Create(obj);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPost("Clone")]
        public async Task Clone([FromBody] Cyclus obj)
        {
            try
            {
                await EigenschapService.Clone(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // READ
        [HttpGet]
        public async Task<IEnumerable<Cyclus>> Get(string strMachineOnderdeelID)
        {
            try
            {
                long.TryParse(strMachineOnderdeelID, out long machineOnderdeelID);

                return await EigenschapService.GetFromMachineOnderdeel(machineOnderdeelID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // READ
        [HttpGet("GetFromCyclusType")]
        public async Task<IEnumerable<Cyclus>> GetFromCyclusType(string strCyclusTypeID)
        {
            try
            {
                long.TryParse(strCyclusTypeID, out long cyclusType);

                return await EigenschapService.GetFromCyclusType(cyclusType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] Cyclus obj)
        {
            try
            {
                await EigenschapService.Update(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // DELETE:
        [HttpDelete()]
        public async Task Delete([FromBody] Cyclus obj)
        {
            try
            {
                await EigenschapService.Delete(obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
