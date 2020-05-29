using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public class MachineOnderdeelService : IMachineOnderdeelService
    {
        // PROPERTIES
        private readonly IMachineOnderdeelRepository repository;
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public MachineOnderdeelService(IMachineOnderdeelRepository _repository, ProductieBaseDataContext db)
        {
            repository = _repository;
            DB = db;
        }

        // CREATE
        public async Task Create(MachineOnderdeel obj)
        {
            try
            {

                // Save Tot DB
                repository.Create(obj);
                await DB.SaveChangesAsync();
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

                MachineOnderdeel tmpResult = await repository.GetFromID(machineOnderdeelID);


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<MachineOnderdeel>> GetFromMachine(string machine)
        {
            try
            {
                List<MachineOnderdeel> tmpResult = await repository.GetFromMachine(machine);


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<MachineOnderdeel>> GetAll()
        {
            try
            {
                List<MachineOnderdeel> tmpResult = await repository.GetAll();


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // UPDATE
        public async Task Update(MachineOnderdeel obj)
        {
            try
            {
                repository.Update(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        // DELETE
        public async Task Delete(MachineOnderdeel obj)
        {
            try
            {
                repository.Delete(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
