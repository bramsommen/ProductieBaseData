using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace DAL
{
    public class MaakInstellingenRepository : IMaakInstellingenRepository
    {

        // PROPERTIES
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public MaakInstellingenRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(MaakInstelling obj)
        {
            try
            {
                DB.MaakInstelling.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<List<MaakInstelling>> GetFromPoolNaam(long machineOnderdeelID)
        {
            try
            {
                return await DB.MaakInstelling.Include(x=>x.MachineOnderdeel).Where(x => x.MachineOnderdeel.Id.Equals(machineOnderdeelID)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        // UPDATE
        public void Update(MaakInstelling obj)
        {
            try
            {
                DB.MaakInstelling.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(MaakInstelling obj)
        {
            try
            {
                DB.MaakInstelling.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

