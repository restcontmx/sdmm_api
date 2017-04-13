using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warrior.EncryptionSystem
{
    /// <summary>
    /// Encryption Service implementation
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        /// <summary>
        /// Decrypt a list of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public IList<T> decryptData<T>(string data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// decrypt a list of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public IList<T> decryptListData<T>(IList<string> data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Encrypt just one object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public IList<string> encryptData<T>(T data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Encrypt bunch of data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public IList<string> encryptListData<T>(IList<T> data)
        {
            throw new NotImplementedException();
        }

    }// End of Encryption Service implementation
}