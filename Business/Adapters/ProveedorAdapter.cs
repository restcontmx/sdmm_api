using Models.Catalogs;
using Models.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.Adapters
{
    public static class ProveedorAdapter
    {
        public static ProveedorVo objectToVo(Proveedor obj)
        {
            return new ProveedorVo
            {
                id = obj.id,
                nombre_comercial = obj.nombre_comercial,
                razon_social = obj.razon_social,
                rfc = obj.rfc,
                codigo_proveedor = obj.codigo_proveedor,
                permiso_sedena = obj.permiso_sedena,
                calle = obj.calle,
                no_ext = obj.no_ext,
                no_int = obj.no_int,
                colonia = obj.colonia,
                cp = obj.cp,
                localidad = obj.localidad,
                ciudad = obj.ciudad,
                estado = obj.estado,
                user_id = obj.user.id,
                folio = obj.folio,
                timestamp = obj.timestamp.ToString(),
                updated = obj.updated.ToString()
            };
        }

        public static Proveedor voToObject(ProveedorVo vo)
        {
            return new Proveedor
            {
                id = vo.id,
                nombre_comercial = vo.nombre_comercial,
                razon_social = vo.razon_social,
                rfc = vo.rfc,
                codigo_proveedor = vo.codigo_proveedor,
                permiso_sedena = vo.permiso_sedena,
                calle = vo.calle,
                no_ext = vo.no_ext,
                no_int = vo.no_int,
                colonia = vo.colonia,
                cp = vo.cp,
                localidad = vo.localidad,
                ciudad = vo.ciudad,
                estado = vo.estado,
                folio = vo.folio,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}