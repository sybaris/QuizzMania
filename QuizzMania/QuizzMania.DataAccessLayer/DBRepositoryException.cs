using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QuizzMania.DataAccessLayer
{
    [Serializable]
    public class DBRepositoryException : Exception
    {
        public DBRepositoryException()
        {
        }

        public DBRepositoryException(string? message) : base(message)
        {
        }

        public DBRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DBRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
