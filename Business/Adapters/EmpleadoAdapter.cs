using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    /// <summary>
    /// Empleado adapter 
    /// </summary>
    public static class EmpleadoAdapter
    {
        public static EmpleadoVo objectToVo(Empleado obj)
        {
            return new EmpleadoVo
            {
            };
        }

        /// <summary>
        /// Void object to object
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        public static Empleado voToObject(EmpleadoVo vo)
        {
            return new Empleado
            {
                id = vo.id,
                tipo_empleado = new TipoEmpleado { id = vo.tipoempleado_id },
                nombre = vo.nombre,
                ap_paterno = vo.ap_paterno,
                ap_materno = vo.ap_materno,
                compania = new Compania { id = vo.compania_id },
                status = vo.status == 0 ? false : true,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}