using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
    public interface IEigenschapService
    {
        Task Create(Eigenschap obj);

        Task<List<Eigenschap>> GetFromMachineOnderdeel(long machineOnderdeelID);

        Task<List<Eigenschap>> GetFromMachineOnderdeelType(long machineOnderdeelID, string strdataType);

        Task Update(Eigenschap obj);

        Task Delete(Eigenschap obj);
    }
}
