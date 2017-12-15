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
    public class FichaEntregaService : IFichaEntregaService
    {
        private IFichaEntregaRepository ficha_repository;

        public FichaEntregaService(IFichaEntregaRepository ficha_repository)
        {
            this.ficha_repository = ficha_repository;
        }

        //Create Maquinaria
        public TransactionResult create(FichaEntregaRecepcionVo ficha_vo)
        {
            FichaEntregaRecepcion ficha = FichaEntregaAdapter.voToObject(ficha_vo);
            //return maquinaria_repository.create(maquina);

            int id = ficha_repository.create(ficha);

            if (id > 0)
            {
                foreach (DetalleFichaEntregaRecepcionVo dvo in ficha_vo.detalles)
                {
                    dvo.ficha_id = id;
                    var tr2 = TransactionResult.CREATED;

                    tr2 = ficha_repository.createDetalle(DetalleFichaEntregaAdapter.voToObject(dvo));
                    if (tr2 != TransactionResult.CREATED)
                    {
                        return tr2;
                    }
                }
                return TransactionResult.CREATED;
            }
            return TransactionResult.ERROR;
        }

        //Delete Maquinaria
        public TransactionResult delete(int id)
        {
            ficha_repository.deleteDetallesByIdFicha(id);
            return ficha_repository.delete(id);
        }

        //Detail Maquinaria
        public FichaEntregaRecepcion detail(int id)
        {
            FichaEntregaRecepcion ficha = ficha_repository.detail(id);
            ficha.detalles = ficha_repository.getAllDetallesByFichaId(ficha.id);
            return ficha;
        }

        //Traer Todas las máquinas
        public IList<FichaEntregaRecepcion> getAll()
        {
            return ficha_repository.getAll();
        }

        //Actualizar Maquinaria
        public TransactionResult update(FichaEntregaRecepcionVo ficha_vo)
        {
            ficha_repository.deleteDetallesByIdFicha(ficha_vo.id);

            foreach (DetalleFichaEntregaRecepcionVo dvo in ficha_vo.detalles)
            {
                dvo.ficha_id = ficha_vo.id;
                var tr2 = TransactionResult.CREATED;

                tr2 = ficha_repository.createDetalle(DetalleFichaEntregaAdapter.voToObject(dvo));
                if (tr2 != TransactionResult.CREATED)
                {
                    return tr2;
                }
            }

            return ficha_repository.update(FichaEntregaAdapter.voToObject(ficha_vo));
        }
    }
}