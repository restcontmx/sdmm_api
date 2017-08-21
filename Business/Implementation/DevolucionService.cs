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

        public TransactionResult createP(DevolucionVo devolucion_vo, User user_log)
        {
            
            Devolucion devolucion = DevolucionAdapter.voToObject(devolucion_vo);
            devolucion.user = user_log;


            int id =  devolucion_repository.createP(devolucion);
            if (id > 0)
            {
                var tr = TransactionResult.CREATED;
                foreach (RegistroDetalleDevVo rvo in devolucion_vo.registros)
                {
                    rvo.devolucion_id = id;
                    rvo.user_id = devolucion.user.id;
                    tr = devolucion_repository.createRegistroDetalleDev(RegistroDetalleDevAdapter.voToObject(rvo));
                    if (tr != TransactionResult.CREATED)
                    {
                        return tr;
                    }
                }
                return tr;
            }
            return TransactionResult.ERROR;
        }

        public IList<Devolucion> getAll()
        {
            return devolucion_repository.getAll();
        }

        public IList<RegistroDetalleDev> detail(int id)
        {
            return devolucion_repository.detail(id);
        }

        public DetalleDevByCajaVo getDetalleByCaja(string folio)
        {
            return DetalleDevByCajaAdapter.objectToVo(devolucion_repository.getDetalleByCaja(folio));
        }

    }
}