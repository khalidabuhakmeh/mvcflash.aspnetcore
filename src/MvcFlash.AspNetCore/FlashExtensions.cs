using System;

namespace MvcFlash.AspNetCore
{
    public static class FlashExtensions
    {
        public static IFlash Error(
            this IFlash flash,
            string content,
            string title = null,
            string key = null)
        {
            return AddMessage(flash, nameof(Error), content, title, key);
        }

        public static IFlash Information(
            this IFlash flash,
            string content,
            string title = null,
            string key = null)
        {
            return AddMessage(flash, nameof(Information), content, title, key);
        }

        public static IFlash Success(
            this IFlash flash,
            string content,
            string title = null,
            string key = null)
        {
            return AddMessage(flash, nameof(Success), content, title, key);
        }

        public static IFlash Warning(
            this IFlash flash,
            string content,
            string title = null,
            string key = null)
        {
            return AddMessage(flash, nameof(Warning), content, title, key);
        }

        private static IFlash AddMessage(
            this IFlash flash,
            string type,
            string content,
            string title = null,
            string key = null)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));

            flash.Push(new Message { Type = type, Content = content, Key = key, Title = title });
            return flash;
        }
    }
}
