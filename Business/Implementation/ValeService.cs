using Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using Warrior.Handlers.Enums;
using Data.Interface;
using Business.Adapters;

namespace Business.Implementation
{
    /// <summary>
    /// Vale service implementation
    /// </summary>
    public class ValeService : IValeService
    {
        private IValeRepository vale_repository;

        /// <summary>
        /// Vale service constructor
        /// </summary>
        /// <param name="vale_repository"></param>
        public ValeService(IValeRepository vale_repository) {
            this.vale_repository = vale_repository;
        }

        /// <summary>
        /// Create Vale function
        /// </summary>
        /// <param name="vale_vo"></param>
        /// <param name="user_log"></param>
        /// <returns></returns>
        public TransactionResult create(ValeVo vale_vo, User user_log)
        {
            Vale vale = ValeAdapter.voToObject(vale_vo);
            vale.user = user_log;
            int id = vale_repository.create(vale);
            if (id > 0) {
                foreach (DetalleValeVo dvo in vale_vo.detalles)
                {
                    dvo.vale_id = id;
                    int tr = vale_repository.createDetalle(DetalleValeAdapter.voToObject(dvo));
                    if (0 >= tr)
                    {
                        return TransactionResult.ERROR;
                    }
                    else
                    {
                        if (dvo.registros != null)
                        {
                            bool insert = true;
                            foreach(RegistroDetalleVo r in dvo.registros)
                            {
                                if (r.folio == null || r.producto_id == 0)
                                {
                                    insert = false;
                                    break;
                                }
                            }
                            if (insert)
                            {
                                var tr2 = TransactionResult.CREATED;
                                foreach (RegistroDetalleVo rvo in dvo.registros)
                                {
                                    dvo.vale_id = id;
                                    rvo.user_id = vale_vo.user_id;
                                    rvo.vale_id = id;
                                    rvo.detallevale_id = tr;

                                    tr2 = vale_repository.createRegistroDetalle(RegistroDetalleAdapter.voToObject(rvo));
                                    if (tr2 != TransactionResult.CREATED)
                                    {
                                        return tr2;
                                    }
                                }
                            }
                            
                        }
                    }
                }
                return TransactionResult.CREATED;
            }
            return TransactionResult.ERROR;
        }

        /// <summary>
        /// Create registro detalle 
        /// </summary>
        /// <param name="registro_vo"></param>
        /// <param name="user_log"></param>
        /// <returns></returns>
        public TransactionResult createRegistroDetalle(RegistroDetalleVo registro_vo, User user_log)
        {
            RegistroDetalle registro = RegistroDetalleAdapter.voToObject(registro_vo);
            registro.user = user_log;
            return vale_repository.createRegistroDetalle(registro);
        }

        /// <summary>
        /// Create registro detalle 
        /// </summary>
        /// <param name="registro_vo"></param>
        /// <param name="user_log"></param>
        /// <returns></returns>
        public TransactionResult createRegistroDetalleOver(RegistroDetalleVo registro_vo, User user_log)
        {
            RegistroDetalle registro = RegistroDetalleAdapter.voToObject(registro_vo);
            registro.user = user_log;
            return vale_repository.createRegistroDetalleOver(registro);
        }

        public TransactionResult createRegistroDetalleByList(IList<RegistroDetalleVo> registrodetalles_vo, User user)
        {
            foreach (RegistroDetalleVo registro in registrodetalles_vo) {
                if (createRegistroDetalle(registro, user)!= TransactionResult.CREATED) {
                    return TransactionResult.ERROR;
                }
            }return TransactionResult.CREATED;
        }

        /// Delete vale
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionResult delete(int id)
        {
            IList<DetalleVale> detallesAux = vale_repository.getAllDetalles(id);
            foreach (DetalleVale d in detallesAux)
            {
                //vale_repository.updateDetalleVale(DetalleValeAdapter.voToObject(d));
                vale_repository.deleteRegistroDetalle(d.id);
            }

            return vale_repository.delete(id);
        }

        /// <summary>
        /// Retrieve vale detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vale detail(int id)
        {
            return vale_repository.detail(id);
        }

        /// <summary>
        /// Get all vales
        /// </summary>
        /// <returns></returns>
        public IList<Vale> getAll()
        {
            return vale_repository.getAll();
        }

        /// <summary>
        /// get all registers by detalle id
        /// </summary>
        /// <param name="detalle_id"></param>
        /// <returns></returns>
        public IList<RegistroDetalle> getAllRegistersByDetalle(int detalle_id)
        {
            return vale_repository.getAllRegistersByDetalle(detalle_id);
        }

        /// <summary>
        /// get all registers over by detalle id
        /// </summary>
        /// <param name="detalle_id"></param>
        /// <returns></returns>
        public IList<RegistroDetalle> getAllRegistersOverByDetalle(int detalle_id)
        {
            return vale_repository.getAllRegistersOverByDetalle(detalle_id);
        }


        /// <summary>
        /// Get Details by vale id
        /// </summary>
        /// <param name="vale_id"></param>
        /// <returns></returns>
        public IList<DetalleVale> getDetailsByValeId(int vale_id)
        {
            return vale_repository.getAllDetalles(vale_id);
        }

        public IList<RegistroDetalle> getAllRegistersByFolioCaja(string folioCaja)
        {
            return vale_repository.getAllRegistersByFolioCaja(folioCaja);
        }

        public IList<RegistroDetalle> getAllRegistersSacos()
        {
            return vale_repository.getAllRegistersSacos();
        }

        public IList<RegistroDetalle> getAllRegistersHistorico()
        {
            return vale_repository.getAllRegistersHistorico();
        }

        public IList<RegistroDetalle> getAllRegistersHistoricoOver()
        {
            return vale_repository.getAllRegistersHistoricoOver();
        }


        /// <summary>
        /// Update vale
        /// </summary>
        /// <param name="vale_vo"></param>
        /// <returns></returns>
        public TransactionResult update(ValeVo vale_vo)
        {

            vale_repository.deleteDetalleVale(vale_vo.id);

            foreach (DetalleValeVo dvo in vale_vo.detalles)
            {
                dvo.vale_id = vale_vo.id;
                int tr = vale_repository.createDetalle(DetalleValeAdapter.voToObject(dvo));
                if (0 >= tr)
                {
                    return TransactionResult.ERROR;
                }
                else
                {
                    if (dvo.registros != null)
                    {
                        bool insert = true;
                        foreach (RegistroDetalleVo r in dvo.registros)
                        {
                            if (r.folio == null || r.producto_id == 0)
                            {
                                insert = false;
                                break;
                            }
                        }
                        if (insert)
                        {
                            var tr2 = TransactionResult.CREATED;
                            foreach (RegistroDetalleVo rvo in dvo.registros)
                            {
                                dvo.vale_id = vale_vo.id;
                                rvo.user_id = vale_vo.user_id;
                                rvo.vale_id = vale_vo.id;
                                rvo.detallevale_id = tr;
                                tr2 = vale_repository.createRegistroDetalle(RegistroDetalleAdapter.voToObject(rvo));
                                if (tr2 != TransactionResult.CREATED)
                                {
                                    return tr2;
                                }
                            }
                        }
                    }
                }
            }

            return vale_repository.update(ValeAdapter.voToObject(vale_vo));
        }

        public TransactionResult updateStatus(ValeVo vale_vo)
        {
            return vale_repository.updateStatus(ValeAdapter.voToObject(vale_vo));
        }

        public TransactionResult updateAutorizacion(ValeVo vale_vo, User user_log)
        {
            Vale vale = ValeAdapter.voToObject(vale_vo);
            vale.userAutorizo = user_log;

            return vale_repository.updateAutorizacion(vale);
        }

        public User validarLoginTablet(UserVo user)
        {
            return vale_repository.validarLoginTablet(UserAdapter.voToObject(user));
        }
    }
}