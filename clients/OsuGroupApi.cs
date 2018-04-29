using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Converters;
using ScheduleServer.Models;
using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public class OsuGroupApi : OsuClientApi {
        protected GroupJsonConverter converter;

        public OsuGroupApi(OsuApiConfig config, GroupJsonConverter converter) : base(config) {
            this.converter = converter;
        }

        public async Task<List<Group>> GetGroups(Faculty faculty, Course course) {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("request", "group"),
                new KeyValuePair<string, string>("filial", "1"),
                new KeyValuePair<string, string>("facult", faculty.Code),
                new KeyValuePair<string, string>("potok", course.Code)
            });
            HttpResponseMessage response = await DefaultSend(formData);

            var data = JObject.Parse(await GetContent(response, DefaultEncoding))["list"];
            var groups = new List<Group>();

            foreach (var rawGroup in data) {
                var group = converter.Convert(rawGroup);

                group.Course = course;
                group.Faculty = faculty;

                groups.Add(group);
            }

            return groups;
        }
    }
}