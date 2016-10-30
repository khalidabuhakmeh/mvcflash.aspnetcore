using System.Collections.Generic;

namespace MvcFlash.AspNetCore.Sample.Models
{
    public class MessageWithData : Message
    {
        public Dictionary<string,string> Data { get; set; } = new Dictionary<string, string>();
    }
}