using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace BLL
{
    public interface IProductEigenschapService
    {
        Task Create(ProductEigenschap obj);

        Task<List<ProductEigenschap>> GetFrom(long versieID);

        Task Update(ProductEigenschap obj);

        Task Delete(ProductEigenschap obj);
    }
}
