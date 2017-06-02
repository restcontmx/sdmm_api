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
    public class DevolucionServide : IDevolucionService
    {
        private IDevolucionRepository devolucion_repository;

        public DevolucionServide(IDevolucionRepository devolucion_repository)
        {
            this.devolucion_repository = devolucion_repository;
        }

        public TransactionResult create(DevolucionVo devolucion_vo, User user_log)
        {
            Devolucion devolucion = DevolucionAdapter.voToObject(devolucion_vo);
            devolucion.user = user_log;
            return devolucion_repository.create(devolucion);
        }

        public DetalleDevByCajaVo getDetalleByCaja(string folio)
        {
            return DetalleDevByCajaAdapter.objectToVo(devolucion_repository.getDetalleByCaja(folio));
        }

    }
}