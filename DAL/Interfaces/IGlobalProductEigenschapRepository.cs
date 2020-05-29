using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;
namespace DAL.Interfaces
{
    public interface IGlobalProductEigenschapRepository
    {
        void Create(GlobalProductEigenschap obj);

        Task<GlobalProductEigenschap> GetFromID(long ID);

        Task<List<GlobalProductEigenschap>> GetFromArtikelCode(string artikelCode);

        void Update(GlobalProductEigenschap obj);

        void Delete(GlobalProductEigenschap obj);
    }
}
