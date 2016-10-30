using System;
using System.Collections.Generic;
using MessageTypes = MvcFlash.AspNetCore.FlashExtensions;

namespace MvcFlash.AspNetCore.Frameworks
{
    public class Bootstrap : IFlashFramework
    {
        private readonly IDictionary<string, string> types =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {nameof(MessageTypes.Error), "danger"},
                {nameof(MessageTypes.Information), "info"},
                {nameof(MessageTypes.Warning), "warning"},
                {nameof(MessageTypes.Success), "success"},
            };

        public string Convert(string type)
        {
            return types[type];
        }
    }
}
