using Models.Catalogs;
using Models.VOs;
using System;

namespace Business.Adapters
{
    public class BitacoraDesarrolloAdapter
    {
        public static BitacoraDesarrolloVo objectToVo(Categoria obj)
        {
            return new BitacoraDesarrolloVo
            {
            };
        }

        public static BitacoraDesarrollo voToObject(BitacoraDesarrolloVo vo)
        {

            return new BitacoraDesarrollo
            {
                id = vo.id,
                maquinaria = new Maquinaria { id = vo.maquinaria_id },
                fecha_bitacora = Convert.ToDateTime(vo.fecha_bitacora),
                grupo = vo.grupo,
                turno = vo.turno,
                compania =  new Compania { id = vo.compania_id },
                vale_acero =  vo.vale_acero,
                vale_explosivos = vo.vale_explosivos,
                subnivel = new SubNivel { id = vo.subnivel_id },
                zona = vo.zona,
                tipo_desarrollo = new TipoDesarrollo { id = vo.tipo_desarrollo_id },
                hora_primer_barreno = Convert.ToDateTime(vo.hora_primer_barreno),
                hora_ultimo_barreno = Convert.ToDateTime(vo.hora_ultimo_barreno),
                numero_barrenos = vo.numero_barrenos,
                anclas =  vo.anclas,
                mallas = vo.mallas,
                operador = new Operador { id = vo.operador_id },
                ayudante =  new Operador { id = vo.ayudante_id },
                comentarios = vo.comentarios,
                status_edicion = vo.status_edicion,
                dias_apertura_calendario = vo.dias_apertura_calendario,
                user = new Models.Auth.User { id = vo.user_id }
            };

    }
    }
}