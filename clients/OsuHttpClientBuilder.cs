using System;
using System.Net.Http;

namespace ScheduleServer.Clients {
    public class OsuHttpClientBuilder {
        protected HttpClient client;

        public OsuHttpClientBuilder() {
            client = new HttpClient();
        }

        public OsuHttpClientBuilder SetBaseUri(Uri baseUri) {
            client.BaseAddress = baseUri;

            return this;
        }

        public OsuHttpClientBuilder SetHeader() {
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            return this;
        }

        public HttpClient Build() {
            return client;
        }
    }
}