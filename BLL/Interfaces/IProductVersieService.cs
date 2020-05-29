using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
    public interface IProductVersieService
    {
        Task Create(ProductVersie obj);

        Task CreateFromTemplate(Product obj);

        Task<ProductVersie> NewVersieFromTemplate(Product product, float hoogteVersie);

        Task CreateNewFromOtherVersion(ProductVersie obj);

        Task<ProductVersie> Copy(ProductVersie obj, long productID);

        Task<ProductVersie> GetFrom(long productVersieID);

     Task<string> ValidateVersie(long productVersieID);

        Task Update(ProductVersie obj);

        Task Delete(ProductVersie obj);

        void UpdateVersie(ref ProductVersie pv);
    }
}
