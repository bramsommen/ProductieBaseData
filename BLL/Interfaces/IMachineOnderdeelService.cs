using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
  public  interface IMachineOnderdeelService
    {
        Task Create(MachineOnderdeel obj);

        Task<MachineOnderdeel> GetFromID(long ID);

        Task<List<MachineOnderdeel>> GetFromMachine(string machine);

        Task<List<MachineOnderdeel>> GetAll();

        Task Update(MachineOnderdeel obj);

        Task Delete(MachineOnderdeel obj);
    }
}
