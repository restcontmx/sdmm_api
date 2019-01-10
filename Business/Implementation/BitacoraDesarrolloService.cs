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
    public class BitacoraDesarrolloService : IBitacoraDesarrolloService
    {
        private IBitacoraDesarrolloRepository bitacora_repository;

        public BitacoraDesarrolloService(IBitacoraDesarrolloRepository bitacora_repository)
        {
            this.bitacora_repository = bitacora_repository;
        }

        public TransactionResult create(BitacoraDesarrolloVo bitacora_vo, User user_log)
        {
            if (bitacora_vo.comentarios == null)
            {
                bitacora_vo.comentarios = "";
            }
            if (bitacora_vo.vale_acero == null)
            {
                bitacora_vo.vale_acero = "";
            }
            if (bitacora_vo.vale_explosivos == null)
            {
                bitacora_vo.vale_explosivos = "";
            }

            BitacoraDesarrollo obj = BitacoraDesarrolloAdapter.voToObject(bitacora_vo);
            obj.user = user_log;

            int id = bitacora_repository.create(obj);
            if (id > 0)
            {
                var tr = TransactionResult.CREATED;
                if (bitacora_vo.demoras != null)
                {
                    foreach (DetalleDemoraBitacoraVo dvo in bitacora_vo.demoras)
                    {
                        dvo.bitacora_desarrollo_id = id;
                        dvo.tipo_bitacora = 1;
                        tr = bitacora_repository.createDetalleDemoraBitacora(DetalleDemoraBitacoraAdapter.voToObject(dvo));
                        if (tr != TransactionResult.CREATED)
                        {
                            return tr;
                        }
                    }
                }
                return tr;
            }
            return TransactionResult.ERROR;

            //return bitacora_repository.create(obj);
        }

        public TransactionResult delete(int id)
        {
            return bitacora_repository.delete(id);
        }

        public BitacoraDesarrollo detail(int id)
        {
            BitacoraDesarrollo bit = bitacora_repository.detail(id);
            bit.demoras = bitacora_repository.getDemorasByIdBitacora(bit.id);
            return bit;
        }

        public IList<BitacoraDesarrollo> getAll()
        {
            return bitacora_repository.getAll();
        }

        public IList<BitacoraDesarrollo> getAllBitacoraByIdSupervisor(int user_id)
        {
            return bitacora_repository.getAllBitacoraByIdSupervisor(user_id);
        }

        public TransactionResult update(BitacoraDesarrolloVo bitacora_vo, User user_log)
        {
            if (bitacora_vo.comentarios == null)
            {
                bitacora_vo.comentarios = "";
            }
            bitacora_vo.user_id = user_log.id;
            //Eliminamos los detalles existentes
            bitacora_repository.deleteDetalleDemoraBitacora(bitacora_vo.id);

            //Creamos las demoras otra vez
            if (bitacora_vo.demoras != null)
            {
                var tr = TransactionResult.CREATED;

                if (bitacora_vo.demoras != null)
                {
                    foreach (DetalleDemoraBitacoraVo dvo in bitacora_vo.demoras)
                    {
                        dvo.bitacora_desarrollo_id = bitacora_vo.id;
                        dvo.tipo_bitacora = 1;
                        tr = bitacora_repository.createDetalleDemoraBitacora(DetalleDemoraBitacoraAdapter.voToObject(dvo));
                        if (tr != TransactionResult.CREATED)
                        {
                            return tr;
                        }
                    }
                }
            }

            return bitacora_repository.update(BitacoraDesarrolloAdapter.voToObject(bitacora_vo));
        }

        public TransactionResult autorizarEdicion(BitacoraDesarrolloVo bitacora_vo)
        {
            return bitacora_repository.autorizarEdicion(BitacoraDesarrolloAdapter.voToObject(bitacora_vo));
        }

        public TransactionResult autorizarRango(BitacoraDesarrolloVo bitacora_vo)
        {
            return bitacora_repository.autorizarRango(BitacoraDesarrolloAdapter.voToObject(bitacora_vo));
        }
    }
}