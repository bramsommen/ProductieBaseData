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
    public class ProductController : ControllerBase
    {
        IProductService Service;

        public ProductController(IProductService service)
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
        public async Task Post([FromBody] Product obj)
        {
            try
            {
                await Service.Create(obj);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpPost("Copy")]
        public async Task Copy(string strProductID)
        {
            try
            {
                long.TryParse(strProductID, out long productID);

                await Service.Copy(productID);
            }
            catch (Exception ex)
            {

            }
        }

        // READ
        [HttpGet]
        public async Task<IEnumerable<Product>> Get(string strmachineOnderdeelID)
        {
            try
            {
                long.TryParse(strmachineOnderdeelID, out long machineOnderdeelID);

                return await Service.GetFrom(machineOnderdeelID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // READ
        [HttpGet("GetfromID")]
        public async Task<Product> GetfromID(string strProductID)
        {
            try
            {
                long.TryParse(strProductID, out long productID);

                return await Service.GetFromID(productID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // READ
        [HttpGet("GetFromArtikelCode")]
        public async Task<Product> GetFromArtikelCode(string strmachineOnderdeelID, string artikelCode)
        {
            try
            {
                long.TryParse(strmachineOnderdeelID, out long machineOnderdeelID);

                return await Service.GetFromArtikelCode(machineOnderdeelID, artikelCode.ToUpper());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetLaatsteVersie")]
        public async Task<Product> GetLaatsteVersie(string strmachineOnderdeelID, string artikelCode)
        {
            try
            {
                long.TryParse(strmachineOnderdeelID, out long machineOnderdeelID);

                return await Service.GetLaatsteVersie(machineOnderdeelID, artikelCode.ToUpper());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // UPDATE
        [HttpPut()]
        public async Task Put([FromBody] Product obj)
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
        public async Task Delete([FromBody] Product obj)
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
