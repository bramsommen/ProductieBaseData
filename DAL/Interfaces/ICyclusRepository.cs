using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
    public interface ICyclusRepository
    {
        void Create(Cyclus obj);

        Task<Cyclus> GetFromID(long CycleID);

        Task<List<Cyclus>> GetFromMachineOnderdeel(long machineOnderdeelID);

        Task<List<Cyclus>> GetFromMachineOnderdeelType(long cyclusType);

        void Update(Cyclus obj);

        void Delete(Cyclus obj);
    }
}
