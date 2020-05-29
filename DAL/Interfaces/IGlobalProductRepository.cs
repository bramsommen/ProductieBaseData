using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL.Interfaces
{
    public interface IGlobalProductRepository
    {
        void Create(GlobalProduct obj);

        Task<GlobalProduct> GetFromArtikelCode(string artikelCode);

        Task<List<GlobalProduct>> GetAll();

        void Update(GlobalProduct obj);

        void Delete(GlobalProduct obj);
    }
}
