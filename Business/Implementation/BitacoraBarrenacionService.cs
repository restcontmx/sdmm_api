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
    public class BitacoraBarrenacionService : IBitacoraBarrenacionService
    {
        private IBitacoraBarrenacionRepository bitacora_repository;

        public BitacoraBarrenacionService(IBitacoraBarrenacionRepository bitacora_repository)
        {
            this.bitacora_repository = bitacora_repository;
        }

        public TransactionResult create(BitacoraBarrenacionVo bitacora_vo, User user_log)
        {
            if(bitacora_vo.comentarios == null)
            {
                bitacora_vo.comentarios = "";
            }
            if (bitacora_vo.mesa == null)
            {
                bitacora_vo.mesa = "";
            }
            if (bitacora_vo.beta == null)
            {
                bitacora_vo.beta = "";
            }
            if (bitacora_vo.vale_acero == null)
            {
                bitacora_vo.vale_acero = "";
            }

            BitacoraBarrenacion obj = BitacoraBarrenacionAdapter.voToObject(bitacora_vo);
            obj.user = user_log;

            int id = bitacora_repository.create(obj);
            if (id > 0)
            {
                var tr = TransactionResult.CREATED;

                if (bitacora_vo.demoras != null)
                {
                    //Agregar demoras
                    foreach (DetalleDemoraBitacoraVo dvo in bitacora_vo.demoras)
                    {
                        dvo.bitacora_barrenacion_id = id;
                        dvo.tipo_bitacora = 2;
                        tr = bitacora_repository.createDetalleDemoraBitacora(DetalleDemoraBitacoraAdapter.voToObject(dvo));
                        if (tr != TransactionResult.CREATED)
                        {
                            return tr;
                        }
                    }
                }

                if (bitacora_vo.lineas != null)
                {
                    //Agregar lineas
                    foreach (LineaVo lin in bitacora_vo.lineas)
                    {
                        lin.bitacora_id = id;
                        int idL = bitacora_repository.createLineaBitacora(LineaAdapter.voToObject(lin));
                        if (idL > 0)
                        {
                            if (lin.barrenos != null)
                            {
                                foreach (BarrenoVo bvo in lin.barrenos)
                                {
                                    bvo.bitacora_id = bitacora_vo.id;
                                    bvo.linea_id = idL;
                                    tr = bitacora_repository.createBarrenoLineaBitacora(BarrenoAdapter.voToObject(bvo));
                                    if (tr != TransactionResult.CREATED)
                                    {
                                        return tr;
                                    }
                                }
                            }
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

        public BitacoraBarrenacion detail(int id)
        {
            BitacoraBarrenacion bit = bitacora_repository.detail(id);
            bit.demoras = bitacora_repository.getDemorasByIdBitacora(bit.id);
            bit.lineas = bitacora_repository.getLineasByIdBitacora(bit.id);

            foreach(Linea lin in bit.lineas)
            {
                lin.barrenos = bitacora_repository.getBarrenosByIdLinea(lin.id);
            }
            return bit;
        }

        public IList<BitacoraBarrenacion> getAll()
        {
            return bitacora_repository.getAll();
        }

        public IList<BitacoraBarrenacion> getAllBitacoraByIdSupervisor(int user_id)
        {
            return bitacora_repository.getAllBitacoraByIdSupervisor(user_id);
        }

        public TransactionResult update(BitacoraBarrenacionVo bitacora_vo, User user_log)
        {
            if (bitacora_vo.comentarios == null)
            {
                bitacora_vo.comentarios = "";
            }
            bitacora_vo.user_id = user_log.id;
            //Eliminamos los detalles existentes
            bitacora_repository.deleteDetalleDemoraBitacora(bitacora_vo.id);
            bitacora_repository.deleteBarrenosByIdBitacora(bitacora_vo.id);
            bitacora_repository.deleteLineaByIdBitacora(bitacora_vo.id);

            //Creamos las demoras y lineas
            if (bitacora_vo.demoras != null || bitacora_vo.lineas != null)
            {
                var tr = TransactionResult.CREATED;

                if (bitacora_vo.demoras != null)
                {
                    //Agregar demoras
                    foreach (DetalleDemoraBitacoraVo dvo in bitacora_vo.demoras)
                    {
                        dvo.bitacora_barrenacion_id = bitacora_vo.id;
                        dvo.tipo_bitacora = 2;
                        tr = bitacora_repository.createDetalleDemoraBitacora(DetalleDemoraBitacoraAdapter.voToObject(dvo));
                        if (tr != TransactionResult.CREATED)
                        {
                            return tr;
                        }
                    }
                }

                if (bitacora_vo.lineas != null)
                {
                    //Agregar lineas
                    foreach (LineaVo lin in bitacora_vo.lineas)
                    {
                        lin.bitacora_id = bitacora_vo.id;
                        int idL = bitacora_repository.createLineaBitacora(LineaAdapter.voToObject(lin));
                        if (idL > 0)
                        {
                            if (lin.barrenos != null)
                            {
                                foreach (BarrenoVo bvo in lin.barrenos)
                                {
                                    bvo.bitacora_id = bitacora_vo.id;
                                    bvo.linea_id = idL;
                                    tr = bitacora_repository.createBarrenoLineaBitacora(BarrenoAdapter.voToObject(bvo));
                                    if (tr != TransactionResult.CREATED)
                                    {
                                        return tr;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bitacora_repository.update(BitacoraBarrenacionAdapter.voToObject(bitacora_vo));
        }

        public TransactionResult autorizarEdicion(BitacoraBarrenacionVo bitacora_vo)
        {
            return bitacora_repository.autorizarEdicion(BitacoraBarrenacionAdapter.voToObject(bitacora_vo));
        }

        public TransactionResult autorizarRango(BitacoraBarrenacionVo bitacora_vo)
        {
            return bitacora_repository.autorizarRango(BitacoraBarrenacionAdapter.voToObject(bitacora_vo));
        }
    }
}