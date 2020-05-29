using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public class CyclusTypeService : ICyclusTypeService
    {
        // PROPERTIES
        private readonly ICyclusTypeRepository repository;
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public CyclusTypeService(ICyclusTypeRepository _repository, ProductieBaseDataContext db)
        {
            repository = _repository;
            DB = db;
        }

        // CREATE
        public async Task Create(CyclusType obj)
        {
            try
            {
                repository.Create(obj);
                await DB.SaveChangesAsync();
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
                List<CyclusType> tmpResult = await repository.GetFromPoolNaam(machineOnderdeelID);


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // UPDATE
        public async Task Update(CyclusType obj)
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
        public async Task Delete(CyclusType obj)
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
