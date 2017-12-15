﻿using Business.Interface;
using System.Collections.Generic;
using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using Warrior.Handlers.Enums;
using Data.Interface;
using Business.Adapters;

namespace Business.Implementation
{
    public class SalidaCombustibleService : ISalidaCombustibleService
    {
        private ISalidaCombustibleRepository salidas_repository;

        public SalidaCombustibleService(ISalidaCombustibleRepository salidas_repository)
        {
            this.salidas_repository = salidas_repository;
        }

        //Create Maquinaria
        public TransactionResult create(SalidaCombustibleVo salida_vo)
        {
            SalidaCombustible salida = SalidaCombustibleAdapter.voToObject(salida_vo);
            //return maquinaria_repository.create(maquina);

            int id = salidas_repository.create(salida);

            if (id > 0)
            {
                foreach (DetalleSalidaCombustibleVo dvo in salida_vo.detalles)
                {
                    dvo.salida_combustible_id = id;
                    var tr2 = TransactionResult.CREATED;

                    tr2 = salidas_repository.createDetalle(DetalleSalidaCombustibleAdapter.voToObject(dvo));
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
            salidas_repository.deleteDetallesByIdSalida(id);
            return salidas_repository.delete(id);
        }

        //Detail Maquinaria
        public SalidaCombustible detail(int id)
        {
            SalidaCombustible salida = salidas_repository.detail(id);
            salida.detalles = salidas_repository.getAllDetallesBySalidaId(salida.id);
            return salida;
        }

        //Traer Todas las máquinas
        public IList<SalidaCombustible> getAll()
        {
            return salidas_repository.getAll();
        }

        //Actualizar Maquinaria
        public TransactionResult update(SalidaCombustibleVo salida_vo)
        {
            salidas_repository.deleteDetallesByIdSalida(salida_vo.id);

            foreach (DetalleSalidaCombustibleVo dvo in salida_vo.detalles)
            {
                dvo.salida_combustible_id = salida_vo.id;
                var tr2 = TransactionResult.CREATED;

                tr2 = salidas_repository.createDetalle(DetalleSalidaCombustibleAdapter.voToObject(dvo));
                if (tr2 != TransactionResult.CREATED)
                {
                    return tr2;
                }
            }

            return salidas_repository.update(SalidaCombustibleAdapter.voToObject(salida_vo));
        }
    }
}