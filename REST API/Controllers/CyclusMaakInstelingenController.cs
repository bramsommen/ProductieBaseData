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
    public class CyclusMaakInstelingenController : ControllerBase
    {
        ICyclusMaakInstellingService Service;

        public CyclusMaakInstelingenController(ICyclusMaakInstellingService service)
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
        public async Task Post([FromBody] CyclusMaakInstelling obj)
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
        public async Task<IEnumerable<CyclusMaakInstelling>> Get(string strcyclusID)
        {
            try
            {
                long.TryParse(strcyclusID, out long cyclusID);

                List<CyclusMaakInstelling> tmp = await Service.GetFrom(cyclusID);

                return tmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] CyclusMaakInstelling obj)
        {
            try
            {
                await Service.Update(obj);
            }
            catch (Exception ex)
            {

            }
        }


        [HttpPut("SwapStap")]
        public async Task SwapStap(string strCyclusStap1, string strCyclusStap2)
        {
            try
            {
                long.TryParse(strCyclusStap1, out long cyclusStap1);
                long.TryParse(strCyclusStap2, out long cyclusStap2);

                await Service.SwapStap(cyclusStap1, cyclusStap2);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPut("Attach")]
        public async Task Attach(string strCyclusMaakInStellingID)
        {
            try
            {
                long.TryParse(strCyclusMaakInStellingID, out long cyclusMaakInStellingID);

                await Service.Attach(cyclusMaakInStellingID);
            }
            catch (Exception ex)
            {

            }
        }

        // DELETE:
        [HttpDelete()]
        public async Task Delete([FromBody] CyclusMaakInstelling obj)
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
