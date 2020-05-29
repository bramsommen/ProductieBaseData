using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICyclusService
    {
        Task Create(Cyclus obj);

        Task Clone(Cyclus obj);

        Task<Cyclus> GetFromID(long CycleID);

        Task<List<Cyclus>> GetFromMachineOnderdeel(long machineOnderdeelID);

        Task<List<Cyclus>> GetFromCyclusType(long cyclusType);

        Task Update(Cyclus obj);

        Task Delete(Cyclus obj);
    }
}
