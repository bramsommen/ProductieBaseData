using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL.Interfaces
{
   public interface IGlobalProductService
    {
        Task Create(GlobalProduct obj);

        Task CopyFrom(GlobalProduct obj, string _artikelCode);

        Task<GlobalProduct> GetFromArtikelCode(string artikelCode);

        Task<List<GlobalProduct>> GetAll();

        Task Update(GlobalProduct obj);

        Task Delete(GlobalProduct obj);
    }
}