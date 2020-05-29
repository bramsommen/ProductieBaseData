using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModelsBaseData;
using System.Threading.Tasks;

namespace DAL
{
    public class CyclusRepository : ICyclusRepository
    {
        // PROPERTIES
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public CyclusRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(Cyclus obj)
        {
            try
            {
                DB.Cyclus.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<Cyclus> GetFromID(long CycleID)
        {
            try
            {
                return await DB.Cyclus
            .Include(x => x.CyclusType)
            .Include(x => x.CyclusMaakInstelling).ThenInclude(x => x.MaakInstelling)
            .Include(x => x.CyclusMaakInstelling).ThenInclude(x => x.ProductEigenschap)
            .Where(x => x.Id.Equals(CycleID)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Cyclus>> GetFromMachineOnderdeel(long machineOnderdeelID)
        {
            try
            {
                return await DB.Cyclus
                    .Include(x => x.CyclusType)
                    .Include(x => x.MachineOnderdeel)

                    .Where(x => x.MachineOnderdeel.Id.Equals(machineOnderdeelID)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Cyclus>> GetFromMachineOnderdeelType(long cyclusType)
        {
            try
            {
                return await DB.Cyclus.Include(x => x.CyclusType).Where(x => x.CyclusType.Id.Equals(cyclusType)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // UPDATE
        public void Update(Cyclus obj)
        {
            try
            {
                DB.Cyclus.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(Cyclus obj)
        {
            try
            {
                DB.Cyclus.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
