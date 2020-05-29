using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICyclusTypeService
    {
        Task Create(CyclusType obj);

        Task<List<CyclusType>> GetFromPoolNaam(long machineOnderdeelID);

        Task Update(CyclusType obj);

        Task Delete(CyclusType obj);
    }
}
