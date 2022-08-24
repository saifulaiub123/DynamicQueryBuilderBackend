using System;

namespace Involys.Poc.Api.Services
{

    [Serializable]
    public class CommandeNotFoundException : Exception
    {
        public CommandeNotFoundException() : base(Constants.CommandeNotFound) { }
        public CommandeNotFoundException(string message) : base(message) { }
        public CommandeNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected CommandeNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
