using Business.Interface;
using System.Collections.Generic;
using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using Warrior.Handlers.Enums;
using Data.Interface;
using Business.Adapters;


namespace Business.Implementation
{
    public class PipaService : IPipaService
    {
        private IPipaRepository pipa_repository;

        public PipaService(IPipaRepository pipa_repository)
        {
            this.pipa_repository = pipa_repository;
        }

        //Create Pipa con sus tanques
        public TransactionResult create(PipaVo pipa_vo)
        {
            //Pipa pipa = PipaAdapter.voToObject(pipa_vo);
            //return pipa_repository.create(pipa);

            Pipa pipa = PipaAdapter.voToObject(pipa_vo);

            int id = pipa_repository.create(pipa);

            if (id > 0)
            {
                foreach (TanqueVo dvo in pipa_vo.tanques)
                {
                    dvo.pipa_id = id;
                    var tr2 = TransactionResult.CREATED;

                    tr2 = pipa_repository.createTanque(TanqueAdapter.voToObject(dvo));
                    if (tr2 != TransactionResult.CREATED)
                    {
                        return tr2;
                    }
                }
                return TransactionResult.CREATED;
            }
            return TransactionResult.ERROR;
        }

        //Delete Pipa
        public TransactionResult delete(int id)
        {
            pipa_repository.deleteTanquesByIdPipa(id);
            return pipa_repository.delete(id);
        }

        //Obtener detalle de pipa
        public Pipa detail(int id)
        {
            Pipa pipa = pipa_repository.detail(id);
            pipa.tanques = pipa_repository.getAllTanquesByIdPipa(pipa.id);
            return pipa;
        }

        public IList<Pipa> getAll()
        {
            IList<Pipa> pipas = pipa_repository.getAll();

            foreach (Pipa p in pipas)
            {
                p.tanques = pipa_repository.getAllTanquesByIdPipa(p.id);
            }

            return pipas;
        }

        public TransactionResult update(PipaVo pipa_vo)
        {
            IList<Tanque> tanquesLast = pipa_repository.getAllTanquesByIdPipa(pipa_vo.id);

            pipa_repository.deleteTanquesByIdPipa(pipa_vo.id);

            foreach (TanqueVo dvo in pipa_vo.tanques)
            {
                dvo.pipa_id = pipa_vo.id;

                foreach(Tanque t in tanquesLast)
                {
                    if(dvo.combustible_id == t.combustible.id)
                    {
                        dvo.litros = t.litros;
                        break;
                    }
                }

                var tr2 = TransactionResult.CREATED;

                tr2 = pipa_repository.createTanque(TanqueAdapter.voToObject(dvo));
                if (tr2 != TransactionResult.CREATED)
                {
                    return tr2;
                }
            }

            return pipa_repository.update(PipaAdapter.voToObject(pipa_vo));
        }
    }
}