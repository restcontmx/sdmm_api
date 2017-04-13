using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warrior.EncryptionSystem
{
    /// <summary>
    /// Encryption Service definition
    /// </summary>
    public interface IEncryptionService
    {
        IList<String> encryptData<T>( T data );
        IList<String> encryptListData<T>( IList<T> data );
        IList<T> decryptListData<T>( IList<String> data );
        IList<T> decryptData<T>( String data );

    }// End of Encryption Service definition
}
