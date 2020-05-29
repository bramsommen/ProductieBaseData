using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
  public interface IProductVersieRepository
    {
        void Create(ProductVersie obj);

        Task<ProductVersie> GetFrom(long productVersieID);

        Task<List<ProductVersie>> GetFromProduct(long productID);

        void Update(ProductVersie obj);

        void Delete(ProductVersie obj);
    }
}
