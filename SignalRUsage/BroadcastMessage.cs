using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WebEditor
{
     public class BroadcastMessage : PersistentConnection
     {
          protected override Task OnConnected(IRequest request, string connectionId)
          {
               return Connection.Send(connectionId, "true");
          }


          protected override Task OnReceived(IRequest request, string connectionId, string data)
          {
               return Connection.Broadcast(data, new string[] { connectionId });
          }


     }
}