using Models.Catalogs;
using Models.VOs;

namespace Business.Adapters
{
    public class CombustibleAdapter
    {
        public static CombustibleVo objectToVo(Combustible obj)
        {
            return new CombustibleVo
            {
            };
        }

        public static Combustible voToObject(CombustibleVo vo)
        {
            return new Combustible
            {
                id = vo.id,
                nombre = vo.nombre,
                unidad = vo.unidad,
                codigo = vo.codigo
            };
        }
    }
}