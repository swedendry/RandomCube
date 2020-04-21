using Game.Contents;
using Game.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.Extensions
{
    public static class HubContextExtension
    {
        public static Task Send(this IHubContext<GameHub> context, string method, object arg1, params string[] connectionIds)
        {
            return context.Clients.Clients(connectionIds).SendAsync(method, arg1);
        }

        //public static Task Clients(this IHubContext<GameHub> context, List<string> connectionIds, string method, object arg1)
        //{
        //    return context.Clients.Clients(connectionIds).SendAsync(method, arg1);
        //}

        //public static Task Send<T>(this IHubContext<GameHub> context, List<T> users, string method, object arg1) where T : BaseUser
        //{
        //    return context.Clients.Client(connectionId).SendAsync(method, arg1);
        //}

        //public static Task AllExclude<T>(this IHubContext<GameHub> context, List<T> users, List<string> connectionIds, string method, object arg1) where T : BaseUser
        //{
        //    return context.Clients.Clients(connectionIds).SendAsync(method, arg1);
        //}
    }
}
