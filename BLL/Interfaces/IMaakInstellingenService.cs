using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
    public interface IMaakInstellingenService
    {
        Task Create(MaakInstelling obj);

        Task<List<MaakInstelling>> GetFromMachineOnderdeel(long machineOnderdeelID);

        Task Update(MaakInstelling obj);

        Task Delete(MaakInstelling obj);
    }
}
