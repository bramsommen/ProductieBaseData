using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
    public interface ICyclusTypeRepository
    {
        void Create(CyclusType obj);

        Task<List<CyclusType>> GetFromPoolNaam(long machineOnderdeelID);

        void Update(CyclusType obj);

        void Delete(CyclusType obj);
    }
}
