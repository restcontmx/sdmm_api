using Models.Catalogs;
using Models.VOs;


namespace Business.Adapters
{
    public static class CompaniaAdapter
    {
        public static CompaniaVo objectToVo(Compania obj)
        {
            return new CompaniaVo
            {
            };
        }

        public static Compania voToObject(CompaniaVo vo)
        {
            return new Compania
            {
                id = vo.id,
                razon_social = vo.razon_social,
                nombre_sistema = vo.nombre_sistema,
                categoria = new Categoria { id = int.Parse(vo.categoria_id)},
                cuenta = new Cuenta { id = int.Parse(vo.cuenta_id)},
                user = new Models.Auth.User { id = vo.user_id }
                
            };
        }
    }
}