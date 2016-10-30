using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MvcFlash.AspNetCore
{
    internal static class SessionExtensions
    {
        public static IEnumerable<string> AllFlashKeys(this ISession session)
        {
            return session.Keys.Where(x => x.Contains(Flash.SessionKeySuffix));
        }

        internal static IEnumerable<MessageMetadata> AllMessageMetadatas(this ISession session)
        {
            return AllFlashKeys(session).Select(x => new MessageMetadata(x));
        }
    }
}