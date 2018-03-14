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
    public class MaquinariaService : IMaquinariaService
    {
        private IMaquinaRepository maquinaria_repository;

        public MaquinariaService(IMaquinaRepository maquinaria_repository)
        {
            this.maquinaria_repository = maquinaria_repository;
        }

        //Create Maquinaria
        public TransactionResult create(MaquinariaVo maquina_vo)
        {
            Maquinaria maquina = MaquinariaAdapter.voToObject(maquina_vo);
            //return maquinaria_repository.create(maquina);

            int id = maquinaria_repository.create(maquina);

            if (id > 0)
            {
                foreach (DetalleConsumoMaquinariaVo dvo in maquina_vo.detalles)
                {
                    dvo.maquinaria_id = id;
                    var tr2 = TransactionResult.CREATED;

                    tr2 = maquinaria_repository.createDetalle(DetalleConsumoAdapter.voToObject(dvo));
                    if (tr2 != TransactionResult.CREATED)
                    {
                        return tr2;
                    }
                }

                foreach (CuentaVo cvo in maquina_vo.cuentas)
                {
                    var tr2 = TransactionResult.CREATED;

                    tr2 = maquinaria_repository.createCuenta(CuentaAdapter.voToObject(cvo), id);
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
            maquinaria_repository.deleteDetallesByIdMaquinaria(id);
            return maquinaria_repository.delete(id);
        }

        //Detail Maquinaria
        public Maquinaria detail(int id)
        {
            Maquinaria maq = maquinaria_repository.detail(id);
            maq.detalles = maquinaria_repository.getAllDetallesByMaquinariaId(maq.id);
            maq.cuentas = maquinaria_repository.getAllCuentasByMaquinariaId(maq.id);
            return maq;
        }

        //Traer Todas las máquinas
        public IList<Maquinaria> getAll()
        {
            return maquinaria_repository.getAll();
        }

        //Actualizar Maquinaria
        public TransactionResult update(MaquinariaVo maquina_vo)
        {
            maquinaria_repository.deleteDetallesByIdMaquinaria(maquina_vo.id);

            foreach (DetalleConsumoMaquinariaVo dvo in maquina_vo.detalles)
            {
                dvo.maquinaria_id = maquina_vo.id;
                var tr2 = TransactionResult.CREATED;

                tr2 = maquinaria_repository.createDetalle(DetalleConsumoAdapter.voToObject(dvo));
                if (tr2 != TransactionResult.CREATED)
                {
                    return tr2;
                }
            }

            //Revisamos que la maquinaria ya tenga cuentas, de lo contrario las creamos
            IList<Cuenta> cuentas = maquinaria_repository.getAllCuentasByMaquinariaId(maquina_vo.id);

            if (cuentas.Count > 0 && cuentas != null)
            {

                foreach (CuentaVo cvo in maquina_vo.cuentas)
                {
                    var tr2 = TransactionResult.OK;
                    Cuenta cuentaAux = maquinaria_repository.cuentaDetail(cvo.id);

                    if (cuentaAux == null)
                    {
                        tr2 = maquinaria_repository.createCuenta(CuentaAdapter.voToObject(cvo), maquina_vo.id);
                        if (tr2 != TransactionResult.CREATED)
                        {
                            return tr2;
                        }
                    }
                    else
                    {
                        tr2 = maquinaria_repository.updateCuenta(CuentaAdapter.voToObject(cvo));
                        if (tr2 != TransactionResult.OK)
                        {
                            return tr2;
                        }
                    }
                }
            }
            else
            {
                foreach (CuentaVo cvo in maquina_vo.cuentas)
                {
                    var tr2 = TransactionResult.CREATED;

                    tr2 = maquinaria_repository.createCuenta(CuentaAdapter.voToObject(cvo), maquina_vo.id);
                    if (tr2 != TransactionResult.CREATED)
                    {
                        return tr2;
                    }
                }
            }

            return maquinaria_repository.update(MaquinariaAdapter.voToObject(maquina_vo));
        }
    }
}