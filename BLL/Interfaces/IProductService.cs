using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
    public interface IProductService
    {
        Task Create(Product obj);

        Task Copy(long productID);

        Task<List<Product>> GetFrom(long MachineOnderdeelId);

        Task<Product> GetFromID(long productID);

        Task<Product> GetFromArtikelCode(long machineOnderdeelID,string artikelCode);

        Task<Product> GetLaatsteVersie(long machineOnderdeelID, string artikelCode);

        Task Update(Product obj);

        Task Delete(Product obj);
    }
}
