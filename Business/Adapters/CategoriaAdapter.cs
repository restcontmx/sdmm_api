using Models.Catalogs;
using Models.VOs;

namespace Business.Adapters
{
    public static class CategoriaAdapter
    {
        public static CategoriaVo objectToVo(Categoria obj)
        {
            return new CategoriaVo
            {
            };
        }

        public static Categoria voToObject(CategoriaVo vo)
        {
            return new Categoria
            {
                id = vo.id,
                nivel = new Nivel { id = vo.nivel_id },
                procesominero = new ProcesoMinero { id = vo.procesominero_id },
                nombre = vo.nombre,
                user = new Models.Auth.User { id = vo.user_id }
            };
        }
    }
}