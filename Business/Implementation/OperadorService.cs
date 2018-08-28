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
    public class OperadorService : IOperadorService
    {
        private IOperadorRepository operador_repository;

        public OperadorService(IOperadorRepository operador_repository)
        {
            this.operador_repository = operador_repository;
        }

        public TransactionResult create(OperadorVo operador_vo, int sistema)
        {
            Operador operador = OperadorAdapter.voToObject(operador_vo);
            return operador_repository.create(operador, sistema);
        }

        public TransactionResult delete(int id, int sistema)
        {
            return operador_repository.delete(id, sistema);
        }

        public Operador detail(int id, int sistema)
        {
            return operador_repository.detail(id, sistema);
        }

        public IList<Operador> getAll(int sistema)
        {
            return operador_repository.getAll(sistema);
        }

        public TransactionResult update(OperadorVo operador_vo, int sistema)
        {
            return operador_repository.update(OperadorAdapter.voToObject(operador_vo), sistema);
        }
    }
}