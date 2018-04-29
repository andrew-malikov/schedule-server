using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Converters;
using ScheduleServer.Models;
using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public class OsuTutorApi : OsuClientApi {
        protected TutorJsonConverter converter;

        public OsuTutorApi(OsuApiConfig config, TutorJsonConverter converter) : base(config) {
            this.converter = converter;
        }

        public async Task<List<Tutor>> GetCourses(Faculty faculty, Department department) {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("who", "2"),
                new KeyValuePair<string, string>("request", "prep"),
                new KeyValuePair<string, string>("filial", "1"),
                new KeyValuePair<string, string>("facult", faculty.Code),
                new KeyValuePair<string, string>("kafedra", department.Code)
            });
            HttpResponseMessage response = await DefaultSend(formData);

            var data = JObject.Parse(await GetContent(response, DefaultEncoding))["list"];
            var tutors = new List<Tutor>();

            foreach (var tutor in data) {
                tutors.Add(converter.Convert(tutor));
            }

            return tutors;
        }
    }
}