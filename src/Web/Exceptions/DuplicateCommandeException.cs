using System;

namespace Involys.Poc.Api.Services
{

    [Serializable]
    public class DuplicateCommandeException : Exception
    {
        public DuplicateCommandeException() : base(Constants.ValueMustBeUnique) { }
        public DuplicateCommandeException(string message) : base(message) { }
        public DuplicateCommandeException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateCommandeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
