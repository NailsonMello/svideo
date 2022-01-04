using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace SVideo.Application.Exceptions
{
    [Serializable]
    public class PersistenceException : Exception
    {
        protected PersistenceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public PersistenceException(Exception exception) : base("Ocorreu um ou mais erros ao persistir os dados.", exception) { }
        public PersistenceException(string message, Exception exception) : base(message, exception) { }

        public static Exception CheckPersistenceException(DbUpdateException ex)
        {
            if (ex.InnerException?.GetType() == (typeof(SqlException)))
            {
                string key = ((SqlException)ex.InnerException).Message.Split('_').Last();

                var numberError = ((SqlException)ex.InnerException).Number;

                throw numberError switch
                {
                    2627 => new UniqueKeyException(key, ex),
                    8152 => new TruncatedDataException(key, ex),
                    _ => new PersistenceException(key, ex),
                };
            }

            throw new PersistenceException(ex);
        }
    }
}
