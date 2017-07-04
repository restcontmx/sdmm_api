﻿using Models.Catalogs;
using System.Collections.Generic;
using Warrior.Handlers.Enums;

namespace Data.Interface
{
    public interface ISubNivelRepository
    {
        IList<SubNivel> getAll();
        SubNivel detail(int id);
        TransactionResult create(SubNivel subnivel);
        TransactionResult update(SubNivel subnivel);
        TransactionResult delete(int id);
        IList<string> getNombresLugares();
    }
}
