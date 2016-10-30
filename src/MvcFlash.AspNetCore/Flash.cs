using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MvcFlash.AspNetCore
{
    public class Flash : IFlash
    {
        private readonly ISession session;
        public const string SessionKeySuffix = "F!";
        public readonly Encoding Encoding = Encoding.UTF8;

        public Flash(ISession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            this.session = session;
        }

        public virtual void Push<TMessage>(TMessage message)
            where TMessage : Message
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var order = session.AllFlashKeys().Count();
            var type = message.GetType().AssemblyQualifiedName;
            var sessionKey = $"{SessionKeySuffix}:{order}:{type}:{message.Key}";
            var json = JsonConvert.SerializeObject(message, Formatting.None);
            var bytes = Encoding.GetBytes(json);

            if (!string.IsNullOrWhiteSpace(message.Key))
            {
                var keys = session.AllFlashKeys().Where(x => x.Contains(message.Key)).ToList();
                keys.ForEach(session.Remove);
            }

            session.Set(sessionKey, bytes);
        }

        public virtual Message Pop()
        {
            var entry = session
                .AllMessageMetadatas()
                .OrderByDescending(x => x.Order)
                .Select(Get)
                .FirstOrDefault();
            
            if (entry == null)
                return null;

            session.Remove(entry.Metadata.SessionKey);
            return entry.Result;
        }

        public virtual int Count()
        {
            return session
                .AllFlashKeys()
                .Count();
        }

        public void Clear()
        {
            session.AllFlashKeys()
                .ToList()
                .ForEach(session.Remove);
        }

        private MessageResult Get(MessageMetadata metadata)
        {
            byte[] bytes;
            if (!session.TryGetValue(metadata.SessionKey, out bytes)) return null;

            var json = Encoding.GetString(bytes);

            return new MessageResult
            {
                Metadata = metadata,
                Result = (Message)JsonConvert.DeserializeObject(json, metadata.MessageType)
            };
        }
    }
}
