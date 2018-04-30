using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using AngleSharp.Parser.Html;

using ScheduleServer.Converters;
using ScheduleServer.Models;
using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public class OsuGroupApi : OsuClientApi {
        protected GroupJsonConverter groupConverter;
        protected GroupScheduleHtmlConverter scheduleConverter;

        public OsuGroupApi(OsuApiConfig config, GroupJsonConverter groupConverter, GroupScheduleHtmlConverter scheduleConverter) : base(config) {
            this.groupConverter = groupConverter;
            this.scheduleConverter = scheduleConverter;
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
                var group = groupConverter.Convert(rawGroup);

                group.Course = course;
                group.Faculty = faculty;

                groups.Add(group);
            }

            return groups;
        }

        public async Task<GroupSchedule> GetSchedule(Group group) {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("request", "rasp"),
                new KeyValuePair<string, string>("mode", "full"),
                new KeyValuePair<string, string>("filial", "1"),
                new KeyValuePair<string, string>("facult", group.Faculty.Code),
                new KeyValuePair<string, string>("potok", group.Course.Code),
                new KeyValuePair<string, string>("potok", group.Code)
            });
            HttpResponseMessage response = await DefaultSend(formData);

            var responseData = JObject.Parse(await GetContent(response, DefaultEncoding))["content"].Value<string>();
            var document = new HtmlParser().Parse(responseData);
            var htmlTable = document.QuerySelector("tbody");

            var schedule = scheduleConverter.Convert(htmlTable);

            schedule.Group = group;

            return schedule;
        }
    }
}