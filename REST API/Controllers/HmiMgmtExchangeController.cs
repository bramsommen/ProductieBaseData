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
    public class HmiMgmtExchangeController : ControllerBase
    {
        IHmiMgmtExchangeService Service;

        public HmiMgmtExchangeController(IHmiMgmtExchangeService service)
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
        public async Task Post([FromBody] HmiMgmtExchange obj)
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
        public async Task<HmiMgmtExchange> Get(string strHmiMgmtExchangeID)
        {
            try
            {
                long.TryParse(strHmiMgmtExchangeID, out long hmiMgmtExchangeID);

                return await Service.GetFromID(hmiMgmtExchangeID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetFromMachineNaam")]
        public async Task<HmiMgmtExchange> GetFromMachineNaam(string machine, string naam)
        {
            try
            {

                return await Service.GetFromMachineNaam(machine, naam);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetFromMachine")]
        public async Task<IEnumerable<HmiMgmtExchange>> GetFromMachine(string machine)
        {
            try
            {

                return await Service.GetFromMachine(machine);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] HmiMgmtExchange obj)
        {
            try
            {
                await Service.Update(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // UPDATE
        [HttpPut("UpdateValue")]
        public async Task UpdateValue([FromBody] HmiMgmtExchange obj)
        {
            try
            {
                await Service.UpdateValue(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // DELETE:
        [HttpDelete()]
        public async Task Delete([FromBody] HmiMgmtExchange obj)
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
