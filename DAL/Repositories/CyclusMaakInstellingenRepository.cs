using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModelsBaseData;
using System.Threading.Tasks;

namespace DAL
{
    public class CyclusMaakInstellingenRepository : ICyclusMaakInstellingenRepository
    {
        // PROPERTIES
       private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public CyclusMaakInstellingenRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(CyclusMaakInstelling obj)
        {
            try
            {
               DB.CyclusMaakInstelling.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<CyclusMaakInstelling> GetFromId(long CyclusMaakInstellingID)
        {
            try
            {
                return await DB.CyclusMaakInstelling
                    .Include(x=>x.MaakInstelling)
                      .Include(x => x.ProductEigenschap)
                      .Where(x => x.Id.Equals(CyclusMaakInstellingID)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<CyclusMaakInstelling>> GetFrom(long cyclusID)
        {
            try
            {
                return await DB.CyclusMaakInstelling
                    .Include(x => x.Cyclus)
                    .Include(x => x.MaakInstelling)
                     .Include(x => x.ProductEigenschap)
                    .Where(x => x.CyclusId.Equals(cyclusID)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<CyclusMaakInstelling>> GetFromMachineOnderdeel (long machineOnderdeelID)
        {
            try
            {
                return await DB.CyclusMaakInstelling
                    .Include(x => x.Cyclus).ThenInclude(x=>x.MachineOnderdeel)
                    .Include(x => x.MaakInstelling)
                    .Include(x => x.ProductEigenschap)

                    .Where(x => x.Cyclus.MachineOnderdeel.Id.Equals(machineOnderdeelID)).ToListAsync();       
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // UPDATE
        public void Update(CyclusMaakInstelling obj)
        {
            try
            {
                DB.CyclusMaakInstelling.Update(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(CyclusMaakInstelling obj)
        {
            try
            {
                 DB.CyclusMaakInstelling.Remove(obj);            
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
