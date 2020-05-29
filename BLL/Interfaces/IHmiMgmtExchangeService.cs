using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
    public interface IHmiMgmtExchangeService
    {
        Task Create(HmiMgmtExchange obj);

        Task<HmiMgmtExchange> GetFromID(long ID);

        Task<HmiMgmtExchange> GetFromMachineNaam(string machine, string naam);

        Task<List<HmiMgmtExchange>> GetFromMachine(string machine);

        Task Update(HmiMgmtExchange obj);

        Task UpdateValue(HmiMgmtExchange obj);

        Task Delete(HmiMgmtExchange obj);
    }
}
