using Business.Interface;
using Data.Interface;
using System.Collections.Generic;
using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using Warrior.Handlers.Enums;
using Business.Adapters;

namespace Business.Implementation
{
    public class CajaService : ICajaService
    {
        public ICajaRepository caja_repository;

        public CajaService(ICajaRepository caja_repository) {
            this.caja_repository = caja_repository;
        }

        /// <summary>
        /// Create object on the repository
        /// </summary>
        /// <param name="caja_vo"></param>
        /// <param name="user_log"></param>
        /// <returns></returns>
        public TransactionResult create(CajaVo caja_vo, User user_log)
        {
            Caja caja = CajaAdapter.voToObject(caja_vo);
            caja.user = user_log;
            return caja_repository.create(caja);
        }

        /// <summary>
        /// Delete object on the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionResult delete(int id)
        {
            return caja_repository.delete(id);
        }

        /// <summary>
        /// Retrieve object 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Caja detail(int id)
        {
            return caja_repository.detail(id);
        }

        /// <summary>
        /// Get all objects from the repository
        /// </summary>
        /// <returns></returns>
        public IList<Caja> getAll()
        {
            return caja_repository.getAll();
        }

        /// <summary>
        /// Update object on the repository
        /// </summary>
        /// <param name="caja_vo"></param>
        /// <returns></returns>
        public TransactionResult update(CajaVo caja_vo)
        {
            return caja_repository.update(CajaAdapter.voToObject(caja_vo));
        }

        /// <summary>
        /// Create object on the repository
        /// </summary>
        /// <param name="obs_vo"></param>
        /// <param name="user_log"></param>
        /// <returns></returns>
        public TransactionResult createObservacion(ObservacionVo obs_vo, User user_log)
        {
            Observacion obs = ObservacionAdapter.voToObject(obs_vo);
            obs.user = user_log;
            return caja_repository.createObservacion(obs);
        }
    }
}