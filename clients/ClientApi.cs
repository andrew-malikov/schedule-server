using System.Net.Http;
using System.Threading.Tasks;

namespace ScheduleServer.Clients {
    public abstract class ClientApi {
        protected async Task<HttpResponseMessage> Send(HttpClient client, HttpRequestMessage request) {
            return await client.SendAsync(request);
        }
    }
}