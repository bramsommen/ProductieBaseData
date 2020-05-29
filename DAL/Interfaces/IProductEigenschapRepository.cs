using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
    public interface IProductEigenschapRepository
    {
        void Create(ProductEigenschap obj);

        Task<List<ProductEigenschap>> GetFrom(long versieID);

        void Update(ProductEigenschap obj);

        void Delete(ProductEigenschap obj);
    }
}
