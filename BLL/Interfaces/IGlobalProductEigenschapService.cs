using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL.Interfaces
{
    public interface IGlobalProductEigenschapService
    {
        Task Create(GlobalProductEigenschap obj);

        Task AddEigenschapFromMachineonderdeel(string artikelCode, long machineonderdeeliD);

        Task<GlobalProductEigenschap> GetFromID(long ID);

        Task<List<GlobalProductEigenschap>> GetFromArtikelCode(string artikelCode);

        Task Update(GlobalProductEigenschap obj);

        Task Delete(GlobalProductEigenschap obj);
    }
}