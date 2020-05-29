using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL
{
    public class MachineOnderdeelRepository : IMachineOnderdeelRepository
    {

        // PROPERTIES
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public MachineOnderdeelRepository(ProductieBaseDataContext dB)
        {
            DB = dB;
        }


        // CREATE
        public void Create(MachineOnderdeel obj)
        {
            try
            {
                DB.MachineOnderdeel.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // READ
        public async Task<MachineOnderdeel> GetFromID(long machineOnderdeelID)
        {
            try
            {
                return await DB.MachineOnderdeel.Where(x => x.Id.Equals(machineOnderdeelID)).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<MachineOnderdeel>> GetFromMachine(string machine)
        {
            try
            {
                return await DB.MachineOnderdeel.Where(x => x.Machine.Equals(machine)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<MachineOnderdeel>> GetAll()
        {
            try
            {
                return await DB.MachineOnderdeel.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // UPDATE
        public void Update(MachineOnderdeel obj)
        {
            try
            {
                DB.MachineOnderdeel.Update(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public void Delete(MachineOnderdeel obj)
        {
            try
            {
                DB.MachineOnderdeel.Remove(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
