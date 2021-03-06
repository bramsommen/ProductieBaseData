﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public class MaakInstellingenService : IMaakInstellingenService
    {
        // PROPERTIES
        private readonly IMaakInstellingenRepository repository;
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public MaakInstellingenService(IMaakInstellingenRepository _repository, ProductieBaseDataContext db)
        {
            repository = _repository;
            DB = db;
        }

        // CREATE
        public async Task Create(MaakInstelling obj)
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
        public async Task<List<MaakInstelling>> GetFromMachineOnderdeel(long machineOnderdeelID)
        {
            try
            {
                List<MaakInstelling> tmpResult = await repository.GetFromPoolNaam(machineOnderdeelID);

                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // UPDATE
        public async Task Update(MaakInstelling obj)
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
        public async Task Delete(MaakInstelling obj)
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
