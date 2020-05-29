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
    public class MaakInstellingController : ControllerBase
    {
        IMaakInstellingenService Service;

        public MaakInstellingController(IMaakInstellingenService service)
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
        public async Task Post([FromBody] MaakInstelling obj)
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
        public async Task<IEnumerable<MaakInstelling>> Get(string strmachineOnderdeelID)
        {
            try
            {
                long.TryParse(strmachineOnderdeelID, out long machineOnderdeelID);


                return await Service.GetFromMachineOnderdeel(machineOnderdeelID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] MaakInstelling obj)
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
        public async Task Delete([FromBody] MaakInstelling obj)
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
