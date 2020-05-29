using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public class EigenschapService : IEigenschapService
    {
        // PROPERTIES
        private readonly IEigenschapRepository repository;
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public EigenschapService(IEigenschapRepository _repository, ProductieBaseDataContext db)
        {
            repository = _repository;
            DB = db;
        }

        // CREATE
        public async Task Create(Eigenschap obj)
        {
            try
            {

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
        public async Task<List<Eigenschap>> GetFromMachineOnderdeel(long machineOnderdeelID)
        {
            try
            {
                List<Eigenschap> tmpResult = await repository.GetFromPoolNaam(machineOnderdeelID);

                tmpResult = tmpResult.OrderBy(x => x.Sort).ToList();

                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Eigenschap>> GetFromMachineOnderdeelType(long machineOnderdeelID, string strdataType)
        {
            try
            {
                List<Eigenschap> tmpResult = await this.GetFromMachineOnderdeel(machineOnderdeelID);

                tmpResult = tmpResult.Where(x => x.DataType.Equals(strdataType)).ToList();

                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // UPDATE
        public async Task Update(Eigenschap obj)
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
        public async Task Delete(Eigenschap obj)
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
