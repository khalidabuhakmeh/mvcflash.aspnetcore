using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MvcFlash.AspNetCore
{
    internal class MessageMetadata
    {
        public MessageMetadata(string sessionKey)
        {
            if (sessionKey == null)
                throw new ArgumentNullException(nameof(sessionKey));

            var parts = sessionKey.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);

            SessionKey = sessionKey;
            Order = parts.Skip(1).Take(1).Select(i => Convert.ToInt32(i)).FirstOrDefault();
            MessageType = parts.Skip(2).Take(1).Select(Type.GetType).FirstOrDefault() ?? typeof(Message);
        }

        public Type MessageType { get; set; }
        public int Order { get; protected set; }
        public string SessionKey { get;  protected set; }
    }

    internal class MessageResult
    {
        public MessageMetadata Metadata { get; set; }
        public Message Result { get; set; }
    }
}