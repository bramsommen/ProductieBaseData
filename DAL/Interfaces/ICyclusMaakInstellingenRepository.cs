using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
    public interface ICyclusMaakInstellingenRepository
    {
        void Create(CyclusMaakInstelling obj);

        Task<CyclusMaakInstelling> GetFromId(long CyclusMaakInstellingID);

        Task<List<CyclusMaakInstelling>> GetFrom (long cyclusID);

        Task<List<CyclusMaakInstelling>> GetFromMachineOnderdeel(long machineOnderdeelID);

        void Update(CyclusMaakInstelling obj);

        void Delete(CyclusMaakInstelling obj);
    }
}
