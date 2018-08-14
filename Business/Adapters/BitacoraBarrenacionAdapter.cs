using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    public class BitacoraBarrenacionAdapter
    {
        public static BitacoraBarrenacionVo objectToVo(Linea obj)
        {
            return new BitacoraBarrenacionVo
            {
            };
        }

        public static BitacoraBarrenacion voToObject(BitacoraBarrenacionVo vo)
        {

            return new BitacoraBarrenacion
            {
                id = vo.id,
                maquinaria = new Maquinaria { id = vo.maquinaria_id },
                operador = new Operador { id = vo.operador_id },
                ayudante = new Operador { id = vo.ayudante_id },
                turno = vo.turno,
                fecha_bitacora = Convert.ToDateTime(vo.fecha_bitacora),
                mesa = vo.mesa,
                beta = vo.beta,
                vale_acero = vo.vale_acero,
                comentarios = vo.comentarios,
                metros_finales = vo.metros_finales,
                hora_primer_barreno = Convert.ToDateTime(vo.hora_primer_barreno),
                hora_ultimo_barreno = Convert.ToDateTime(vo.hora_ultimo_barreno),
                status_edicion = vo.status_edicion,
                dias_apertura_calendario = vo.dias_apertura_calendario,
                user = new Models.Auth.User { id = vo.user_id }
            };

        }
    }
}