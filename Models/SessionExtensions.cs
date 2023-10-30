using Microsoft.AspNetCore.Http;
using System.Text.Json;

public static class SessionExtensions
{
    public static T GetObject<T>(this ISession session, string key) where T : class
    {
        var data = session.GetString(key);
        if (data == null)
        {
            return null;
        }
        return JsonSerializer.Deserialize<T>(data);
    }


    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }
}
