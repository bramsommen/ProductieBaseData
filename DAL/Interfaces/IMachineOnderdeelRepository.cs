using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
   public interface IMachineOnderdeelRepository
    {
        void Create(MachineOnderdeel obj);

        Task<MachineOnderdeel> GetFromID(long ID);

        Task<List<MachineOnderdeel>> GetFromMachine(string machine);

        Task<List<MachineOnderdeel>> GetAll();

        void Update(MachineOnderdeel obj);

        void Delete(MachineOnderdeel obj);
    }
}
