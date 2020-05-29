using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
    public interface IProductVersieCyclusRepository
    {
        void Create(ProductVersieCyclus obj);

        Task<List<ProductVersieCyclus>> GetFrom(long versieID);

        Task<ProductVersieCyclus> GetFromID(long productCyclusMaakInstellingenID);

        void Update(ProductVersieCyclus obj);

        void Delete(ProductVersieCyclus obj);
    }
}
