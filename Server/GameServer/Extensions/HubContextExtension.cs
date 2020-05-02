using GameServer.Hubs;
using GameServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;

namespace GameServer.Extensions
{
    public static class HubContextExtension
    {
        public static IClientProxy Clients(this IHubContext<GameHub> context, params string[] connectionIds)
        {
            return context.Clients.Clients(connectionIds);
        }

        public static IClientProxy Clients(this IHubContext<GameHub> context, List<GameUser> users)
        {
            var connectionIds = users.Select(x => x.ConnectionId);
            return context.Clients(connectionIds.ToArray());
        }

        public static IClientProxy ClientsExceptByConnectionId(this IHubContext<GameHub> context, List<GameUser> users, params string[] excludedConnectionIds)
        {
            var connectionIds = users.Where(x => !excludedConnectionIds.Contains(x.ConnectionId)).Select(x => x.ConnectionId);
            return context.Clients(connectionIds.ToArray());
        }

        public static IClientProxy ClientsExceptById(this IHubContext<GameHub> context, List<GameUser> users, params string[] excludedIds)
        {
            var connectionIds = users.Where(x => !excludedIds.Contains(x.Id)).Select(x => x.ConnectionId);
            return context.Clients(connectionIds.ToArray());
        }
    }
}
