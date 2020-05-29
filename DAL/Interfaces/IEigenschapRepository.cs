using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
    public interface IEigenschapRepository
    {
        void Create(Eigenschap obj);

        Task<List<Eigenschap>> GetFromPoolNaam(long machineOnderdeelID);

        void Update(Eigenschap obj);

        void Delete(Eigenschap obj);
    }
}
