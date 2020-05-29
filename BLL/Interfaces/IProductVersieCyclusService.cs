using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
    public interface IProductVersieCyclusService
    {
        Task Create(ProductVersieCyclus obj);

        Task CreateAll(long productVersieID, long cyclusID);

        Task<List<ProductVersieCyclus>> GetFrom(long versieID);

        Task<ProductVersieCyclus> GetFromID(long productCyclusMaakInstellingenID);

        Task Update(ProductVersieCyclus obj);

        Task Delete(ProductVersieCyclus obj);
    }
}
