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
    public class AbastecimientoService : IAbastecimientoService
    {
        private IAbastecimientoRepository abastecimiento_repository;

        public AbastecimientoService(IAbastecimientoRepository abastecimiento_repository)
        {
            this.abastecimiento_repository = abastecimiento_repository;
        }

        //Create Maquinaria
        public TransactionResult create(AbastecimientoPipaVo abastecimiento_vo)
        {
            AbastecimientoPipa abastecimiento = AbastecimientoAdapter.voToObject(abastecimiento_vo);
            //return maquinaria_repository.create(maquina);

            int id = abastecimiento_repository.create(abastecimiento);

            if (id > 0)
            {
                foreach (DetalleAbastecimientoPipaVo dvo in abastecimiento_vo.detalles)
                {
                    dvo.abastecimiento_id = id;
                    var tr2 = TransactionResult.CREATED;

                    tr2 = abastecimiento_repository.createDetalle(DetalleAbastecimientoAdapter.voToObject(dvo));
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
            abastecimiento_repository.deleteDetallesByIdAbastecimiento(id);
            return abastecimiento_repository.delete(id);
        }

        //Detail Maquinaria
        public AbastecimientoPipa detail(int id)
        {
            AbastecimientoPipa abastecimiento = abastecimiento_repository.detail(id);
            abastecimiento.detalles = abastecimiento_repository.getAllDetallesByAbastecimientoId(abastecimiento.id);
            return abastecimiento;
        }

        //Traer Todas las máquinas
        public IList<AbastecimientoPipa> getAll()
        {
            return abastecimiento_repository.getAll();
        }

        //Actualizar Maquinaria
        public TransactionResult update(AbastecimientoPipaVo abastecimiento_vo)
        {
            abastecimiento_repository.deleteDetallesByIdAbastecimiento(abastecimiento_vo.id);

            foreach (DetalleAbastecimientoPipaVo dvo in abastecimiento_vo.detalles)
            {
                dvo.abastecimiento_id = abastecimiento_vo.id;
                var tr2 = TransactionResult.CREATED;

                tr2 = abastecimiento_repository.createDetalle(DetalleAbastecimientoAdapter.voToObject(dvo));
                if (tr2 != TransactionResult.CREATED)
                {
                    return tr2;
                }
            }

            return abastecimiento_repository.update(AbastecimientoAdapter.voToObject(abastecimiento_vo));
        }
    }
}