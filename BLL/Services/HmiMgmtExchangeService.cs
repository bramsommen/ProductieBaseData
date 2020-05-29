using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using ModelsBaseData;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class HmiMgmtExchangeService : IHmiMgmtExchangeService
    {
        // PROPERTIES
        private readonly IHmiMgmtExchangeRepository repository;
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public HmiMgmtExchangeService(IHmiMgmtExchangeRepository _repository, ProductieBaseDataContext db)
        {
            repository = _repository;
            DB = db;
        }

        // CREATE
        public async Task Create(HmiMgmtExchange obj)
        {
            try
            {
                List<HmiMgmtExchange> oldItems = await this.GetFromMachine(obj.Machine);
                oldItems= oldItems.Where(x => x.Naam.Equals(obj.Naam)).ToList();

                if (oldItems.Count > 0)
                {
                    throw new Exception("Er bestaat reeds een item met deze naam");
                }

                // Save Tot DB
                repository.Create(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // READ
        public async Task<HmiMgmtExchange> GetFromID(long ID)
        {
            try
            {
                HmiMgmtExchange tmpResult = await repository.GetFromID(ID);


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<HmiMgmtExchange> GetFromMachineNaam(string machine, string naam)
        {
            try
            {
                HmiMgmtExchange tmpResult = await repository.GetFromMachineNaam(machine, naam);


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<HmiMgmtExchange>> GetFromMachine(string machine)
        {
            try
            {
                List<HmiMgmtExchange> tmpResult = await repository.GetFromMachine(machine);


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // UPDATE
        public async Task Update(HmiMgmtExchange obj)
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

        public async Task UpdateValue(HmiMgmtExchange obj)
        {
            try
            {
                HmiMgmtExchange hme = await this.GetFromMachineNaam(obj.Machine, obj.Naam);

                if (hme == null)
                {
                    throw new Exception("Geen object gevonden met overeenkomstige parameters");
                }

                // Pas value aan
                hme.Value = obj.Value;

                // Save naar DB
                await DB.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE
        public async Task Delete(HmiMgmtExchange obj)
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
