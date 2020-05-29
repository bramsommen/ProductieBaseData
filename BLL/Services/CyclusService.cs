using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public class CyclusService : ICyclusService
    {
        // PROPERTIES
        private readonly ICyclusRepository repository;
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public CyclusService(ICyclusRepository _repository, ProductieBaseDataContext db)
        {
            repository = _repository;
            DB = db;
        }

        // CREATE
        public async Task Create(Cyclus obj)
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

        public async Task Clone(Cyclus obj)
        // Dupliceren van Cylus
        {
            try
            {
                // Ophalen van object
                Cyclus completeCylus = await this.GetFromID(obj.Id);

                Cyclus newCyclus = new Cyclus();
                newCyclus.Id = 0;
                newCyclus.MachineOnderdeelId = obj.MachineOnderdeelId;
                newCyclus.Naam = obj.Naam + "(COPY)";

                newCyclus.CyclusTypeId = obj.CyclusTypeId;
                newCyclus.CyclusType = null;

                foreach (CyclusMaakInstelling item in completeCylus.CyclusMaakInstelling)
                {
                    CyclusMaakInstelling cmi = new CyclusMaakInstelling();
                    cmi.Id = 0;
                    cmi.CyclusId = 0;
                    cmi.MaakInstellingId = item.MaakInstellingId;
                    cmi.Stap = item.Stap;
                    cmi.ChildStap = item.ChildStap;

                    cmi.ProductEigenschapId = item.ProductEigenschapId;
                    cmi.ProductEigenschap = null;

                    cmi.StaticWaarde = item.StaticWaarde;
                    cmi.Check = item.Check;

                    // Voeg nieuwe maak instelling toe
                    newCyclus.CyclusMaakInstelling.Add(cmi);
                }

                // Save To DB
                await this.Create(newCyclus);


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // READ
        public async Task<Cyclus> GetFromID(long CycleID)
        {
            try
            {
                return await repository.GetFromID(CycleID);
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
                List<Cyclus> tmpResult = await repository.GetFromMachineOnderdeel(machineOnderdeelID);

                // Order op type
                tmpResult = tmpResult.OrderBy(x => x.CyclusType.Naam).ThenBy(x => x.Naam).ToList();

                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Cyclus>> GetFromCyclusType(long cyclusType)
        {
            try
            {
                List<Cyclus> tmpResult = await repository.GetFromMachineOnderdeelType(cyclusType);


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // UPDATE
        public async Task Update(Cyclus obj)
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
        public async Task Delete(Cyclus obj)
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

