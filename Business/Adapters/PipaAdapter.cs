using Models.Catalogs;
using Models.VOs;
namespace Business.Adapters
{
    public static class PipaAdapter
    {
        public static PipaVo objectToVo(Pipa obj)
        {
            return new PipaVo
            {
            };
        }

        public static Pipa voToObject(PipaVo vo)
        {
            return new Pipa
            {
                id = vo.id,
                nombre = vo.nombre,
                no_economico = vo.no_economico,
                placas = vo.placas
            };
        }
    }
}