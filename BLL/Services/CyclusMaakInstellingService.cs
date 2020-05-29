using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL;
using ModelsBaseData;
using System.Threading.Tasks;

namespace BLL
{
    public class CyclusMaakInstellingService : ICyclusMaakInstellingService
    {
        // PROPERTIES
        private readonly ICyclusMaakInstellingenRepository repository;
        private readonly ProductieBaseDataContext DB;

        // CONSTRUCTOR
        public CyclusMaakInstellingService(ICyclusMaakInstellingenRepository _repository, ProductieBaseDataContext db)
        {
            repository = _repository;
            DB = db;
        }

        // CREATE
        public async Task Create(CyclusMaakInstelling obj)
        {
            try
            {
                repository.Create(obj);
                await DB.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // READ
        public async Task<CyclusMaakInstelling> GetFromId(long CyclusMaakInstellingID)
        {
            try
            {
                CyclusMaakInstelling tmpResult = await repository.GetFromId(CyclusMaakInstellingID);

                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CyclusMaakInstelling>> GetFrom(long cyclusID)
        {
            try
            {
                List<CyclusMaakInstelling> tmpResult = await repository.GetFrom(cyclusID);

                tmpResult = tmpResult.OrderBy(x => x.Stap).ThenBy(x => x.ChildStap).ToList();


                return tmpResult; ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CyclusMaakInstelling>> GetFromMachineOnderdeel(long machineOnderdeelID)
        {
            try
            {
                List<CyclusMaakInstelling> tmpResult = await repository.GetFrom(machineOnderdeelID);
                tmpResult = tmpResult.OrderBy(x => x.Stap).ThenBy(x => x.ChildStap).ToList();



                return tmpResult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // SWap Stap
        // Omwisselen van plaats in volgorder - Property "STAP
        public async Task SwapStap(long cyclusStap1, long cyclusStap2)
        {
            try
            {
                // STAP ID 1
                CyclusMaakInstelling cmiA = await this.GetFromId(cyclusStap1);
                int stap1 = cmiA.Stap;
                // Opzoeken of er meerdere CyclusStappen zijn met dezelfde stap nummer
                List<CyclusMaakInstelling> lstCmiA = await this.GetFrom(cmiA.CyclusId);
                lstCmiA = lstCmiA.Where(x => x.Stap.Equals(cmiA.Stap)).ToList();


                // STAP ID 2
                CyclusMaakInstelling cmiB = await this.GetFromId(cyclusStap2);
                int stap2 = cmiB.Stap;
                List<CyclusMaakInstelling> lstCmiB = await this.GetFrom(cmiB.CyclusId);
                lstCmiB = lstCmiB.Where(x => x.Stap.Equals(cmiB.Stap)).ToList();

                if (stap1.Equals(stap2))
                // Als bijdeze stappen hetzelfde zijn dan wild dat zeggen dat er gezocht moet worden naar een nieuwe parent
                {
                    // Zoek of er een grotere stap is
                    List<CyclusMaakInstelling> tmp = await this.GetFrom(cmiA.CyclusId);
                    tmp = tmp.Where(x => x.Stap > cmiA.Stap).ToList();

                    if (tmp.Count != 0)
                    // Er is een item gevonden met een grotere stap
                    {

                        cmiB = await this.GetFromId(tmp[0].Id);
                        stap2 = cmiB.Stap;
                        lstCmiB = await this.GetFrom(cmiB.CyclusId);
                        lstCmiB = lstCmiB.Where(x => x.Stap.Equals(cmiB.Stap)).ToList();
                    }
                    else
                    {
                        return;
                    }

                }

                // UPDATE ALLE STAPPEN MAT ID xx 1
                foreach (CyclusMaakInstelling item in lstCmiA)
                {
                    item.Stap = stap2;

                    // Update Database
                    repository.Update(item);
                    await DB.SaveChangesAsync();
                }


                // UPDATE ALLE STAPPEN MET ID xx 2
                foreach (CyclusMaakInstelling item in lstCmiB)
                {
                    item.Stap = stap1;

                    // Update Database
                    repository.Update(item);
                    await DB.SaveChangesAsync();
                }

                // Als alles Geswapt is, order de volgorde nummers
                await UpdateStapVolgordeCyclusMaakInstellingenen(cmiA.CyclusId);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // Attach
        public async Task Attach(long cyclusMaakInStellingID)
        // Pin / Unpin item aan bovenliggend item

        {
            try
            {
                // Object ophalen
                CyclusMaakInstelling obj0 = null;
                CyclusMaakInstelling refObj = await this.GetFromId(cyclusMaakInStellingID);
                CyclusMaakInstelling obj2 = null;
                CyclusMaakInstelling obj3 = null;

                // Lijst opmaken van maak instelllingen voor aangegeven cyclus
                List<CyclusMaakInstelling> lst = await this.GetFrom(refObj.CyclusId);
                lst = lst.OrderBy(x => x.Stap).ThenBy(x => x.ChildStap).ToList();

                int indexRefObject = lst.FindIndex(x => x.Id.Equals(refObj.Id));

                int hoogsteStap = lst[lst.Count - 1].Stap; // Actueel hoogste stap in lijst

                // Object 0 inlezen
                if (indexRefObject > 0)
                // Er is nog een object voor mij
                {
                    obj0 = lst[indexRefObject - 1];
                }

                // Object 2 inlezen
                if (indexRefObject < (lst.Count - 1))
                // Er is nog een object achter mij
                {
                    obj2 = lst[indexRefObject + 1];
                }

                // Object 3 inlezen
                if (indexRefObject < (lst.Count - 2))
                // Er is nog een object achter mij
                {
                    obj3 = lst[indexRefObject + 2];
                }


                switch (refObj.ChildStap)
                {
                    case 0:
                        // Ik ben een vrijstaand item

                        if (obj0 != null)
                        {
                            if (obj0.ChildStap == 0)
                            // Als het bovenliggende item ook blanco is, maak er dan een parent van
                            {
                                obj0.ChildStap = 1;
                            }

                            // Maak van mezelf een kind
                            refObj.Stap = obj0.Stap;
                            refObj.ChildStap = 2;
                        }

                        break;
                    case 1:
                        // Ik ben een parent item

                        if (obj2 != null)
                        // Maak van mijn eerstvolgend kind een parent
                        {
                            if (refObj.Stap == obj2.Stap)
                            {
                                if (obj3 != null)
                                {
                                    if (obj2.Stap != obj3.Stap)
                                    // Volgende stap is de laatste stap, dus maak hiervan een blanco item
                                    {
                                        obj2.ChildStap = 0;
                                    }
                                    else
                                    {
                                        obj2.ChildStap = 1;
                                    }

                                }
                                else
                                {
                                    obj2.ChildStap = 0;
                                }
                            }
                        }

                        // Voeg mij achteraan toe aan de lijst
                        refObj.Stap = hoogsteStap + 1;
                        refObj.ChildStap = 0;

                        break;
                    case 2:
                        // Ik ben een kind item

                        // Ben ik het laatste kind?
                        if (obj0 != null)
                        // Is het bovenliggend object mijn parant?
                        {
                            if (obj0.Stap == refObj.Stap && obj0.ChildStap == 1)
                            // Bovenliggend object is mijn parant
                            {
                                if (obj2 != null)
                                {
                                    if (obj2.Stap != refObj.Stap)
                                    // Het volgende item is hoort niet bij mij
                                    // Ik ben het laatste kind
                                    {                                                                    // Reset parent status
                                        obj0.ChildStap = 0;
                                    }
                                }
                            }
                        }

                        // Unpin mij van groep
                        // Voeg mij achteraan toe aan de lijst
                        refObj.Stap = hoogsteStap + 1;
                        refObj.ChildStap = 0;


                        break;
                    default:
                        break;
                }


                //
                //
                // Update Database OBJ 0
                if (obj0 != null)
                {
                    repository.Update(obj0);
                    await DB.SaveChangesAsync();
                }

                if (refObj != null)
                {
                    // Update Database REF OBJ
                    repository.Update(refObj);
                    await DB.SaveChangesAsync();
                }

                if (obj2 != null)
                {
                    // Update Database OBJ 2
                    repository.Update(obj2);
                    await DB.SaveChangesAsync();
                }

                // Als alles is bijgewerkt, order de volgorde nummers
                await UpdateStapVolgordeCyclusMaakInstellingenen(refObj.CyclusId);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // UPDATE
        public async Task Update(CyclusMaakInstelling obj)
        {
            try
            {
                repository.Update(obj);
                await DB.SaveChangesAsync();

                await this.UpdateStapVolgordeCyclusMaakInstellingenen(obj.CyclusId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // DELETE
        public async Task Delete(CyclusMaakInstelling obj)
        {
            try
            {
                if (obj.ChildStap > 0)
                // Als deze stap een gepolled item is, verwijder deze dan eerst uit de lsijt
                {
                    await this.Attach(obj.Id);
                }

                // Verwijder item
                repository.Delete(obj);
                await DB.SaveChangesAsync();

                // Als alles Geswapt is, order de volgorde nummers
                await UpdateStapVolgordeCyclusMaakInstellingenen(obj.CyclusId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateStapVolgordeCyclusMaakInstellingenen(long cyclusID)
        // Volgorde bijwerken van cyclus maak instellingen
        {
            // Ophalen van actuele volgorder
            List<CyclusMaakInstelling> lstOudeVolgorde = await this.GetFrom(cyclusID);

            int logicNumberCount = 1; // Logische nummering
            int subStapNummer = 1;

            for (int index = 0; index < lstOudeVolgorde.Count; index++)
            // Pas de uitvoervolgorde aan, naar een logische nummering
            {
                CyclusMaakInstelling pcmi0 = null;
                CyclusMaakInstelling pcmi1 = lstOudeVolgorde[index];


                if (index > 0)
                // Vanaf het 2e record
                {
                    // Gef voorliggend object
                    pcmi0 = lstOudeVolgorde[index - 1];

                    if (pcmi1.Stap != pcmi0.Stap)
                    // Als de uitvoervolgorde verschillend is, tel dan een nieuw logish nummer bij
                    {
                        logicNumberCount++;

                        // Reset Substap NUmmer
                        subStapNummer = 1;
                    }
                    else
                    {
                        // Tel één substap er bij
                        subStapNummer++;
                    }
                }

                pcmi1.ChildStapVolgorde = subStapNummer;
                pcmi1.sortTmp = logicNumberCount;
            }



            foreach (CyclusMaakInstelling item in lstOudeVolgorde)
            {
                item.Stap = item.sortTmp;
            }

            await DB.SaveChangesAsync();

        }
    }
}
