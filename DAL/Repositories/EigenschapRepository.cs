using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL
{
    public class EigenschapRepository : IEigenschapRepository
    {

        // PROPERTIES
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public EigenschapRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(Eigenschap obj)
        {
            try
            {
                DB.Eigenschap.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<List<Eigenschap>> GetFromPoolNaam(long machineOnderdeelID)
        {
            try
            {
                return await DB.Eigenschap.Include(x=>x.MachineOnderdeel).Where(x => x.MachineOnderdeel.Id.Equals(machineOnderdeelID)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // UPDATE
        public void Update(Eigenschap obj)
        {
            try
            {
                DB.Eigenschap.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(Eigenschap obj)
        {
            try
            {
                DB.Eigenschap.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
