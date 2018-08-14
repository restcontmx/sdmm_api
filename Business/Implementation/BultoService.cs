using Business.Interface;
using Data.Interface;
using System.Collections.Generic;
using Models.Auth;
using Models.Catalogs;
using Models.VOs;
using Warrior.Handlers.Enums;
using Business.Adapters;

namespace Business.Implementation
{
    public class BultoService: IBultoService
    {
        public IBultoRepository bulto_repository;

        public BultoService(IBultoRepository bulto_repository)
        {
            this.bulto_repository = bulto_repository;
        }

        /// <summary>
        /// Create object on the repository
        /// </summary>
        /// <param name="caja_vo"></param>
        /// <param name="user_log"></param>
        /// <returns></returns>
        public TransactionResult create(BultoVo bulto_vo, User user_log)
        {
            Bulto bulto = BultoAdapter.voToObject(bulto_vo);
            bulto.user = user_log;
            return bulto_repository.create(bulto);
        }

        /// <summary>
        /// Delete object on the repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TransactionResult delete(int id)
        {
            return bulto_repository.delete(id);
        }

        /// <summary>
        /// Retrieve object 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Bulto detail(int id)
        {
            return bulto_repository.detail(id);
        }

        /// <summary>
        /// Get all objects from the repository
        /// </summary>
        /// <returns></returns>
        public IList<Bulto> getAll()
        {
            return bulto_repository.getAll();
        }


        public TransactionResult createBultosByList(IList<BultoVo> bultos_vo, User user)
        {
            //bool insertInv = true;
            int auxCount = 0;
            int producto_id = 0;
            int turno = 1;

            foreach (BultoVo registro in bultos_vo)
            {
                TransactionResult tr = create(registro, user);
                producto_id = registro.producto_id;

                if (tr != TransactionResult.CREATED)
                {
                    if (tr != TransactionResult.EXISTS)
                    {
                        return TransactionResult.ERROR;
                    }
                }
                else
                {
                    auxCount = auxCount + 1;
                }

                /*
                //Inserta la cantidad de bultos con los datos del primer bulto
                if (insertInv)
                {
                    Inventario inv = new Inventario
                    {
                        cantidad = bultos_vo.Count,
                        producto = new Producto { id = registro.producto_id },
                        turno = 1
                    };

                    bulto_repository.createInventario(inv);

                    insertInv = false;
                }    
                */

            }

            if (auxCount > 0)
            {
                Inventario inv = new Inventario
                {
                    cantidad = auxCount,
                    producto = new Producto { id = producto_id },
                    turno = turno
                };

                bulto_repository.createInventario(inv);
            }
            return TransactionResult.CREATED;
        }
    }
}