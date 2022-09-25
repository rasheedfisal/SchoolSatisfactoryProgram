using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL.Extensions
{
    public static class SessionExtension
    {
        public static void SetObjectAsJson(this ISession session, string key, object obj)
        {
            session.SetString(key, JsonSerializer.Serialize(obj));
        }
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
