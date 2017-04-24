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
    public class TipoProductoService : ITipoProductoService
    {
        private ITipoProductoRepository tipoproducto_repository;

        public TipoProductoService(ITipoProductoRepository tipoproducto_repository) {
            this.tipoproducto_repository = tipoproducto_repository;
        }

        public TransactionResult create(TipoProductoVo tipoproducto_vo, User user_log)
        {
            TipoProducto tipoproducto = TipoProductoAdapter.voToObject(tipoproducto_vo);
            return tipoproducto_repository.create(tipoproducto);
        }

        public TransactionResult delete(int id)
        {
            return tipoproducto_repository.delete(id);
        }

        public TipoProducto detail(int id)
        {
            return tipoproducto_repository.detail(id);
        }

        public IList<TipoProducto> getAll()
        {
            return tipoproducto_repository.getAll();
        }

        public TransactionResult update(TipoProductoVo tipoproducto_vo)
        {
            return tipoproducto_repository.update(TipoProductoAdapter.voToObject(tipoproducto_vo));
        }
    }
}