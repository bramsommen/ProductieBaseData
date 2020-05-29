using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
   public interface IProductRepository
    {
        void Create(Product obj);

        Task<List<Product>> GetFrom(long machineOnderdeelID);

        Task<Product> GetFromID(long productID);

        Task<Product> GetFromArtikelCode(long machineOnderdeelID, string artikelCode);

        void Update(Product obj);

        void Delete(Product obj);
    }
}
