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
    public class EmpleadoService : IEmpleadoService
    {
        private IEmpleadoRepository empleado_repository;

        public EmpleadoService(IEmpleadoRepository empleado_repository) {
            this.empleado_repository = empleado_repository;
        }

        public TransactionResult create(EmpleadoVo empleado_vo, User user_log)
        {
            Empleado empleado = EmpleadoAdapter.voToObject(empleado_vo);
            empleado.user = user_log;
            return empleado_repository.create(empleado);
        }

        public TransactionResult delete(int id)
        {
            return empleado_repository.delete(id);
        }

        public Empleado detail(int id)
        {
            return empleado_repository.detail(id);
        }

        public IList<Empleado> getAll()
        {
            return empleado_repository.getAll();
        }

        public TransactionResult update(EmpleadoVo empleado_vo)
        {
            return empleado_repository.update(EmpleadoAdapter.voToObject(empleado_vo));
        }
    }
}