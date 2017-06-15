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
                var tr = TransactionResult.CREATED;
                foreach (DetalleValeVo dvo in vale_vo.detalles)
                {
                    dvo.vale_id = id;
                    tr = vale_repository.createDetalle( DetalleValeAdapter.voToObject(dvo) );
                    if (tr != TransactionResult.CREATED) {
                        return tr;
                    }
                }return tr;
            }return TransactionResult.ERROR;
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

        public TransactionResult createRegistroDetalleByList(IList<RegistroDetalleVo> registrodetalles_vo, User user)
        {
            foreach (RegistroDetalleVo registro in registrodetalles_vo) {
                if (createRegistroDetalle(registro, user)!= TransactionResult.CREATED) {
                    return TransactionResult.ERROR;
                }
            }return TransactionResult.CREATED;
        }

        /// <summary>
        /// Delete vale
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionResult delete(int id)
        {
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


        /// <summary>
        /// Update vale
        /// </summary>
        /// <param name="vale_vo"></param>
        /// <returns></returns>
        public TransactionResult update(ValeVo vale_vo)
        {
            return vale_repository.update(ValeAdapter.voToObject(vale_vo));
        }

        public TransactionResult updateStatus(ValeVo vale_vo)
        {
            return vale_repository.updateStatus(ValeAdapter.voToObject(vale_vo));
        }
    }
}