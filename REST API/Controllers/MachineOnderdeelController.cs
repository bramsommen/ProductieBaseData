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
    public class MachineOnderdeelController : ControllerBase
    {
        IMachineOnderdeelService Service;

        public MachineOnderdeelController(IMachineOnderdeelService _service)
        {
            try
            {
                Service = _service;
            }
            catch (Exception ex)
            {

            }
        }


        // CREATE
        [HttpPost]
        public async Task Post([FromBody] MachineOnderdeel obj)
        {
            try
            {
              await  Service.Create(obj);
            }
            catch (Exception ex)
            {

            }
        }

        // READ
        [HttpGet]
        public async Task<IEnumerable<MachineOnderdeel>> Get(string machine)
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

        [HttpGet("GetAll")]
        public async Task<IEnumerable<MachineOnderdeel>> GetAll()
        {
            try
            {
                return await Service.GetAll();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] MachineOnderdeel obj)
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
        public async Task Delete([FromBody] MachineOnderdeel obj)
        {
            try
            {
              await  Service.Delete(obj);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
