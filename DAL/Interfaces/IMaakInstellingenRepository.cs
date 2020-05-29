using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsBaseData;

namespace DAL
{
    public interface IMaakInstellingenRepository
    {
        void Create(MaakInstelling obj);

        Task<List<MaakInstelling>> GetFromPoolNaam(long machineOnderdeelID);

        void Update(MaakInstelling obj);

        void Delete(MaakInstelling obj);
    }
}
