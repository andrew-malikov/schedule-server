using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public abstract class OsuClientApi : ClientApi {
        protected OsuApiConfig config;

        public OsuClientApi(OsuApiConfig config) {
            this.config = config;
        }

        protected async Task<HttpResponseMessage> DefaultSend(FormUrlEncodedContent formData) {
            using (var client = GetHttpClient()) {
                return await Send(client, GetHttpRequest(formData));
            }
        }

        protected async Task<HttpResponseMessage> DefaultSend(Uri uri, FormUrlEncodedContent formData, HttpMethod method) {
            using (var client = GetHttpClient(uri)) {
                return await Send(client, GetHttpRequest(formData, method));
            }
        }

        protected async Task<string> GetContent(HttpResponseMessage response) {
            return new StreamReader(await response.Content.ReadAsStreamAsync()).ReadToEnd();
        }

        protected async Task<string> GetContent(HttpResponseMessage response, Encoding encoding) {
            return new StreamReader(await response.Content.ReadAsStreamAsync(), encoding).ReadToEnd();
        }

        protected HttpClient GetHttpClient() {
            return new OsuHttpClientBuilder().SetBaseUri(config.GetBaseUri()).SetHeader().Build();
        }

        protected HttpClient GetHttpClient(Uri uri) {
            return new OsuHttpClientBuilder().SetBaseUri(uri).Build();
        }

        protected HttpRequestMessage GetHttpRequest(HttpContent content) {
            return new OsuHttpRequestBuilder().SetHttpContent(content).SetMethod(HttpMethod.Post).Build();
        }

        protected HttpRequestMessage GetHttpRequest(HttpContent content, HttpMethod method) {
            return new OsuHttpRequestBuilder().SetHttpContent(content).SetMethod(method).Build();
        }

        protected Encoding DefaultEncoding => Encoding.GetEncoding("koi8r");
    }
}