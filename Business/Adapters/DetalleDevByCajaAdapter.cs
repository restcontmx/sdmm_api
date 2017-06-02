using Models.Auth;
using Models.Catalogs;
using Models.VOs;


namespace Business.Adapters
{
    public static class DetalleDevByCajaAdapter
    {
        public static DetalleDevByCaja voToObject(DetalleDevByCajaVo vo)
        {
            return new DetalleDevByCaja
            {
                nombreP = vo.nombreP,
                empresa = vo.empresa,
                vale = new Vale { id = int.Parse(vo.vale_id) }

            };
        }

        public static DetalleDevByCajaVo objectToVo(DetalleDevByCaja vo)
        {
            return new DetalleDevByCajaVo
            {
                nombreP = vo.nombreP,
                empresa = vo.empresa,
                vale_id = vo.vale.id.ToString()

            };
        }
    }

}