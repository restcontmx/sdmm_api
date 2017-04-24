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
    public class TipoEmpleadoService : ITipoEmpleadoService
    {
        public ITipoEmpleadoRepository tipoempleado_repository;

        public TipoEmpleadoService(ITipoEmpleadoRepository tipoempleado_repository) {
            this.tipoempleado_repository = tipoempleado_repository;
        }

        public TransactionResult create(TipoEmpleadoVo tipoempleado_vo, User user_log)
        {
            TipoEmpleado tipoempleado = TipoEmpleadoAdapter.voToObject(tipoempleado_vo);
            return tipoempleado_repository.create(tipoempleado);
        }

        public TransactionResult delete(int id)
        {
            return tipoempleado_repository.delete(id);
        }

        public TipoEmpleado detail(int id)
        {
            return tipoempleado_repository.detail(id);
        }

        public IList<TipoEmpleado> getAll()
        {
            return tipoempleado_repository.getAll();
        }

        public TransactionResult update(TipoEmpleadoVo tipoempleado_vo)
        {
            return tipoempleado_repository.update(TipoEmpleadoAdapter.voToObject(tipoempleado_vo));
        }
    }
}