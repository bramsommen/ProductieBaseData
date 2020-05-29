using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
    public interface IHmiMgmtExchangeRepository
    {
        void Create(HmiMgmtExchange obj);

        Task<HmiMgmtExchange> GetFromID(long ID);

        Task<HmiMgmtExchange> GetFromMachineNaam(string machine, string naam);

        Task<List<HmiMgmtExchange>> GetFromMachine(string machine);

        void Update(HmiMgmtExchange obj);

        void Delete(HmiMgmtExchange obj);
    }
}
