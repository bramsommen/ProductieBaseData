using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
    public interface ICyclusMaakInstellingService
    {
        Task Create(CyclusMaakInstelling obj);

        Task<CyclusMaakInstelling> GetFromId(long CyclusMaakInstellingID);

        Task<List<CyclusMaakInstelling>> GetFrom(long cyclusID);

        Task<List<CyclusMaakInstelling>> GetFromMachineOnderdeel(long machineOnderdeelID);

        Task SwapStap(long cyclusStap1, long cyclusStap2);

        Task Attach(long cyclusMaakInStellingID);

        Task Update(CyclusMaakInstelling obj);

        Task Delete(CyclusMaakInstelling obj);
    }
}
