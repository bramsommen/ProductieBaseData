using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModelsBaseData;
using System.Threading.Tasks;

namespace DAL
{
    public class CyclusTypeRepository : ICyclusTypeRepository
    {
        // PROPERTIES
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public CyclusTypeRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(CyclusType obj)
        {
            try
            {
                DB.CyclusType.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<List<CyclusType>> GetFromPoolNaam(long machineOnderdeelID)
        {
            try
            {
                return await DB.CyclusType.Include(x=>x.MachineOnderdeel).Where(x => x.MachineOnderdeel.Id.Equals(machineOnderdeelID)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // UPDATE
        public void Update(CyclusType obj)
        {
            try
            {
                DB.CyclusType.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(CyclusType obj)
        {
            try
            {
                DB.CyclusType.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
