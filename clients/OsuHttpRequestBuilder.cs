using System.Net.Http;

namespace ScheduleServer.Clients {
    public class OsuHttpRequestBuilder {
        protected HttpRequestMessage request;

        public OsuHttpRequestBuilder() {
            request = new HttpRequestMessage();
        }

        public OsuHttpRequestBuilder SetHttpContent(HttpContent content) {
            request.Content = content;

            return this;
        }

        public HttpRequestMessage Build() {
            return request;
        }
    }
}