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
    public class InventarioService : IInventarioService
    {
        public IInventarioRepository inventario_repository;
        public InventarioService(IInventarioRepository inventario_repository)
        {
            this.inventario_repository = inventario_repository;
        }

        public TransactionResult create(InventarioVo inventario_vo)
        {
            Inventario obj = InventarioAdapter.voToObject(inventario_vo);
            return inventario_repository.create(obj);
        }

        public TransactionResult createIventarioDiario()
        {
            return inventario_repository.createIventarioDiario();
        }

        public TransactionResult delete(int id)
        {
            return inventario_repository.delete(id);
        }

        public Inventario detail(int id)
        {
            return inventario_repository.detail(id);
        }

        public IList<InfoInventario> getAll(string checkDate)
        {
            return inventario_repository.getAll(checkDate);
        }
    }
}