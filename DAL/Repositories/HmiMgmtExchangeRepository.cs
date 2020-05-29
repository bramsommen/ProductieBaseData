using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL
{
    public class HmiMgmtExchangeRepository : IHmiMgmtExchangeRepository
    {

        // PROPERTIES
        ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public HmiMgmtExchangeRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(HmiMgmtExchange obj)
        {
            try
            {
                DB.HmiMgmtExchange.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HmiMgmtExchange> GetFromID(long ID)
        {
            try
            {
                return await DB.HmiMgmtExchange.Where(x => x.Id.Equals(ID)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<HmiMgmtExchange>  GetFromMachineNaam(string machine, string naam)
        {
            try
            {
                return await DB.HmiMgmtExchange.Where(x => x.Machine.Equals(machine) && x.Naam.Equals(naam)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<HmiMgmtExchange>> GetFromMachine(string machine)
        {
            try
            {              
                return await DB.HmiMgmtExchange.Where(x => x.Machine.Equals(machine)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // UPDATE
        public void Update(HmiMgmtExchange obj)
        {
            try
            {
                DB.HmiMgmtExchange.Update(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(HmiMgmtExchange obj)
        {
            try
            {
                DB.HmiMgmtExchange.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
