using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsBaseData;
using BLL;
using BLL.Interfaces;
using Microsoft.VisualBasic.CompilerServices;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalProductEigenschapController : ControllerBase
    {
        IGlobalProductEigenschapService service;

        public GlobalProductEigenschapController(IGlobalProductEigenschapService _service)
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
        public async Task Post([FromBody] GlobalProductEigenschap obj)
        {
            try
            {
                await service.Create(obj);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPost("AddEigenschapFromMachineonderdeel")]
        public async Task AddEigenschapFromMachineonderdeel(string _artikelCode, string strMachineonderdeeliD)
        {
            try
            {
                long.TryParse(strMachineonderdeeliD, out long machineOnderdeelID);

                await service.AddEigenschapFromMachineonderdeel(_artikelCode, machineOnderdeelID);
            }
            catch (Exception ex)
            {

            }
        }

        // READ
        [HttpGet("GetFromID")]
        public async Task<GlobalProductEigenschap> GetFromID(string strId)

        {
            try
            {
                long.TryParse(strId, out long ID);

                return await service.GetFromID(ID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetFromArtikelCode")]
        public async Task<IEnumerable<GlobalProductEigenschap>> GetFromArtikelCode(string _artikelCode)

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

        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] GlobalProductEigenschap obj)
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
        public async Task Delete([FromBody] GlobalProductEigenschap obj)
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
